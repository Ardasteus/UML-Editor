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

namespace UML_Editor
{
    public class Editor
    {
        private Renderer Renderer;
        private List<INode> Nodes = new List<INode>();
        private IKeyboardHandlerNode FocusedKeyboardNode;
        public Editor(PictureBox renderTarget)
        {
            Renderer = new Renderer(renderTarget);
            renderTarget.MouseClick += OnMouseClick;
            renderTarget.MouseMove += OnMouseMove;
            AddNode(new ButtonNode("btn1", new Vector(50, 50), 50, 50, () => SwitchAllResize(), new RectangleRenderElementStyle(Color.Black, Color.AliceBlue, 1)));
        }

        private void SwitchAllResize()
        {
            if(Nodes[0] != null)
            {
                if (Nodes[0].Resize)
                    Nodes.ForEach(x => x.Resize = false);
                else
                    Nodes.ForEach(x => x.Resize = true);
            }
        }

        public void Render()
        {
            Clear();
            Nodes.OfType<IRenderableNode>().ToList().ForEach(x => x.Render(Renderer));
            Renderer.Render();
        }

        public void Clear()
        {
            Renderer.Clear();
        }

        public void AddNode(INode node)
        {
            Nodes.Add(node);
        }

        public void ResizeAll(int width)
        {
            Nodes.ForEach(x => x.ForceResize(width));
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
            Render();

        }
        public void OnMouseClick(object sender, MouseEventArgs e)
        {
            Vector mouse_position = e.Location - Renderer.Origin;
            INode temp = Nodes.FirstOrDefault(x => CheckIfClicked(mouse_position, x));
            IMouseHandlerNode node = SearchForClicked(temp, mouse_position);
            if(node != null)
            {
                if(FocusedKeyboardNode != null)
                {
                    if(node is IKeyboardHandlerNode kn)
                    {
                        if(FocusedKeyboardNode != kn)
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
                    if(node is IKeyboardHandlerNode n)
                    {
                        FocusedKeyboardNode = n;
                    }
                }

                node.isFocused = true;
                node.HandleMouse();
            }
            else
            {
                if(FocusedKeyboardNode != null)
                {
                    FocusedKeyboardNode.isFocused = false;
                    FocusedKeyboardNode = null;
                }
            }

            Render();
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

        private bool CheckIfClicked(Vector position, INode node)
        {
            int left = node.Position.X;
            int right = node.Position.X + node.Width;
            int top = node.Position.Y;
            int bot = node.Position.Y + node.Height;
            return position.X <= right && position.X >= left && position.Y <= bot && position.Y >= top;
        }
    }
}
