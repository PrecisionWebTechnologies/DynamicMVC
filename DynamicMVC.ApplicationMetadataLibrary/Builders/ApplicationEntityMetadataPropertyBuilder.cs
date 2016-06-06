using System;
using System.Collections.Generic;
using DynamicMVC.ApplicationMetadataLibrary.Interfaces;
using DynamicMVC.ApplicationMetadataLibrary.Models;

namespace DynamicMVC.ApplicationMetadataLibrary.Builders
{
    public class ApplicationEntityMetadataPropertyBuilder : IApplicationEntityMetadataPropertyBuilder
    {
        public IEnumerable<ApplicationEntityMetadataProperty> Build(Type type)
        {
            var applicationEntityMetadataProperties = new List<ApplicationEntityMetadataProperty>();
            foreach (var propertyInfo in type.GetProperties())
            {
                var applicationEntityMetadataProperty = new ApplicationEntityMetadataProperty(propertyInfo.Name);
                foreach (var attribute in propertyInfo.GetCustomAttributes(true))
                {
                    applicationEntityMetadataProperty.Attributes.Add((Attribute)attribute);
                }
                applicationEntityMetadataProperties.Add(applicationEntityMetadataProperty);
            }
            return applicationEntityMetadataProperties;
        }
    }
}
