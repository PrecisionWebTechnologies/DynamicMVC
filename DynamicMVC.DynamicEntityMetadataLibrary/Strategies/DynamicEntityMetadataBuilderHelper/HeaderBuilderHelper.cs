using System.Linq;
using DynamicMVC.Annotations;
using DynamicMVC.DynamicEntityMetadataLibrary.Interfaces;
using DynamicMVC.DynamicEntityMetadataLibrary.Models;
using DynamicMVC.EntityMetadataLibrary.Models;

namespace DynamicMVC.DynamicEntityMetadataLibrary.Strategies.DynamicEntityMetadataBuilderHelper
{
   public  class HeaderBuilderHelper : IDynamicEntityMetadataBuilderHelper
    {
       public void Build(DynamicEntityMetadata dynamicEntityMetadata, EntityMetadata entityMetadata)
       {
           var dynamicHeader = (DynamicHeaderAttribute)entityMetadata.EntityAttributes.FirstOrDefault(x => x is DynamicHeaderAttribute);
           if (dynamicHeader != null)
           {
               //set create header
               if (!string.IsNullOrWhiteSpace(dynamicHeader.CreateHeader))
                   dynamicEntityMetadata.CreateHeader = dynamicHeader.CreateHeader;
               else
                   dynamicEntityMetadata.CreateHeader = "Create " + dynamicEntityMetadata.TypeName;

               //set details header
               if (!string.IsNullOrWhiteSpace(dynamicHeader.DetailsHeader))
                   dynamicEntityMetadata.DetailsHeader = dynamicHeader.DetailsHeader;
               else
                   dynamicEntityMetadata.DetailsHeader = dynamicEntityMetadata.TypeName + " Details";

               //set edit header
               if (!string.IsNullOrWhiteSpace(dynamicHeader.EditHeader))
                   dynamicEntityMetadata.EditHeader = dynamicHeader.EditHeader;
               else
                   dynamicEntityMetadata.EditHeader = "Edit " + dynamicEntityMetadata.TypeName;

               //Index Header
               if (!string.IsNullOrWhiteSpace(dynamicHeader.IndexHeader))
                   dynamicEntityMetadata.IndexHeader = dynamicHeader.IndexHeader;
               else
                   dynamicEntityMetadata.IndexHeader = dynamicEntityMetadata.TypeName + " List";

               //Delte Header 
               if (!string.IsNullOrWhiteSpace(dynamicHeader.DeleteHeader))
                   dynamicEntityMetadata.DeleteHeader = dynamicHeader.DeleteHeader;
               else
                   dynamicEntityMetadata.DeleteHeader = "Delete " + dynamicEntityMetadata.TypeName;
           }
           else
           {
               dynamicEntityMetadata.CreateHeader = "Create " + dynamicEntityMetadata.TypeName;
               dynamicEntityMetadata.DetailsHeader = dynamicEntityMetadata.TypeName + " Details";
               dynamicEntityMetadata.EditHeader = "Edit " + dynamicEntityMetadata.TypeName;
               dynamicEntityMetadata.IndexHeader = dynamicEntityMetadata.TypeName + " List";
               dynamicEntityMetadata.DeleteHeader = "Delete " + dynamicEntityMetadata.TypeName;
           }
       }
    }
}
