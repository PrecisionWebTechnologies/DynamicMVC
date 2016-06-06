using System.Collections.Generic;
using System.Linq;
using DynamicMVC.Annotations.Enums;
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
    public class DynamicEditViewModelBuilder : IDynamicEditViewModelBuilder
    {
        private readonly IRequestManager _requestManager;
        private readonly IPropertyFilterManager _propertyFilterManager;
        private readonly IDynamicEditorModelBuilder[] _dynamicEditorModelBuilders;
        private readonly IDynamicPropertyViewModelBuilder _dynamicPropertyViewModelBuilder;

        public DynamicEditViewModelBuilder(IRequestManager requestManager, IPropertyFilterManager propertyFilterManager, IDynamicEditorModelBuilder[] dynamicEditorModelMaterializerses, IDynamicPropertyViewModelBuilder dynamicPropertyViewModelBuilder)
        {
            _requestManager = requestManager;
            _propertyFilterManager = propertyFilterManager;
            _dynamicEditorModelBuilders = dynamicEditorModelMaterializerses;
            _dynamicPropertyViewModelBuilder = dynamicPropertyViewModelBuilder;
        }

        public DynamicEditViewModel Build(DynamicEntityMetadata dynamicEntityMetadata, dynamic editModel, string returnUrl)
        {
            var dynamicEditViewModel = new DynamicEditViewModel();
            dynamicEditViewModel.Header = dynamicEntityMetadata.EditHeader;
            dynamicEditViewModel.TypeName = dynamicEntityMetadata.TypeName;
            dynamicEditViewModel.ReturnUrl = returnUrl;
            dynamicEditViewModel.Item = editModel;
            dynamicEditViewModel.DynamicUIMethods = dynamicEntityMetadata.GetDynamicMethods(TemplateTypeEnum.Edit).ToList();

            foreach (var dynamicPropertyViewModel in GetDynamicPropertyViewModels(dynamicEntityMetadata, editModel))
            {
                var dynamicEditorViewModel = new DynamicEditorViewModel();
                dynamicEditorViewModel.ViewModelPropertyName = dynamicPropertyViewModel.ViewModelPropertyName;
                dynamicEditorViewModel.DynamicEditorName = dynamicPropertyViewModel.DynamicEditorName;
                dynamicEditorViewModel.DynamicPropertyEditorViewModel = dynamicPropertyViewModel;
                dynamicEditViewModel.DynamicEditorViewModels.Add(dynamicEditorViewModel);
            }

           return dynamicEditViewModel;
        }

        private List<DynamicPropertyEditorViewModel> _dynamicPropertyEditorViewModels;

        public IEnumerable<DynamicPropertyEditorViewModel> GetDynamicPropertyViewModels(DynamicEntityMetadata dynamicEntityMetadata, dynamic item)
        {
            if (_dynamicPropertyEditorViewModels == null)
            {
                _dynamicPropertyEditorViewModels = new List<DynamicPropertyEditorViewModel>();
                var viewProperties = GetViewProperties(dynamicEntityMetadata);
                viewProperties = viewProperties.Where(x => x.IsSimple).ToList(); //this view only shows simple properties
                foreach (var dynamicPropertyMetadata in viewProperties)
                {
                    var dynamicPropertyEditorViewModel = _dynamicPropertyViewModelBuilder.BuildDynamicPropertyEditorViewModelForEdit(dynamicPropertyMetadata);
                    if (dynamicPropertyEditorViewModel != null)
                    {
                        //create model for editor if applicable
                        IDynamicEditorModelBuilder dynamicEditorModelMaterializer = _dynamicEditorModelBuilders.SingleOrDefault(x => x.DynamicEditorName() == dynamicPropertyEditorViewModel.DynamicEditorName);
                        if (dynamicEditorModelMaterializer != null)
                            dynamicEditorModelMaterializer.Build(dynamicPropertyMetadata, dynamicPropertyEditorViewModel, item);

                        _dynamicPropertyEditorViewModels.Add(dynamicPropertyEditorViewModel);
                    }
                }
            }
            return _dynamicPropertyEditorViewModels;
        }

        /// <summary>
        /// Get properties to be scaffolded for this view.  If view properties are passed in through the request, filter by them.
        /// </summary>
        /// <param name="dynamicEntityMetadata"></param>
        /// <returns></returns>
        public IEnumerable<DynamicPropertyMetadata> GetViewProperties(DynamicEntityMetadata dynamicEntityMetadata)
        {
            var dynamicPropertyMetadatas = dynamicEntityMetadata.ScaffoldEditProperties;
            var viewProperties = _requestManager.ViewProperties();
            if (!string.IsNullOrWhiteSpace(viewProperties))
                dynamicPropertyMetadatas = _propertyFilterManager.FilterAndOrderProperties(dynamicPropertyMetadatas, viewProperties).ToList();

            return dynamicPropertyMetadatas;
        }
    }
}
