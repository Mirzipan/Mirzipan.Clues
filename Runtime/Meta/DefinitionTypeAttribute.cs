﻿using System;

namespace Mirzipan.Clues.Meta
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public class DefinitionTypeAttribute: Attribute
    {
        public Type IndexedType { get; set; }

        public DefinitionTypeAttribute(Type indexedType = null)
        {
            IndexedType = indexedType;
        }
    }
}