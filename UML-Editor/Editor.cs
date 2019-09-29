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
using UML_Editor.Others;
using UML_Editor.Relationships;
using UML_Editor.CodeGenerating;

namespace UML_Editor
{
    public class Editor
    {
        private Renderer Renderer;
        private List<INode> Nodes = new List<INode>();
        private IKeyboardHandlerNode FocusedKeyboardNode;
        private ContextMenuNode OptionsPrefab;
        private ContextMenuNode OptionsMenu;
        private ClassDiagramNode CurrentFocus;
        private bool IsCreatingRelationship = false;
        private ClassDiagramNode RelationshipOrigin { get; set; }
        private RelationshipManager RelationshipManager = new RelationshipManager();
        private ClassDiagramNode Dragged;
        private Vector DraggingVector;
        public Editor(PictureBox renderTarget)
        {
            Renderer = new Renderer(renderTarget);
            renderTarget.MouseClick += OnMouseClick;
            renderTarget.MouseMove += OnMouseMove;
            renderTarget.MouseDown += OnMouseDown;
            renderTarget.MouseUp += OnMouseUp;
            //AddNode(new ButtonNode("btn1", new Vector(50, 50), 50, Renderer.GetTextHeight(1), () => SwitchAllResize(), new RectangleRenderElementStyle(Color.Black, Color.AliceBlue, 1)));
            AddDiagram(new ClassDiagramNode(new Vector(-100, -100), "Class", Modifiers.None, AccessModifiers.Public));
            ((ClassDiagramNode)Nodes[0]).AddProperty("Prop", "String", AccessModifiers.Public, Modifiers.None);
            ((ClassDiagramNode)Nodes[0]).AddMethod("Method", "void", AccessModifiers.Public, Modifiers.None);
            ((ClassDiagramNode)Nodes[0]).AddProperty("Prop", "String", AccessModifiers.Public, Modifiers.None);
            ((ClassDiagramNode)Nodes[0]).AddMethod("Method", "void", AccessModifiers.Public, Modifiers.None);
            AddDiagram(new ClassDiagramNode(new Vector(100, 100), "Class", Modifiers.None, AccessModifiers.Public));
            ((ClassDiagramNode)Nodes[1]).AddProperty("Prop", "String", AccessModifiers.Public, Modifiers.None);
            ((ClassDiagramNode)Nodes[1]).AddMethod("Method", "void", AccessModifiers.Public, Modifiers.None);
            ((ClassDiagramNode)Nodes[1]).AddProperty("Prop", "String", AccessModifiers.Public, Modifiers.None);
            ((ClassDiagramNode)Nodes[1]).AddMethod("Method", "void", AccessModifiers.Public, Modifiers.None);
            //InitializeRedrawTimer();
        }
        public void Render()
        {
            Clear();
            Nodes.OfType<IRenderableNode>().ToList().ForEach(x => x.Render(Renderer));
            OptionsMenu?.Render(Renderer);
            RelationshipManager.Render(Renderer);
            Renderer.Render();
        }

        public void Clear()
        {
            Renderer.Clear();
        }

        public void AddDiagram(UMLDiagram node)
        {
            Nodes.Add(node);
            node.OnRemoval += OnDiagramRemoval;
        }

        private void OnDiagramRemoval(object sender, DiagramRemovalEventArgs e)
        {
            RemoveDiagram(e.Diagram);
        }
        public void RemoveDiagram(UMLDiagram diagram)
        {
            Nodes.Remove(diagram);
            diagram = null;
        }

        public void OnKeyPress(object sender, KeyPressEventArgs e)
        {
            if(FocusedKeyboardNode != null)
            {
                if(e.KeyChar != (char)13)
                    FocusedKeyboardNode.HandleKey(e.KeyChar);
                else
                {
                    FocusedKeyboardNode.HandleKey(e.KeyChar);
                    if (!FocusedKeyboardNode.isFocused)
                        FocusedKeyboardNode = null;
                }
            }
            Render();
        }
        public void OnKeyDown(object sender, KeyEventArgs e)
        {
            Render();
        }
        public void OnKeyUp(object sender, KeyEventArgs e)
        {
            Render();
        }

        public void OnMouseMove(object sender, MouseEventArgs e)
        {
            Vector mouse_position = e.Location - Renderer.Origin;
            if (e.Button == MouseButtons.Left)
            {
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
            Vector mouse_position = e.Location - Renderer.Origin;
            if (e.Button == MouseButtons.Left)
            {
                Dragged = ((ClassDiagramNode)Nodes.FirstOrDefault(x => CheckIfClicked(mouse_position, x)));
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
            Vector mouse_position = e.Location - Renderer.Origin;
            if(e.Button == MouseButtons.Left)
            {
                HandleLeftClick(mouse_position);
            }
            else if(e.Button == MouseButtons.Right)
            {
                HandleRightClick(mouse_position);
            }

            Render();
        }

        private void HandleLeftClick(Vector mouse_position)
        {
            INode temp = null;
            if (OptionsMenu != null && CheckIfClicked(mouse_position, OptionsMenu))
                temp = OptionsMenu;
            else
                temp = Nodes.FirstOrDefault(x => CheckIfClicked(mouse_position, x));
            if (IsCreatingRelationship && temp != null)
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
                IMouseHandlerNode node = SearchForClicked(temp, mouse_position);
                if (node != null)
                {
                    if (FocusedKeyboardNode != null)
                    {
                        if (node is IKeyboardHandlerNode kn)
                        {
                            if (FocusedKeyboardNode != kn)
                            {
                                FocusedKeyboardNode.isFocused = false;
                                FocusedKeyboardNode = kn;
                            }
                        }
                        else
                        {
                            FocusedKeyboardNode.isFocused = false;
                            FocusedKeyboardNode = null;
                        }
                    }
                    else
                    {
                        if (node is IKeyboardHandlerNode n)
                        {
                            FocusedKeyboardNode = n;
                        }
                    }

                    node.isFocused = true;
                    node.HandleMouse();
                }
                else
                {
                    if (FocusedKeyboardNode != null)
                    {
                        FocusedKeyboardNode.isFocused = false;
                        FocusedKeyboardNode = null;
                    }
                }
            }
        }
        private void HandleRightClick(Vector mouse_position)
        {
            INode temp = Nodes.FirstOrDefault(x => CheckIfClicked(mouse_position, x));
            if(temp == null)
            {
                if (OptionsMenu == null)
                {
                    GeneratePrefab(mouse_position);
                    OptionsMenu = OptionsPrefab;
                }
                else
                {
                    OptionsMenu = null;
                }
            }
            else if(temp is IOptionsNode)
            {
                IOptionsNode op = SearchForOptionsNode(temp, mouse_position);
                op.OptionsPrefab.Position = mouse_position;
                op.ShowOptionsMenu();
            }
        }

        private IMouseHandlerNode SearchForClicked(INode parent_node, Vector mouse_position)
        {
            bool found = false;
            while (!found)
            {
                if (parent_node is IContainerNode cn && parent_node is IMouseHandlerNode mn)
                {
                    INode n = cn.GetChildren().FirstOrDefault(x => CheckIfClicked(mouse_position, x));
                    if (n == null)
                    {
                        return mn;
                    }
                    else
                        parent_node = n;
                }
                else if (parent_node is IContainerNode c)
                {
                    INode n = c.GetChildren().FirstOrDefault(x => CheckIfClicked(mouse_position, x));
                    if (n == null)
                    {
                        return null;
                    }
                    else
                        parent_node = n;
                }
                else if (parent_node is IMouseHandlerNode m)
                {
                    return m;
                }
                else
                {
                    return null;
                }
            }
            return null;
        }
        private IOptionsNode SearchForOptionsNode(INode parent_node, Vector mouse_position)
        {
            while(true)
            {
                if (parent_node is IContainerNode cn && parent_node is IOptionsNode op)
                {
                    INode n = cn.GetChildren().FirstOrDefault(x => CheckIfClicked(mouse_position, x));
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
            OptionsPrefab = new ContextMenuNode("cnt", mouse_positon, 0, 0, RectangleRenderElementStyle.Default);
            OptionsPrefab.AddNode(new ButtonNode("btn1", "Add a Diagram", mouse_positon, Renderer.GetTextWidth(13), Renderer.SingleTextHeight, () =>
            {
                Nodes.Add(new ClassDiagramNode(mouse_positon, "New Class", Modifiers.None, AccessModifiers.Protected));
                OptionsMenu = null;
            },
            RectangleRenderElementStyle.Default));
            OptionsPrefab.AddNode(new ButtonNode("btn1", "Create a Relationship", mouse_positon, Renderer.GetTextWidth(21), Renderer.SingleTextHeight, () =>
            {
                IsCreatingRelationship = true;
                OptionsMenu = null;
            },
            RectangleRenderElementStyle.Default));
            OptionsPrefab.AddNode(new ButtonNode("btn1", "Export", mouse_positon, Renderer.GetTextWidth(6), Renderer.SingleTextHeight, () =>
            {
                GenerateCode();
                OptionsMenu = null;
            },
            RectangleRenderElementStyle.Default));
        }

        private void GenerateCode()
        {
            foreach (ClassDiagramNode classNode in Nodes.OfType<ClassDiagramNode>())
            {
                CodeGenerator generator = new CodeGenerator("D:\\Testing\\" + classNode.NameTextBox.Text + ".cs", classNode);
                generator.GenerateClass();
            }
        }
    }
}
