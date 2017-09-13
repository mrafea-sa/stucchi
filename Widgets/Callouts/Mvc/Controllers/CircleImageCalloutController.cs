    using _Core.Mvc.Controllers;
    using CalloutsWidgets.Mvc.Models;
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

    namespace CalloutsWidgets.Mvc.Controllers
    {
    [EnhanceViewEnginesAttribute]
    [ControllerToolboxItem(Name = "Callouts_CircleImageCallout_MVC", Title = "Circle Image Callout", SectionName = "Callouts")]
    public class CircleImageCalloutController : _BaseController
    {
    #region Properties

        /// <summary>
        /// Gets the Template widget model.
        ///</summary>
        /// <value>
        /// The model.
        /// </value>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public new CircleImageCalloutModel Model

    {
    get
    {
    if (this._model == null)
    this._model = ControllerModelFactory.GetModel<CircleImageCalloutModel>
        (this.GetType());

        return this._model as CircleImageCalloutModel;
        }
        }

        #endregion
        }
        }

