using System;
using DynamicMVC.ApplicationMetadataLibrary.Interfaces;
using DynamicMVC.Shared.Enums;
using DynamicMVC.Shared.Interfaces;

namespace DynamicMVC.ApplicationMetadataLibrary.Strategies.SimpleTypeParsers
{
    /// <summary>
    /// Simple Type Parser for Int64 datatype
    /// </summary>
    public class SimpleTypeInt64Parser : ISimpleTypeParser
    {
        public Type GetSimpleType()
        {
            return typeof(long);
        }

        public dynamic Parse(string value)
        {
            return long.Parse(value);
        }

        public SimpleTypeEnum SimpleTypeEnum()
        {
            return Shared.Enums.SimpleTypeEnum.Int64;
        }
    }
}
