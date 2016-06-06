using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DynamicMVC.Annotations.Enums;
using DynamicMVC.DynamicEntityMetadataLibrary.Models;
using ReflectionLibrary.Interfaces;

namespace DynamicMVC.DynamicEntityMetadataLibrary.Interfaces
{
    public interface IDynamicMethodManager
    {
        IEnumerable<DynamicMethod> GetDynamicMethods(IReflectedClass reflectedClass);
        IEnumerable<DynamicMethod> GetDynamicMethods(TemplateTypeEnum templateTypeEnum, IEnumerable<DynamicMethod> dynamicMethods);
    }
}
