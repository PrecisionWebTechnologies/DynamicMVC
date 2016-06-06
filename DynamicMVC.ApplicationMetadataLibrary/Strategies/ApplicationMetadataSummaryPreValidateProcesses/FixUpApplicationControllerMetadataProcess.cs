using System.Linq;
using DynamicMVC.ApplicationMetadataLibrary.Interfaces;
using DynamicMVC.ApplicationMetadataLibrary.Models;
using DynamicMVC.Shared.Interfaces;

namespace DynamicMVC.ApplicationMetadataLibrary.Strategies.ApplicationMetadataSummaryPreValidateProcesses
{
    public class FixUpApplicationControllerMetadata : IApplicationMetadataSummaryPreValidateProcess
    {
        private readonly INamingConventionManager _namingConventionManager;

        public FixUpApplicationControllerMetadata(INamingConventionManager namingConventionManager)
        {
            _namingConventionManager = namingConventionManager;
        }

        public void Process(ApplicationMetadataSummary applicationMetadataSummary)
        {
            var controllers = applicationMetadataSummary.ApplicationControllers.ToList();
            var controllerNames = controllers.Select(x => x.Name).ToList();
            foreach (var applicationEntity in applicationMetadataSummary.ApplicationEntities)
            {
                var typeName = applicationEntity.EntityType.Name;
                var controllerName = _namingConventionManager.FindControllerName(controllerNames, typeName);
                if (controllerName != null)
                {
                    var controller = controllers.Single(x => x.Name == controllerName);
                    applicationEntity.ApplicationControllerMetadata = controller;
                }
            }
        }
    }
}
