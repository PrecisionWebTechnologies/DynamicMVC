using System;
using System.Collections.Generic;
using System.Linq;
using DynamicMVC.UI.DynamicMVC.Interfaces;

namespace DynamicMVC.UI.DynamicMVC
{
    public static class DynamicMVCContext
    {
        static DynamicMVCContext()
        {
            Options = new DynamicMVCContextOptions();
        }

        public static DynamicMVCContextOptions Options { get; set; }

        public static IEnumerable<DynamicEntityMetadataLibrary.Models.DynamicEntityMetadata> DynamicEntityMetadatas { get; set; }
        
        public static IEnumerable<Type> DynamicFilterViewModels { get; set; }

        public static DynamicEntityMetadataLibrary.Models.DynamicEntityMetadata GetDynamicEntityMetadata(string typeName)
        {
            return DynamicEntityMetadatas.Single(x => x.TypeName() == typeName);
        }

        public static IDynamicMvcManager DynamicMvcManager;
    }
}
