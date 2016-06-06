using System;
using DynamicMVC.ApplicationMetadataLibrary.Interfaces;
using DynamicMVC.Shared.Enums;
using DynamicMVC.Shared.Interfaces;

namespace DynamicMVC.ApplicationMetadataLibrary.Strategies.SimpleTypeParsers
{
    /// <summary>
    /// Simple Type Parser for Byte datatype
    /// </summary>
    public class SimpleTypeByteParser : ISimpleTypeParser
    {
        public Type GetSimpleType()
        {
            return typeof(byte);
        }

        public dynamic Parse(string value)
        {
            return byte.Parse(value);
        }

        public SimpleTypeEnum SimpleTypeEnum()
        {
            return Shared.Enums.SimpleTypeEnum.Byte;
        }
    }
}
