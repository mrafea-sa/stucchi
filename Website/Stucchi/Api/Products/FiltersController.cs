using _Core;
using _Core.Extensions;
using _Core.Helpers;
using _Core.Ops;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web.Http;
using System.Web.Http.Cors;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Model;

namespace SitefinityWebApp.Api.Products
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class FiltersController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage Filters()
        {
            try
            {
                FilterModel filterModel = new FilterModel();
                filterModel.Applications = ProductFiltersOpsMgmt.GetListOfFilters(ModuleTypes.ProductfiltersApplicationName);
                filterModel.BodySizes = ProductFiltersOpsMgmt.GetListOfFilters(ModuleTypes.ProductfiltersBodySizeName);
                filterModel.Connections = ProductFiltersOpsMgmt.GetListOfFilters(ModuleTypes.ProductfiltersConnectionName);
                filterModel.Materials = ProductFiltersOpsMgmt.GetListOfFilters(ModuleTypes.ProductfiltersMaterialName);
                filterModel.WorkingPressures = ProductFiltersOpsMgmt.GetListOfFilters(ModuleTypes.ProductfiltersWorkingPressureName);

                return Request.CreateResponse(HttpStatusCode.OK, filterModel);
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError) { Content = new StringContent(ex.ToString()) });
            }
        }

    }
}