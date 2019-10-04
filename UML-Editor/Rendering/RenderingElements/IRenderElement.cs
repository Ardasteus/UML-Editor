using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UML_Editor.Rendering;
using System.Drawing;

namespace UML_Editor.Rendering.RenderingElements
{
    public interface IRenderElement
    {
        void Render(Renderer renderer);
    }
}
