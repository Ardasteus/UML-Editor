using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using UML_Editor.Rendering;
using UML_Editor.Nodes;
using UML_Editor.Rendering.ElementStyles;
using UML_Editor.Enums;
using UML_Editor.EventArguments;
using UML_Editor.Hitboxes;
using UML_Editor.Relationships;
using UML_Editor.Geometry;
using UML_Editor.NodeStructure;
using UML_Editor.ProjectStructure;

namespace UML_Editor
{
    public class Editor
    {
        public Project Project;
        private Renderer Renderer;
        private List<INode> Diagrams = new List<INode>();
        private IFocusableNode focusedNode;
        private IKeyboardFocusableNode FocusedKeyboardNode;
        private BasicContainerNode OptionsPrefab;
        private BasicContainerNode OptionsMenu;
        private ClassDiagramNode CurrentFocusedDiagram;
        private Relationship CurrentFocusedRelationship;
        private bool IsCreatingRelationship = false;
        public bool isFocused = false;
        private ClassDiagramNode RelationshipOrigin { get; set; }
        private RelationshipManager RelationshipManager = new RelationshipManager();
        private ClassDiagramNode Dragged;
        private Vector DraggingVector;
        public Editor(PictureBox renderTarget, string ProjectName)
        {
            Project = new Project(ProjectName);
            Renderer = new Renderer(renderTarget);
        }
        public void Render()
        {
            if(isFocused)
            {
                Clear();
                Diagrams.OfType<IRenderableNode>().ToList().ForEach(x => x.Render(Renderer));
                OptionsMenu?.Render(Renderer);
                RelationshipManager.Render(Renderer);
                Renderer.Render();
            }
        }

        public void Clear()
        {
            Renderer.Clear();
        }

        public void ClearFocus()
        {
            focusedNode?.OnUnfocused?.Invoke(this, new NodeEventArgs(focusedNode));
            focusedNode = null;
        }

        public void AddDiagram(ClassStructure structure)
        {
            ClassDiagramNode node = new ClassDiagramNode(structure, new BasicNodeStructure(structure.Position, 0, Renderer.SingleTextHeight), RectangleRenderElementStyle.Default);
            node.AddNode(new PropertyNode(new PropertyStructure(Vector.Zero, "Prop", "Type", AccessModifiers.Public, Modifiers.None), new BasicNodeStructure(Vector.Zero, 0, Renderer.SingleTextHeight), RectangleRenderElementStyle.Textbox));
            node.AddNode(new MethodNode(new MethodStructure(Vector.Zero, "Method", "Type", "Name : Type", AccessModifiers.Public, Modifiers.None), new BasicNodeStructure(Vector.Zero, 0, Renderer.SingleTextHeight), RectangleRenderElementStyle.Textbox));
            if (!Project.Classes.Contains(structure))
                Project.AddClass(structure);
            Diagrams.Add(node);
            node.OnRemoval += (sender, args) => RemoveDiagram((ClassDiagramNode) args.Node);
        }

        public void RemoveDiagram(ClassDiagramNode diagram)
        {
            Diagrams.Remove(diagram);
        }

        public void OnKeyPress(object sender, KeyPressEventArgs e)
        {
            if(focusedNode != null && focusedNode is IKeyboardFocusableNode keyNode)
            {
                if(e.KeyChar != (char)13)
                    keyNode.OnKeyPress?.Invoke(sender, e);
                else
                {
                    ClearFocus();
                }
            }
            Render();
        }

        public void OnMouseMove(object sender, MouseEventArgs e)
        {
            Vector mouse_position = (Vector)e.Location / Renderer.Scale - Renderer.Origin;
            if (e.Button == MouseButtons.Left)
            {
                ClearFocus();
                if (Dragged != null)
                    Dragged.Position = mouse_position + DraggingVector;
                else
                {
                    mouse_position = e.Location - Vector.Zero;
                    Renderer.Origin = mouse_position + DraggingVector;
                }
            }
            Render();
        }
        public void OnMouseDown(object sender, MouseEventArgs e)
        {
            Vector mouse_position = (Vector)e.Location / Renderer.Scale - Renderer.Origin;
            if (e.Button == MouseButtons.Left)
            {
                Dragged = ((ClassDiagramNode)Diagrams.FirstOrDefault(x => CheckIfClicked(mouse_position, x)));
                if (Dragged != null)
                    DraggingVector = Dragged.Position - mouse_position;
                else
                {
                    mouse_position = e.Location - Vector.Zero;
                    DraggingVector = Renderer.Origin - mouse_position;
                }
            }
        }
        public void OnMouseUp(object sender, MouseEventArgs e)
        {
            if(Dragged != null)
            {
                Dragged = null;
                DraggingVector = Vector.Zero;
            }
        }
        public void OnMouseClick(object sender, MouseEventArgs e)
        {
            Vector mouse_position = (Vector)e.Location / Renderer.Scale - Renderer.Origin;
            ClearFocus();
            if (e.Button == MouseButtons.Left)
            {
                HandleLeftClick(mouse_position, e);
            }
            else if(e.Button == MouseButtons.Right)
            {
                HandleRightClick(mouse_position, e);
            }

            Render();
        }

        private void HandleLeftClick(Vector mouse_position, MouseEventArgs e)
        {
            INode temp = null;
            if (OptionsMenu != null && CheckIfClicked(mouse_position, OptionsMenu))
                temp = OptionsMenu;
            //else if (RelationshipManager.Relationships.Count > 0 && RelationshipManager.Relationships.FirstOrDefault(x => CheckIfClicked(mouse_position, x)) != null)
            //    temp = RelationshipManager.Relationships.FirstOrDefault(x => CheckIfClicked(mouse_position, x));
            else
                temp = Diagrams.FirstOrDefault(x => CheckIfClicked(mouse_position, x));
            if (IsCreatingRelationship && temp != null && temp is ClassDiagramNode)
            {
                if (RelationshipOrigin == null)
                    RelationshipOrigin = (ClassDiagramNode)temp;
                else
                {
                    RelationshipManager.CreateRelationship(RelationshipOrigin, (ClassDiagramNode)temp);
                    RelationshipOrigin = null;
                    IsCreatingRelationship = false;
                }
            }
            else
            {
                IFocusableNode node = SearchForClicked(temp, mouse_position);
                if (node != null)
                {
                    focusedNode = node;
                    node.OnFocused?.Invoke(this, new NodeEventArgs(node));
                    if(node is IMouseFocusableNode mn)
                        mn.OnMouseClick?.Invoke(this, e);
                }
            }
        }
        private void HandleRightClick(Vector mouse_position, MouseEventArgs e)
        {
            INode temp = Diagrams.FirstOrDefault(x => CheckIfClicked(mouse_position, x));
            //if (temp == null)
            //{
            //    temp = RelationshipManager.Relationships.FirstOrDefault(x => CheckIfClicked(mouse_position, x));
            //}
            if (temp == null)
            {
                if (CurrentFocusedDiagram != null)
                {
                    //CurrentFocusedDiagram.Unfocus();
                    CurrentFocusedDiagram = null;
                }
                if(CurrentFocusedRelationship != null)
                {
                    if(CurrentFocusedRelationship.OptionsMenu != null)
                        CurrentFocusedRelationship.ShowOptionsMenu();
                    CurrentFocusedRelationship = null;
                }
                if (OptionsMenu == null)
                {
                    GeneratePrefab(mouse_position);
                    OptionsMenu = OptionsPrefab;
                }
                else
                    OptionsMenu = null;
            }
            else if(temp is ClassDiagramNode || temp is Relationship || temp is IOptionsNode)
            {
                if(temp is ClassDiagramNode cn)
                {
                    CurrentFocusedDiagram = (ClassDiagramNode)temp;
                    IOptionsNode op = SearchForOptionsNode(temp, mouse_position);
                    op.OptionsPrefab.Position = mouse_position;
                    op.OnOptionsShow?.Invoke(this, EventArgs.Empty);
                }
                //else if(temp is Relationship r)
                //{
                //    if(CurrentFocusedDiagram != null)
                //    {
                //        CurrentFocusedDiagram = null;
                //    }
                //    if (CurrentFocusedRelationship != null)
                //    {
                //        if(CurrentFocusedRelationship != r)
                //        {
                //            CurrentFocusedRelationship.ShowOptionsMenu();
                //            CurrentFocusedRelationship = null;
                //        }
                //    }
                //    CurrentFocusedRelationship = r;
                //    if(r.OptionsMenu != null)
                //    {
                //        r.ShowOptionsMenu();
                //    }
                //    r.OptionsPrefab.Position = mouse_position;
                //    r.ShowOptionsMenu();
                //}
            }
        }

        private IFocusableNode SearchForClicked(INode parent_node, Vector mouse_position)
        {
            bool found = false;
            while (!found)
            {
                if (parent_node is IContainerNode cn && parent_node is IMouseFocusableNode mn)
                {
                    INode n = cn.Children.FirstOrDefault(x => CheckIfClicked(mouse_position, x));
                    if (n == null)
                        return mn;
                    else
                        parent_node = n;
                }
                else if (parent_node is IContainerNode c)
                {
                    INode n = c.Children.FirstOrDefault(x => CheckIfClicked(mouse_position, x));
                    if (n == null)
                        return null;
                    else
                        parent_node = n;
                }
                else if (parent_node is IFocusableNode m)
                    return m;
                else
                    return null;
            }
            return null;
        }
        private IOptionsNode SearchForOptionsNode(INode parent_node, Vector mouse_position)
        {
            while(true)
            {
                if (parent_node is IContainerNode cn && parent_node is IOptionsNode op)
                {
                    INode n = cn.Children.FirstOrDefault(x => CheckIfClicked(mouse_position, x));
                    if (n == null || !(n is IOptionsNode))
                        return op;
                    else
                        parent_node = n;
                }
                else if (parent_node is IOptionsNode o)
                    return o;
                else
                    return null;
            }
        }

        private bool CheckIfClicked(Vector position, INode node)
        {
            foreach  (IHitbox hitbox in node.TriggerAreas)
            {
                if (hitbox.HasTriggered(position))
                    return true;
            }
            return false;
        }
        private void GeneratePrefab(Vector mouse_positon)
        {
            float total_Width = Renderer.GetTextWidth(21);
            OptionsPrefab = new BasicContainerNode(new BasicNodeStructure(mouse_positon, total_Width, Renderer.SingleTextHeight * 3), RectangleRenderElementStyle.Default);
            OptionsPrefab.AddNode(new ButtonNode(new ButtonStructure(Vector.Zero, "Add a Diagram", total_Width, Renderer.SingleTextHeight, () =>
                {
                    AddDiagram(new ClassStructure(mouse_positon, "NewClass", "class", AccessModifiers.Public, Modifiers.None));
                    OptionsMenu = null;
                }),
                RectangleRenderElementStyle.Default,
                TextRenderElementStyle.Default));
            OptionsPrefab.AddNode(new ButtonNode(new ButtonStructure(Vector.Zero, "Make Abstract", total_Width, Renderer.SingleTextHeight, () =>
                {
                    OptionsMenu = null;
                }),
                RectangleRenderElementStyle.Default,
                TextRenderElementStyle.Default));
            OptionsPrefab.AddNode(new ButtonNode(new ButtonStructure(Vector.Zero, "Make Static", total_Width, Renderer.SingleTextHeight, () =>
                {
                    OptionsMenu = null;
                }),
                RectangleRenderElementStyle.Default,
                TextRenderElementStyle.Default));
        }

        private void GenerateCode()
        {
            foreach (ClassDiagramNode classNode in Diagrams.OfType<ClassDiagramNode>())
            {
                //CodeGenerator generator = new CodeGenerator("D:\\Testing\\" + classNode.NameTextBox.Text + ".cs", classNode);
                //generator.GenerateClass();
            }
        }

        public void OnMouseWheel(object sender, MouseEventArgs e)
        {
            float PreviousScale = Renderer.Scale;
            if(e.Delta > 0)
                Renderer.Scale *= 1.1f;
            else
                Renderer.Scale *= 0.9f;

            Vector PreScale = (Vector)e.Location / PreviousScale - Renderer.Origin;
            Vector AfterScale = (Vector)e.Location / Renderer.Scale - Renderer.Origin;

            Vector Subtracted = PreScale - AfterScale;

            Renderer.Origin = Renderer.Origin - Subtracted;

            Render();
        }
        public void OnFormResize(object sender, EventArgs e)
        {
            Renderer.Resize();
            Render();
        }
    }
}
