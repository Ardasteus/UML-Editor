using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UML_Editor.Rendering.RenderingElements;
using UML_Editor.Enums;
using UML_Editor.Rendering;
using System.Drawing;

namespace UML_Editor.Nodes.Members
{
    public class PropertyNode : IChildNode
    {
        public Vector Position { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public List<IRenderElement> RenderElements { get; set; } = new List<IRenderElement>();
        private string FullName;
        public string Name { get; set; }
        public string ValueType { get; set; }
        public AccessModifier AccessModifier { get; set; }     
        public PropertyNode(string name, string value_type, AccessModifier access_modifier)
        {
            Name = name;
            ValueType = value_type;
            AccessModifier = access_modifier;
            GenerateFullName();
        }
        private void GenerateRenderElements()
        {
            RenderElements.Add(new TextRenderElement(new Vector(Position.X - Width / 2, Position.Y), FullName, Color.Black));
        }

        private void GenerateFullName()
        {
            string modifier;
            switch (AccessModifier)
            {
                case AccessModifier.Private:
                    modifier = "-";
                    break;
                case AccessModifier.Public:
                    modifier = "+";
                    break;
                case AccessModifier.Protected:
                    modifier = "#";
                    break;
                default:
                    modifier = "E";
                    break;
            }

            FullName = modifier + " | " + ValueType + " " + Name;
            Width = FullName.Length;
        }
    }
}
