using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UML_Editor.Geometry;

namespace UML_Editor.NodeStructure
{
    public class ButtonStructure : BasicTextNodeStructure
    {
        public Action ButtonAction { get; set; }
        public string ButtonText { get; set; }
        public ButtonStructure(Vector pos, string text, float width, float height, Action action) : base(pos, width, height, text)
        {
            ButtonAction = action;
            ButtonText = text;
        }
    }
}
