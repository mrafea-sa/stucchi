using _Core.Mvc.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UI;

namespace _Core.Mvc.Controllers
{
    public class _BaseController : Controller, ICustomWidgetVisualizationExtended
    {
        #region Private fields and constants

        private string _templateName = "Template";
        protected _BaseModel _model;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the Template widget model.
        /// </summary>
        /// <value>
        /// The model.
        /// </value>
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public virtual _BaseModel Model
        {
            get
            {
                return _model;
            }
        }   

        /// <summary>
        /// Gets or sets the name of the template that widget will be displayed.
        /// </summary>
        /// <value></value>
        public string TemplateName
        {
            get
            {
                return _templateName;
            }

            set
            {
                _templateName = value;
            }
        }

        /// <inheritdoc />
        [Browsable(false)]
        public string EmptyLinkText
        {
            get
            {
                return string.Empty;
            }
        }

        /// <inheritdoc />
        [Browsable(false)]
        public string WidgetCssClass
        {
            get
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Gets the is design mode.
        /// </summary>
        /// <value>The is design mode.</value>
        protected virtual bool IsDesignMode
        {
            get
            {
                return SystemManager.IsDesignMode;
            }
        }

        /// <summary>
        /// Gets a value indicating whether widget is empty.
        /// </summary>
        /// <value>
        ///   <c>true</c> if widget has no image selected; otherwise, <c>false</c>.
        /// </value>
        [Browsable(false)]
        public bool IsEmpty
        {
            get
            {
                return this.Model.IsEmpty();
            }
        }

        #endregion

        #region Actions

        /// <summary>
        /// Renders appropriate list view depending on the <see cref="ListTemplateName" />
        /// </summary>
        /// <param name="page">The page.</param>
        /// <returns>
        /// The <see cref="ActionResult" />.
        /// </returns>
        public virtual ActionResult Index()
        {
            if (this.IsEmpty)
            {
                return new EmptyResult();
            }

            var viewModel = this.Model.GetViewModel();

            return View(this.TemplateName, viewModel);
        }

        public ActionResult ContainerClass()
        {
            return Content(HttpContext.Session["ContainerClass"] != null ? HttpContext.Session["ContainerClass"].ToString() : string.Empty); 
        }

        public ActionResult SectionClass()
        {
            return Content(HttpContext.Session["SectionClass"] != null ? HttpContext.Session["SectionClass"].ToString() : string.Empty);
        }

        /// <inheritDoc/>
        protected override void HandleUnknownAction(string actionName)
        {
            this.Index().ExecuteResult(this.ControllerContext);
        }

        #endregion

    }
}
