using System;
using DynamicMVC.DynamicEntityMetadataLibrary.Models;
using DynamicMVC.Shared;
using DynamicMVC.UI.DynamicMVC.Interfaces;

namespace DynamicMVC.UI.DynamicMVC.Managers
{
    public class DynamicEntitySearchManager : IDynamicEntitySearchManager
    {
        private readonly IRequestManager _requestManager;
        private readonly IDynamicMvcManager _dynamicMvcManager;

        public DynamicEntitySearchManager(IRequestManager requestManager, IDynamicMvcManager dynamicMvcManager)
        {
            _dynamicMvcManager = dynamicMvcManager;
            _requestManager = requestManager;
        }

        [Obsolete]
        public DynamicEntitySearchManager(RequestManager requestManager)
        {
            _requestManager = requestManager;
            _dynamicMvcManager = Container.Resolve<IDynamicMvcManager>();
        }

        private DynamicEntityMetadata _dynamicEntityMetadata;
        public DynamicEntityMetadata DynamicEntityMetadata
        {
            get
            {
                if (_dynamicEntityMetadata == null)
                {
                    var typeName = _requestManager.RouteDataDictionary["typeName"].ToString();
                    _dynamicEntityMetadata = _dynamicMvcManager.GetDynamicEntityMetadata(typeName);
                }
                return _dynamicEntityMetadata;
            }
        }
    }
}
