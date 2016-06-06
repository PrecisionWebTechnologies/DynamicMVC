using System;
using System.Collections.Generic;
using System.Linq;
using DynamicMVC.ApplicationMetadataLibrary.Interfaces;
using DynamicMVC.ApplicationMetadataLibrary.Models;

namespace DynamicMVC.ApplicationMetadataLibrary.Builders
{
    public class ApplicationControllerMethodMetadataBuilder : IApplicationControllerMethodMetadataBuilder
    {
        public IEnumerable<ApplicationControllerMethodMetadata> Build(ApplicationControllerMetadata applicationControllerMetadata, Type type)
        {
            var result = new List<ApplicationControllerMethodMetadata>();
            foreach (var methodInfo in type.GetMethods())
            {
                result.Add(new ApplicationControllerMethodMetadata(applicationControllerMetadata, methodInfo.Name, methodInfo,
                    methodInfo.CustomAttributes.Select(x=>x.AttributeType).ToList()));
            }

            return result;
        }
    }
}
