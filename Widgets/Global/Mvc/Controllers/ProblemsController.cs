using _Core.Mvc.Controllers;
using Global.Mvc.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules;
using Telerik.Sitefinity.Mvc;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.UI;

namespace Global.Mvc.Controllers
{
    [EnhanceViewEnginesAttribute]
    [ControllerToolboxItem(Name = "Global_Problems_MVC", Title = "Problems", SectionName = "Global")]
    public class ProblemsController : _BaseController
    {
        #region Properties

        /// <summary>
        /// Gets the Template widget model.
        ///</summary>
        /// <value>
        /// The model.
        /// </value>
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public new ProblemsModel Model
        {
            get
            {
                if (this._model == null)
                    this._model = ControllerModelFactory.GetModel<ProblemsModel>
                        (this.GetType());

                return this._model as ProblemsModel;
            }
        }

        #endregion
    }
}

