using System;
using System.Collections.Generic;
using System.Linq;
using DynamicMVC.Annotations;
using DynamicMVC.Annotations.Enums;
using DynamicMVC.DynamicEntityMetadataLibrary.Interfaces;
using DynamicMVC.DynamicEntityMetadataLibrary.Models;
using ReflectionLibrary.Extensions;
using ReflectionLibrary.Interfaces;

namespace DynamicMVC.DynamicEntityMetadataLibrary.Managers
{
    /// <summary>
    /// Encapsulates all logic for Dynamic Methods
    /// </summary>
    public class DynamicMethodManager : IDynamicMethodManager
    {
        private readonly IDynamicMethodInvoker[] _dynamicMethodInvokers;

        public DynamicMethodManager(IDynamicMethodInvoker[] dynamicMethodInvokers)
        {
            _dynamicMethodInvokers = dynamicMethodInvokers;
        }

        public IEnumerable<DynamicMethod> GetDynamicMethods(IReflectedClass reflectedClass)
        {
            var dynamicMethods = new List<DynamicMethod>();

            var reflectedMethods = reflectedClass.ReflectedMethods.Where(x => x.HasAttribute<DynamicMethodAttribute>());
            foreach (var reflectedMethod in reflectedMethods)
            {
                var dynamicMethodAttribute = reflectedMethod.GetAttribute<DynamicMethodAttribute>();
                var dynamicMethod = new DynamicMethod();


                dynamicMethod.MethodName = reflectedMethod.Name;

                dynamicMethod.SubmitValue = dynamicMethodAttribute.SubmitValue ?? reflectedMethod.Name;
                dynamicMethod.ButtonText = dynamicMethodAttribute.ButtonText ?? reflectedMethod.Name;
                dynamicMethod.RedirectUrl = dynamicMethodAttribute.RedirectUrl;
                dynamicMethod.TemplateTypeEnum = dynamicMethodAttribute.TemplateTypeEnum;
                var dynamicMethodInvokers = _dynamicMethodInvokers.Where(x => x.DynamicMethodInvokerName() == dynamicMethodAttribute.InvokerName);
                if (dynamicMethodInvokers.Count() > 1)
                    throw new Exception("More than one DynamicMethodInvoker was found for InvokerName " + dynamicMethodAttribute.InvokerName + " (Method: " + reflectedMethod.Name + ")");
                if (!dynamicMethodInvokers.Any())
                    throw new Exception("No DynamicMethodInvoker was found for InvokerName " + dynamicMethodAttribute.InvokerName + " (Method: " + reflectedMethod.Name + ")");
                dynamicMethod.DynamicMethodInvoker = dynamicMethodInvokers.Single();
                dynamicMethod.InvokeMethodFunction = reflectedMethod.ReflectedMethodOperations.InvokeFunction;
                dynamicMethods.Add(dynamicMethod);
            }

            return dynamicMethods;
        }

        public IEnumerable<DynamicMethod> GetDynamicMethods(TemplateTypeEnum templateTypeEnum, IEnumerable<DynamicMethod> dynamicMethods)
        {
            var result = new List<DynamicMethod>();
            foreach (var dynamicMethod in dynamicMethods)
            {
                if (dynamicMethod.TemplateTypeEnum.HasFlag(templateTypeEnum))
                    result.Add(dynamicMethod);
            }
            return result;
        }
    }
}
