﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UML_Editor.Enums;
using UML_Editor.Geometry;

namespace UML_Editor.ProjectStructure
{
    public class PropertyStructure : BasicCodeStructure
    {
        public PropertyStructure(Vector position, string name, string type, AccessModifiers accessModifier, Modifiers modifier) : base(position, name, type, accessModifier, modifier)
        {
        }
    }
}
