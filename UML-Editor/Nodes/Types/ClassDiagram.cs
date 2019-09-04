using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UML_Editor.Rendering;
using UML_Editor.Rendering.RenderingElements;
using UML_Editor.Enums;
using System.Drawing;

namespace UML_Editor.Nodes.Types
{
    public class ClassDiagram : IParentNode
    {
        public Vector Position { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int ChildGap { get; set; } = 1;
        public List<IChildNode> Children { get; set; } = new List<IChildNode>();
        public List<IRenderElement> RenderElements { get; set; } = new List<IRenderElement>();
        public string Name { get; set; }
        public AccessModifier AccessModifier { get; set; }
        private bool isAbstract;
        public bool IsAbstract
        {
            get
            {
                return isAbstract;
            }
            set
            {
                if (value == true)
                    IsInterface = false;

                isAbstract = value;
            }
        }
        private bool isInterface;
        public bool IsInterface
        {
            get
            {
                return isInterface;
            }
            set
            {
                if(value == true)
                    IsAbstract = false;

                isInterface = value;
            }
        }
        public ClassDiagram(Vector position, string name, int width, int height)
        {
            Name = name;
            Position = position;
            Width = width;
            Height = height;
            GenerateRenderElements();
        }
        public void AddProperty()
        {

        }

        public void AddMethod()
        {

        }

        public List<IRenderElement> GetAllRenderElements()
        {
            List<IRenderElement> return_list = new List<IRenderElement>();
            foreach (IRenderElement item in RenderElements)
            {
                return_list.Add(item);
            }
            foreach (IChildNode child in Children)
            {
                foreach (IRenderElement element in child.RenderElements)
                {
                    return_list.Add(element);
                }
            }
            return return_list;
        }
        private void GenerateRenderElements()
        {
            RenderElements.Add(new RectangleRenderElement(Position, Width, Height, Color.White, Color.Black));
            RenderElements.Add(new TextRenderElement(new Vector(Position.X + 10, Position.Y), Name, Color.Black));
        }
    }
}
