using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UML_Editor.Geometry;

namespace UML_Editor.NodeStructure
{
    public class BasicTextNodeStructure : BasicNodeStructure
    {
        public BasicTextNodeStructure(Vector pos, float width, float height, string text) : base(pos, width, height)
        {
            Text = text;
        }

        public string Text { get; set; }
    }
}
