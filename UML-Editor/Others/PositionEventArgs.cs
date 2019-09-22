﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UML_Editor.Rendering;

namespace UML_Editor.Others
{
    public class PositionEventArgs : EventArgs
    {
        public Vector Position;
        public PositionEventArgs(Vector position)
        {
            Position = position;
        }
    }
}