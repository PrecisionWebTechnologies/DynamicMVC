using System;
using System.Linq;
using System.Web.Mvc;
using DynamicMVC.Data.Interfaces;
using DynamicMVC.DynamicEntityMetadataLibrary.Models;
using DynamicMVC.UI.DynamicMVC.Interfaces;
// ReSharper disable InconsistentNaming

namespace DynamicMVC.UI.DynamicMVC
{
    public class DynamicControllerBase : Controller
    {
        protected readonly IDynamicRepository _dynamicRepository;
        protected readonly IRequestManager _requestManager;
        protected readonly IDynamicEntitySearchManager _dynamicEntitySearchManager;
        protected readonly IReturnUrlManager _returnUrlManager;
        protected readonly IDynamicIndexViewModelBuilder _dynamicIndexViewModelBuilder;
        protected readonly IDynamicCreateViewModelBuilder DynamicCreateViewModelBuilder;
        protected readonly IDynamicEditViewModelBuilder DynamicEditViewModelBuilder;
        protected readonly IDynamicDeleteViewModelBuilder _dynamicDeleteViewModelBuilder;
        protected readonly IDynamicDetailsViewModelBuilder DynamicDetailsViewModelBuilder;
        protected readonly IDynamicIndexPageViewModelBuilder DynamicIndexPageViewModelBuilder;

        public DynamicControllerBase(IDynamicRepository dynamicRepository, IRequestManager requestManager, IDynamicEntitySearchManager dynamicEntitySearchManager, IReturnUrlManager returnUrlManager, IDynamicIndexViewModelBuilder dynamicIndexViewModelBuilder, IDynamicCreateViewModelBuilder dynamicCreateViewModelBuilder, IDynamicEditViewModelBuilder dynamicEditViewModelBuilder, IDynamicDeleteViewModelBuilder dynamicDeleteViewModelBuilder, IDynamicDetailsViewModelBuilder dynamicDetailsViewModelBuilder, IDynamicIndexPageViewModelBuilder dynamicIndexPageViewModelBuilder)
        {
            _dynamicRepository = dynamicRepository;
            _requestManager = requestManager;
            _dynamicEntitySearchManager = dynamicEntitySearchManager;
            _returnUrlManager = returnUrlManager;
            _dynamicIndexViewModelBuilder = dynamicIndexViewModelBuilder;
            DynamicCreateViewModelBuilder = dynamicCreateViewModelBuilder;
            DynamicEditViewModelBuilder = dynamicEditViewModelBuilder;
            _dynamicDeleteViewModelBuilder = dynamicDeleteViewModelBuilder;
            DynamicDetailsViewModelBuilder = dynamicDetailsViewModelBuilder;
            DynamicIndexPageViewModelBuilder = dynamicIndexPageViewModelBuilder;
        }

        protected override void Dispose(bool disposing)
        {
            if (_dynamicRepository != null)
                _dynamicRepository.Dispose();
            base.Dispose(disposing);
        }

        public Type EntityType
        {
            get { return DynamicEntityMetadata.EntityTypeFunction()(); }
        }
        public DynamicEntityMetadata DynamicEntityMetadata
        {
            get { return _dynamicEntitySearchManager.DynamicEntityMetadata; }
        }
        public string KeyName
        {
            get { return _dynamicEntitySearchManager.DynamicEntityMetadata.KeyProperty().PropertyName(); }
        }

        public string TypeName
        {
            get { return _dynamicEntitySearchManager.DynamicEntityMetadata.TypeName(); }
        }

        public dynamic ParseKeyType(dynamic id)
        {
            if (id.GetType() == typeof(String[])) //mvc model binder does not bind correctly with the dynamic data type in some cases
                id = ((string[])id)[0];
            return DynamicEntityMetadata.KeyProperty().ParseValue()(id.ToString());
        }

        /// <summary>
        /// this will update any property that may be set through the querystring
        /// </summary>
        /// <param name="model"></param>
        public void UpdateModelAndClearModelState(dynamic model)
        {
            TryUpdateModel(model);
            ModelState.Clear();
        }

        /// <summary>
        /// Redirects to return url or index page
        /// </summary>
        /// <param name="returnUrl">The Url to to redirect to</param>
        /// <param name="model">pass in the create model if the object was just created</param>
        /// <returns></returns>
        public ActionResult ReturnSuccessfulRedirect(string returnUrl, dynamic model = null)
        {
            if (!string.IsNullOrWhiteSpace(returnUrl))
            {
                if (model != null)
                    returnUrl = _returnUrlManager.ReplaceScopeIdentity(returnUrl, DynamicEntityMetadata, model);

                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", TypeName, new { TypeName });
        }

        public void CreateItem(dynamic createModel)
        {
            DynamicMVCContext.DynamicMvcManager.LoadCreateIncludes(DynamicEntityMetadata, createModel, _dynamicRepository, DynamicEntityMetadata.InstanceIncludes().ToArray());
            _dynamicRepository.CreateItem(EntityType, createModel);
        }

        public void EditItem(dynamic editModel)
        {
           _dynamicRepository.SaveChanges();
        }

        public void CorrectQueryStringTypes()
        {
            _requestManager.CorrectQuerystringTypes(DynamicEntityMetadata);
        }
    }
}