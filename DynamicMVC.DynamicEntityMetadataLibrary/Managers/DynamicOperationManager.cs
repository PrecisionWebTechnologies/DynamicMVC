using System;
using System.Collections.Generic;
using System.Linq;
using DynamicMVC.Annotations.Enums;
using DynamicMVC.DynamicEntityMetadataLibrary.Interfaces;
using DynamicMVC.DynamicEntityMetadataLibrary.Models;

namespace DynamicMVC.DynamicEntityMetadataLibrary.Managers
{
    /// <summary>
    /// Encapsulates all logic for Dynamic Operations
    /// </summary>
    public class DynamicOperationManager : IDynamicOperationManager
    {
        public DynamicOperation GetDynamicOperation(TemplateTypeEnum templateTypeEnum, string submitValue, IEnumerable<DynamicMethod> dynamicMethods)
        {
            var result = new DynamicOperation();
            var matchingSubmitValues = dynamicMethods.Where(x => x.SubmitValue == submitValue).ToList();
            matchingSubmitValues = matchingSubmitValues.Where(x => x.TemplateTypeEnum.HasFlag(templateTypeEnum)).ToList();
            if (matchingSubmitValues.Count > 1)
                throw new Exception("Duplicate dynamic methods found for submit value " + submitValue + " and templatetype " + templateTypeEnum.ToString());

            result.DynamicMethod = matchingSubmitValues.SingleOrDefault();
            return result;
        }
    }
}
