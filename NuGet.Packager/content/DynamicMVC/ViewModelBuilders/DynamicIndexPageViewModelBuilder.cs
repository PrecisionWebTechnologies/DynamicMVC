using System;
using System.Collections.Generic;
using System.Linq;
using DynamicMVC.DynamicEntityMetadataLibrary.Models;
using DynamicMVC.Shared.Interfaces;
using DynamicMVC.UI.DynamicMVC.Extensions;
using DynamicMVC.UI.DynamicMVC.Interfaces;
using DynamicMVC.UI.DynamicMVC.ViewModels;
using DynamicMVC.UI.DynamicMVC.ViewModels.DynamicControls;
using DynamicMVC.UI.DynamicMVC.ViewModels.DynamicPropertyViewModels;

#pragma warning disable 1591

namespace DynamicMVC.UI.DynamicMVC.ViewModelBuilders
{
    public class DynamicIndexPageViewModelBuilder : IDynamicIndexPageViewModelBuilder
    {
        private readonly IRequestManager _requestManager;
        private readonly IReturnUrlManager _returnUrlManager;
        private readonly IDynamicFilterManager _dynamicFilterManager;
        private readonly IPropertyFilterManager _propertyFilterManager;
        private readonly IDynamicDisplayPartialModelBuilder[] _dynamicDisplayPartialModelBuilders;
        private readonly IPagingManager _pagingManager;
        private readonly IDynamicPropertyViewModelBuilder _dynamicPropertyViewModelBuilder;

        public DynamicIndexPageViewModelBuilder(IRequestManager requestManager, IReturnUrlManager returnUrlManager, IDynamicFilterManager dynamicFilterManager, IPropertyFilterManager propertyFilterManager, IDynamicDisplayPartialModelBuilder[] dynamicDisplayPartialModelBuilders, IPagingManager pagingManager, IDynamicPropertyViewModelBuilder dynamicPropertyViewModelBuilder)
        {
            _requestManager = requestManager;
            _returnUrlManager = returnUrlManager;
            _dynamicFilterManager = dynamicFilterManager;
            _propertyFilterManager = propertyFilterManager;
            _dynamicDisplayPartialModelBuilders = dynamicDisplayPartialModelBuilders;
            _pagingManager = pagingManager;
            _dynamicPropertyViewModelBuilder = dynamicPropertyViewModelBuilder;
        }

        public DynamicIndexPageViewModel Build(DynamicEntityMetadata dynamicEntityMetadata)
        {
            var dynamicIndexPageViewModel = new DynamicIndexPageViewModel();

            var routeValuesDictionary = _requestManager.QueryStringDictionary;
            _requestManager.CorrectQuerystringTypes(dynamicEntityMetadata);
            var dynamicFilterViewModels = _dynamicFilterManager.GetFilterPropertyViewModels(dynamicEntityMetadata, routeValuesDictionary);
            var filters = dynamicFilterViewModels.Select(x => (Func<IQueryable, IQueryable>)x.FilterModel.Filter).ToList();
            _pagingManager.SetFilters(dynamicEntityMetadata, filters);
            _pagingManager.ValidatePagingParameters(routeValuesDictionary);
            var models = _pagingManager.GetItems(dynamicEntityMetadata, routeValuesDictionary);

            
            dynamicIndexPageViewModel.RouteValueDictionaryWrapper = routeValuesDictionary;
            dynamicIndexPageViewModel.RouteValueDictionaryWrapper.SetValue("ReturnUrl", _returnUrlManager.GetReturnUrl("Index", dynamicEntityMetadata.TypeName, routeValuesDictionary, true));

            dynamicIndexPageViewModel.NextClassName = _pagingManager.NextClassName(routeValuesDictionary);
            dynamicIndexPageViewModel.PreviousClassName = _pagingManager.PreviousClassName(routeValuesDictionary);
            dynamicIndexPageViewModel.PagingMessage = _pagingManager.PagingMessage(routeValuesDictionary);

            dynamicIndexPageViewModel.ShowCreate = dynamicEntityMetadata.ShowCreate;
            dynamicIndexPageViewModel.ShowEdit = dynamicEntityMetadata.ShowEdit;
            dynamicIndexPageViewModel.ShowDelete = dynamicEntityMetadata.ShowDelete;
            dynamicIndexPageViewModel.ShowDetails = dynamicEntityMetadata.ShowDetails;

            dynamicIndexPageViewModel.TypeName = dynamicEntityMetadata.TypeName;

            dynamicIndexPageViewModel.DynamicPropertyIndexViewModels = GetDynamicPropertyViewModels(dynamicEntityMetadata, null).ToList();

            var defaultpropertyName = dynamicEntityMetadata.DefaultProperty.PropertyName;
            var defaultDynamicProperty = dynamicIndexPageViewModel.DynamicPropertyIndexViewModels.SingleOrDefault(x => x.PropertyName == defaultpropertyName);
            if (defaultDynamicProperty != null)
            {
                dynamicIndexPageViewModel.AllowSort = true;
                dynamicIndexPageViewModel.DefaultPropertyName = defaultpropertyName;
                dynamicIndexPageViewModel.DefaultPropertySortExpression = defaultDynamicProperty.SortExpression;
            }
           
            //table header items
            foreach (var dynamicPropertyIndexViewModel in dynamicIndexPageViewModel.DynamicPropertyIndexViewModels)
            {
                var dynamicTableHeaderViewModel = new DynamicTableHeaderViewModel(dynamicPropertyIndexViewModel, routeValuesDictionary, dynamicEntityMetadata.TypeName);
                dynamicIndexPageViewModel.DynamicTableHeaderViewModels.Add(dynamicTableHeaderViewModel);
            }

            //new view model items
            foreach (var model in models)
            {
                var itemViewModel = BuildItemViewModel(dynamicEntityMetadata, routeValuesDictionary, model);
                dynamicIndexPageViewModel.DynamicIndexItemViewModels.Add(itemViewModel);
                var mobileItemViewModel = BuildMobileItemViewModel(dynamicEntityMetadata, routeValuesDictionary, model);
                dynamicIndexPageViewModel.DynamicIndexMobileItemViewModels.Add(mobileItemViewModel);
            }
           
            return dynamicIndexPageViewModel;
        }


        public DynamicIndexItemViewModel BuildItemViewModel(DynamicEntityMetadata dynamicEntityMetadata, RouteValueDictionaryWrapper routeValueDictionaryWrapper, dynamic item)
        {
            var dynamicIndexItemViewModel = new DynamicIndexItemViewModel();
            dynamicIndexItemViewModel.ShowDelete = dynamicEntityMetadata.ShowDelete;
            dynamicIndexItemViewModel.ShowEdit = dynamicEntityMetadata.ShowEdit;
            dynamicIndexItemViewModel.ShowDetails = dynamicEntityMetadata.ShowDetails;
            dynamicIndexItemViewModel.Item = item;
            dynamicIndexItemViewModel.TypeName = dynamicEntityMetadata.TypeName;
            var rv = routeValueDictionaryWrapper.Clone();
            rv.SetValue("Id", dynamicEntityMetadata.KeyProperty.GetValueFunction(item).ToString());
            dynamicIndexItemViewModel.RouteValueDictionaryWrapper = rv;

            IEnumerable<DynamicPropertyIndexViewModel> properties = GetDynamicPropertyViewModels(dynamicEntityMetadata, item);
            foreach (var property in properties)
            {
                dynamicIndexItemViewModel.DynamicPropertyIndexViewModels.Add(property);
            }
            return dynamicIndexItemViewModel;
        }

        public DynamicIndexMobileItemViewModel BuildMobileItemViewModel(DynamicEntityMetadata dynamicEntityMetadata, RouteValueDictionaryWrapper routeValueDictionaryWrapper, dynamic item)
        {
            var dynamicIndexMobileItemViewModel = new DynamicIndexMobileItemViewModel();
            dynamicIndexMobileItemViewModel.ShowDelete = dynamicEntityMetadata.ShowDelete;
            dynamicIndexMobileItemViewModel.ShowEdit = dynamicEntityMetadata.ShowEdit;
            dynamicIndexMobileItemViewModel.ShowDetails = dynamicEntityMetadata.ShowDetails;
            dynamicIndexMobileItemViewModel.Item = item;

            IEnumerable<DynamicPropertyIndexViewModel> properties = GetDynamicPropertyViewModels(dynamicEntityMetadata, item);
            var defaultpropertyName = dynamicEntityMetadata.DefaultProperty.PropertyName;
            var defaultDynamicProperty = properties.SingleOrDefault(x => x.PropertyName == defaultpropertyName);
            var entityName = dynamicEntityMetadata.TypeName;

            foreach (var dynamicPropertyIndexViewModel in properties.Where(x => x.PropertyName != defaultpropertyName).ToList())
            {
                dynamicIndexMobileItemViewModel.DynamicPropertyIndexViewModels.Add(dynamicPropertyIndexViewModel);
            }

            if (defaultDynamicProperty != null)
            {
                dynamicIndexMobileItemViewModel.DefaultDynamicPropertyViewModel = defaultDynamicProperty;
            }

            dynamicIndexMobileItemViewModel.EntityName = entityName;

            dynamicIndexMobileItemViewModel.TypeName = dynamicEntityMetadata.TypeName;
            var rv = routeValueDictionaryWrapper.Clone();
            rv.SetValue("Id", dynamicEntityMetadata.KeyProperty.GetValueFunction(item).ToString());
            dynamicIndexMobileItemViewModel.RouteValueDictionaryWrapper = rv;
            return dynamicIndexMobileItemViewModel;
        }

        public IEnumerable<DynamicPropertyMetadata> GetViewProperties(DynamicEntityMetadata dynamicEntityMetadata)
        {
            var dynamicPropertyMetadatas = dynamicEntityMetadata.ScaffoldIndexProperties;
            var viewProperties = _requestManager.ViewProperties();
            if (!string.IsNullOrWhiteSpace(viewProperties))
                dynamicPropertyMetadatas = _propertyFilterManager.FilterAndOrderProperties(dynamicPropertyMetadatas, viewProperties).ToList();

            return dynamicPropertyMetadatas;
        }

        /// <summary>
        /// Gets DynamicPropertyIndexViewModel for dynamicentity.  Will get PartialView model objects for each property if item is specified.
        /// </summary>
        /// <param name="dynamicEntityMetadata"></param>
        /// <param name="item">Specify item to use to create PartialView model objects for each property</param>
        /// <returns></returns>
        public IEnumerable<DynamicPropertyIndexViewModel> GetDynamicPropertyViewModels(DynamicEntityMetadata dynamicEntityMetadata, dynamic item)
        {
            var dynamicPropertyIndexViewModels = new List<DynamicPropertyIndexViewModel>();
            var viewProperties = GetViewProperties(dynamicEntityMetadata).Where(x => x.IsDynamicCollection || x.IsSimple || x is DynamicForiegnKeyPropertyMetadata).ToList();

            foreach (var dynamicProperty in viewProperties)
            {
                var dynamicPropertyIndexViewModel = _dynamicPropertyViewModelBuilder.BuildDynamicPropertyIndexViewModel(dynamicProperty);
                if (dynamicPropertyIndexViewModel != null)
                {
                    if (item != null)
                    {
                        //create model for PartialViewName if applicable (Strategy Pattern)
                        IDynamicDisplayPartialModelBuilder dynamicDisplayPartialModelBuilder = _dynamicDisplayPartialModelBuilders.SingleOrDefault(x => x.DynamicDisplayPartialName() == dynamicPropertyIndexViewModel.PartialViewName);
                        if (dynamicDisplayPartialModelBuilder != null)
                            dynamicDisplayPartialModelBuilder.Build(dynamicProperty, dynamicPropertyIndexViewModel, item);
                    }

                    dynamicPropertyIndexViewModels.Add(dynamicPropertyIndexViewModel);
                }
            }

            return dynamicPropertyIndexViewModels;
        }

       
    }
}
