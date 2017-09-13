    using _Core.Mvc.Controllers;
    using ProductsWidgets.Mvc.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using System.Web.Mvc;
    using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
    using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes;
    using Telerik.Sitefinity.Localization;
    using Telerik.Sitefinity.Modules.Pages.Configuration;
    using Telerik.Sitefinity.Mvc;
    using Telerik.Sitefinity.Services;
    using Telerik.Sitefinity.Web.UI;

    namespace ProductsWidgets.Mvc.Controllers
    {
    [EnhanceViewEnginesAttribute]
    [ControllerToolboxItem(Name = "Products_ProductSearch_MVC", Title = "Product Search", SectionName = "Products")]
    public class ProductSearchController : _BaseController
    {
    #region Properties

        /// <summary>
        /// Gets the Template widget model.
        ///</summary>
        /// <value>
        /// The model.
        /// </value>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public new ProductSearchModel Model

    {
    get
    {
    if (this._model == null)
    this._model = ControllerModelFactory.GetModel<ProductSearchModel>
        (this.GetType());

        return this._model as ProductSearchModel;
        }
        }

        #endregion
        }
        }
