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
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Model;
using _Core.Extensions;
using _Core;
using System.Collections;

namespace SitefinityWebApp.Api.Products
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ProductsController : ApiController
    {
        private ICollection<dynamic> ListOfProducts
        {
            get
            {
                //Check if items are in Cache
                //if (HttpContext.Current.Cache["PRODUCTS"] != null)
                //    return (HttpContext.Current.Cache["PRODUCTS"] as IList<dynamic>);

                //If Cache is empty populate the cache first
                ICollection<dynamic> listOfProducts = new List<dynamic>();
                var products = DynamicContentOps.GetGenericItems(ModuleTypes.ProductsItemName);

                foreach (var product in products)
                {
                    dynamic item = new ExpandoObject();
                    item.Id = product.Id.ToString();
                    item.Title = HttpUtility.HtmlDecode(product.GetValue<Lstring>("DisplayTitle").ToString());
                    item.Applications = DynamicContentOps.GetGenericRelatedItems(product, "Applications").ToList().GetListOfIds();
                    item.BodySizes = DynamicContentOps.GetGenericRelatedItems(product, "BodySizes").ToList().GetListOfIds();
                    item.Connections = DynamicContentOps.GetGenericRelatedItems(product, "Connections").ToList().GetListOfIds();
                    item.Materials = DynamicContentOps.GetGenericRelatedItems(product, "Materials").ToList().GetListOfIds();
                    item.WorkingPressures = DynamicContentOps.GetGenericRelatedItems(product, "WorkingPressures").ToList().GetListOfIds();

                    listOfProducts.Add(item);

                }

                //Order products by title
                listOfProducts = listOfProducts.OrderBy(pred => pred.Title).ToList();

                //Add the list to Cache
                HttpContext.Current.Cache["PRODUCTS"] = listOfProducts;
                return listOfProducts;
            }
        }

        [HttpGet]
        public HttpResponseMessage Products()
        {
            ProductsModel productsModel = new ProductsModel();
            try
            {
                ICollection<dynamic> products = new List<dynamic>();
                foreach (dynamic product in ListOfProducts)
                {
                    if (!string.IsNullOrEmpty(HttpContext.Current.Request["applications"]) &&
                            HttpContext.Current.Request["applications"].ToGuidList().Intersect((IList<Guid>)product.Applications).Count() == 0
                        ) continue;

                    if (!string.IsNullOrEmpty(HttpContext.Current.Request["bodySizes"]) &&
                            HttpContext.Current.Request["bodySizes"].ToGuidList().Intersect((IList<Guid>)product.BodySizes).Count() == 0
                        ) continue;

                    if (!string.IsNullOrEmpty(HttpContext.Current.Request["connections"]) &&
                            HttpContext.Current.Request["connections"].ToGuidList().Intersect((IList<Guid>)product.Connections).Count() == 0
                        ) continue;

                    if (!string.IsNullOrEmpty(HttpContext.Current.Request["workingPressures"]) &&
                            HttpContext.Current.Request["workingPressures"].ToGuidList().Intersect((IList<Guid>)product.WorkingPressures).Count() == 0
                        ) continue;

                    if (!string.IsNullOrEmpty(HttpContext.Current.Request["materials"]) &&
                            HttpContext.Current.Request["materials"].ToGuidList().Intersect((IList<Guid>)product.Materials).Count() == 0
                        ) continue;

                    products.Add(product);
                }


                int pageNo = !string.IsNullOrEmpty(HttpContext.Current.Request["pageNo"]) ? int.Parse(HttpContext.Current.Request["pageNo"]) : 0;
                int noOfRecordsPerPage = int.Parse(ConfigurationManager.AppSettings["ProductsResultsPerPage"]);

                productsModel.Products = products.Take(noOfRecordsPerPage * (pageNo + 1)).ToList();
                productsModel.HasMoreItems = products.Count > (pageNo + 1) * noOfRecordsPerPage;

                return Request.CreateResponse(HttpStatusCode.OK, productsModel);

            }
            catch (Exception ex)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError) { Content = new StringContent(ex.ToString()) });
            }
        }

    }
}