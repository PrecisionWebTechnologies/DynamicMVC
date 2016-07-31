using System.Collections.Generic;
using System.Linq;
using DynamicMVC.Annotations.Enums;
using DynamicMVC.Data.Interfaces;
using DynamicMVC.DynamicEntityMetadataLibrary.Interfaces;
using DynamicMVC.DynamicEntityMetadataLibrary.Models;
using DynamicMVC.Shared.Interfaces;
using DynamicMVC.UI.DynamicMVC.Interfaces;
using DynamicMVC.UI.DynamicMVC.ViewModels;
using DynamicMVC.UI.DynamicMVC.ViewModels.DynamicControls;
using DynamicMVC.UI.DynamicMVC.ViewModels.DynamicPropertyViewModels;
using WebGrease.Css.Extensions;

#pragma warning disable 1591

namespace DynamicMVC.UI.DynamicMVC.ViewModelBuilders
{
    public class DynamicCreateViewModelBuilder : IDynamicCreateViewModelBuilder
    {
        private readonly IRequestManager _requestManager;
        private readonly IPropertyFilterManager _propertyFilterManager;
        private readonly IDynamicEditorModelBuilder[] _dynamicEditorModelBuilders;
        private readonly IDynamicPropertyViewModelBuilder _dynamicPropertyViewModelBuilder;

        public DynamicCreateViewModelBuilder(IRequestManager requestManager, IPropertyFilterManager propertyFilterManager, IDynamicEditorModelBuilder[] dynamicEditorModelMaterializerses, IDynamicPropertyViewModelBuilder dynamicPropertyViewModelBuilder)
        {
            _requestManager = requestManager;
            _propertyFilterManager = propertyFilterManager;
            _dynamicEditorModelBuilders = dynamicEditorModelMaterializerses;
            _dynamicPropertyViewModelBuilder = dynamicPropertyViewModelBuilder;
        }

        public DynamicCreateViewModel Build(DynamicEntityMetadata dynamicEntityMetadata, dynamic createModel, string returnUrl)
        {
            var dynamicCreateViewModel = new DynamicCreateViewModel();
            dynamicCreateViewModel.Header = dynamicEntityMetadata.CreateHeader;
            dynamicCreateViewModel.TypeName = dynamicEntityMetadata.TypeName;
            dynamicCreateViewModel.ReturnUrl = returnUrl;
            dynamicCreateViewModel.Item = createModel;
            dynamicCreateViewModel.DynamicUIMethods = dynamicEntityMetadata.GetDynamicMethods(TemplateTypeEnum.Create).ToList();
            foreach (var dynamicPropertyEditorViewModel in GetDynamicPropertyViewModels(dynamicEntityMetadata, createModel))
            {
                var dynamicEditorViewModel = new DynamicEditorViewModel();
                dynamicEditorViewModel.ViewModelPropertyName = dynamicPropertyEditorViewModel.ViewModelPropertyName;
                dynamicEditorViewModel.DynamicEditorName = dynamicPropertyEditorViewModel.DynamicEditorName;
                dynamicEditorViewModel.DynamicPropertyEditorViewModel = dynamicPropertyEditorViewModel;
                dynamicCreateViewModel.DynamicEditorViewModels.Add(dynamicEditorViewModel);
            }

            return dynamicCreateViewModel;
        }

        /// <summary>
        /// Get properties to be scaffolded for this view.  If view properties are passed in through the request, filter by them.
        /// </summary>
        /// <param name="dynamicEntityMetadata"></param>
        /// <returns></returns>
        public IEnumerable<DynamicPropertyMetadata> GetViewProperties(DynamicEntityMetadata dynamicEntityMetadata)
        {
            var dynamicPropertyMetadatas = dynamicEntityMetadata.ScaffoldCreateProperties;
            var viewProperties = _requestManager.ViewProperties();
            if (!string.IsNullOrWhiteSpace(viewProperties))
                dynamicPropertyMetadatas = _propertyFilterManager.FilterAndOrderProperties(dynamicPropertyMetadatas,viewProperties).ToList();
            
            return dynamicPropertyMetadatas;
        }

        private List<DynamicPropertyEditorViewModel> _dynamicPropertyViewModels;
        public IEnumerable<DynamicPropertyEditorViewModel> GetDynamicPropertyViewModels(DynamicEntityMetadata dynamicEntityMetadata, dynamic item)
        {
            if (_dynamicPropertyViewModels == null)
            {
                _dynamicPropertyViewModels = new List<DynamicPropertyEditorViewModel>();
                var viewProperties = GetViewProperties(dynamicEntityMetadata);
                viewProperties = viewProperties.Where(x => x.IsSimple).ToList(); //this view only shows simple properties
                foreach (var dynamicProperty in viewProperties)
                {
                    var dynamicPropertyViewModel =_dynamicPropertyViewModelBuilder.BuildDynamicPropertyEditorViewModelForCreate(dynamicProperty);
                    if (dynamicPropertyViewModel != null)
                    {
                        //create model for editor if applicable
                        IDynamicEditorModelBuilder dynamicEditorModelMaterializer = _dynamicEditorModelBuilders.SingleOrDefault(x => x.DynamicEditorName() == dynamicPropertyViewModel.DynamicEditorName);
                        if (dynamicEditorModelMaterializer != null)
                            dynamicEditorModelMaterializer.Build(dynamicProperty, dynamicPropertyViewModel, item);

                        _dynamicPropertyViewModels.Add(dynamicPropertyViewModel); 
                    }
                }
            }
            return _dynamicPropertyViewModels;
        }
    }
}
