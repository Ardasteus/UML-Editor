using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using UML_Editor.Rendering;
using UML_Editor.Nodes;

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

        public void OnKeyPress(object sender, KeyPressEventArgs e)
        {
            if(FocusedKeyboardNode != null)
                FocusedKeyboardNode.HandleKey(e.KeyChar);
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

            IMouseHandlerNode node = Nodes.OfType<IMouseHandlerNode>().ToList().Where(x => CheckIfClicked(mouse_position, x)).FirstOrDefault();
            if(node != null)
            {
                node.isFocused = true;
                if (node is IKeyboardHandlerNode n)
                {
                    FocusedKeyboardNode = n;
                }
            }
            else
            {
                if(FocusedKeyboardNode != null)
                {
                    if(FocusedKeyboardNode is IMouseHandlerNode n)
                        n.isFocused = false;
                    FocusedKeyboardNode = null;
                }
            }
            Render();
        }

        private bool CheckIfClicked(Vector position, IMouseHandlerNode node)
        {
            int left = node.Position.X;
            int right = node.Position.X + node.Width;
            int top = node.Position.Y;
            int bot = node.Position.Y + node.Height;
            return position.X <= right && position.X >= left && position.Y <= bot && position.Y >= top;
        }
    }
}
