using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UML_Editor.EventArguments
{
    public class TextEventArgs : EventArgs
    {
        public string Text { get; }

        public TextEventArgs(string text)
        {
            Text = text;
        }
    }
}
