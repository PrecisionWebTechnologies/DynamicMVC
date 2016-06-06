using System;
using System.Collections.Generic;

namespace ReflectionLibrary.Interfaces
{
    public interface IEntityWithAttributes
    {
        ICollection<Attribute> Attributes { get; set; }
    }
}
