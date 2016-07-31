using System.Collections.Generic;
using System.Linq;
using DynamicMVC.Annotations.Enums;
using DynamicMVC.DynamicEntityMetadataLibrary.Models;
using DynamicMVC.Shared.Interfaces;
using DynamicMVC.UI.DynamicMVC.Interfaces;
using DynamicMVC.UI.DynamicMVC.ViewModels.DynamicControls;
using DynamicMVC.UI.DynamicMVC.ViewModels.DynamicPropertyViewModels;
using WebGrease.Css.Extensions;
using DynamicDetailsViewModel = DynamicMVC.UI.DynamicMVC.ViewModels.DynamicDetailsViewModel;

namespace DynamicMVC.UI.DynamicMVC.ViewModelBuilders
{
    public class DynamicDetailsViewModelBuilder : IDynamicDetailsViewModelBuilder
    {
        private readonly IRequestManager _requestManager;
        private readonly IPropertyFilterManager _propertyFilterManager;
        private readonly IDynamicEditorModelBuilder[] _dynamicEditorModelBuilders;
        private readonly IDynamicPropertyViewModelBuilder _dynamicPropertyViewModelBuilder;

        public DynamicDetailsViewModelBuilder(IRequestManager requestManager, IPropertyFilterManager propertyFilterManager, IDynamicEditorModelBuilder[] dynamicEditorModelMaterializerses, IDynamicPropertyViewModelBuilder dynamicPropertyViewModelBuilder)
        {
            _requestManager = requestManager;
            _propertyFilterManager = propertyFilterManager;
            _dynamicEditorModelBuilders = dynamicEditorModelMaterializerses;
            _dynamicPropertyViewModelBuilder = dynamicPropertyViewModelBuilder;
        }

        public DynamicDetailsViewModel Build(DynamicEntityMetadata dynamicEntityMetadata, dynamic detailModel)
        {
            var dynamicDetailsViewModel = new DynamicDetailsViewModel();
            dynamicDetailsViewModel.TypeName = dynamicEntityMetadata.TypeName();
            dynamicDetailsViewModel.Header = dynamicEntityMetadata.DetailsHeader();
            dynamicDetailsViewModel.Item = detailModel;
            dynamicDetailsViewModel.DynamicUIMethods = dynamicEntityMetadata.GetDynamicMethods(TemplateTypeEnum.Details).ToList();
            foreach (var dynamicPropertyEditorViewModel in GetDynamicPropertyViewModels(dynamicEntityMetadata, detailModel))
            {
                var dynamicEditorViewModel = new DynamicEditorViewModel();
                dynamicEditorViewModel.ViewModelPropertyName = dynamicPropertyEditorViewModel.ViewModelPropertyName;
                dynamicEditorViewModel.DynamicEditorName = dynamicPropertyEditorViewModel.DynamicEditorName;
                dynamicEditorViewModel.DynamicPropertyEditorViewModel = dynamicPropertyEditorViewModel;
                dynamicDetailsViewModel.DynamicEditorViewModels.Add(dynamicEditorViewModel);
            }

           
            return dynamicDetailsViewModel;
        }

        /// <summary>
        /// Get properties to be scaffolded for this view.  If view properties are passed in through the request, filter by them.
        /// </summary>
        /// <param name="dynamicEntityMetadata"></param>
        /// <returns></returns>
        public IEnumerable<DynamicPropertyMetadata> GetViewProperties(DynamicEntityMetadata dynamicEntityMetadata)
        {
            var dynamicPropertyMetadatas = dynamicEntityMetadata.ScaffoldDetailsProperties();
            var viewProperties = _requestManager.ViewProperties();
            if (!string.IsNullOrWhiteSpace(viewProperties))
                dynamicPropertyMetadatas = _propertyFilterManager.FilterAndOrderProperties(dynamicPropertyMetadatas, viewProperties).ToList();

            return dynamicPropertyMetadatas;
        }

        private List<DynamicPropertyEditorViewModel> _dynamicPropertyViewModels;
        public IEnumerable<DynamicPropertyEditorViewModel> GetDynamicPropertyViewModels(DynamicEntityMetadata dynamicEntityMetadata, dynamic item)
        {
            if (_dynamicPropertyViewModels == null)
            {
                _dynamicPropertyViewModels = new List<DynamicPropertyEditorViewModel>();
                var viewProperties = GetViewProperties(dynamicEntityMetadata);
                viewProperties = viewProperties.Where(x => x.IsSimple()|| x.IsDynamicCollection()).ToList(); //this view only shows simple properties
                foreach (var dynamicPropertyMetadata in viewProperties)
                {
                    var dynamicPropertyEditorViewModel = _dynamicPropertyViewModelBuilder.BuildDynamicPropertyEditorViewModelForDetails(dynamicPropertyMetadata);
                    if (dynamicPropertyEditorViewModel != null)
                    {
                        //create model for editor if applicable
                        IDynamicEditorModelBuilder dynamicEditorModelMaterializer = _dynamicEditorModelBuilders.SingleOrDefault(x => x.DynamicEditorName() == dynamicPropertyEditorViewModel.DynamicEditorName);
                        if (dynamicEditorModelMaterializer != null)
                            dynamicEditorModelMaterializer.Build(dynamicPropertyMetadata, dynamicPropertyEditorViewModel, item);

                        _dynamicPropertyViewModels.Add(dynamicPropertyEditorViewModel);
                    }
                }
            }
            return _dynamicPropertyViewModels;
        }
    
    }
}
