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
    [ControllerToolboxItem(Name = "Global_PageHeader_MVC", Title = "Page Header", SectionName = "Global")]
    public class PageHeaderController : _BaseController
    {
        #region Properties

        /// <summary>
        /// Gets the Template widget model.
        ///</summary>
        /// <value>
        /// The model.
        /// </value>
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public new PageHeaderModel Model
        {
            get
            {
                if (this._model == null)
                    this._model = ControllerModelFactory.GetModel<PageHeaderModel>
                        (this.GetType());

                return this._model as PageHeaderModel;
            }
        }

        #endregion
    }
}

