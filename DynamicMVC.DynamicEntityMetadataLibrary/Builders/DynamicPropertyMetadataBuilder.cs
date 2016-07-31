using System.Collections.Generic;
using System.Linq;
using DynamicMVC.DynamicEntityMetadataLibrary.Interfaces;
using DynamicMVC.DynamicEntityMetadataLibrary.Models;
using ReflectionLibrary.Interfaces;

namespace DynamicMVC.DynamicEntityMetadataLibrary.Builders
{
    public class DynamicPropertyMetadataBuilder : IDynamicPropertyMetadataBuilder
    {
        private readonly INavigationPropertyManager _navigationPropertyManager;

        public DynamicPropertyMetadataBuilder(INavigationPropertyManager navigationPropertyManager)
        {
            _navigationPropertyManager = navigationPropertyManager;
        }

        public IEnumerable<DynamicPropertyMetadata> Build(IReflectedClass reflectedClass, IEnumerable<IReflectedClass> reflectedClasses)
        {
            var dynamicPropertyMetadatas = new List<DynamicPropertyMetadata>();
            foreach (var reflectedProperty in reflectedClass.ReflectedProperties.ToList())
            {
                var dynamicPropertyMetadata = new DynamicPropertyMetadata(reflectedProperty, reflectedClasses);
                if (_navigationPropertyManager.IsForeignKey(reflectedProperty, reflectedClass))
                {
                    dynamicPropertyMetadata = new DynamicForiegnKeyPropertyMetadata(reflectedProperty, reflectedClasses);
                }
                if (dynamicPropertyMetadata.IsDynamicEntity())
                {
                    dynamicPropertyMetadata = new DynamicComplexPropertyMetadata(reflectedProperty, reflectedClasses);
                }
                if (dynamicPropertyMetadata.IsDynamicCollection())
                {
                    dynamicPropertyMetadata = new DynamicCollectionEntityPropertyMetadata(reflectedProperty, reflectedClasses);
                }

                dynamicPropertyMetadatas.Add(dynamicPropertyMetadata);

            }
            return dynamicPropertyMetadatas;
        }
    }
}
