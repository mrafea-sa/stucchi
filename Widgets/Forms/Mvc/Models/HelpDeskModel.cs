using _Core;
using _Core.Extensions;
using _Core.Mvc.Models;
using _Core.Helpers;
using FormsWidgets.Mvc.Models;
using System;
using System.Dynamic;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.DynamicModules.Model;
using SfImage = Telerik.Sitefinity.Libraries.Model.Image;


namespace FormsWidgets.Mvc.Models
{
    public class HelpDeskModel : _BaseModel
    {
        /// <inheritdoc />
        public string Title { get; set; }

        /// <inheritdoc />
        public override _BaseViewModel GetViewModel()
        {
            var viewModel = new HelpDeskViewModel()
            {
                Title = this.Title,
            };

            return viewModel;
        }

        /// <inheritdoc />
        public override bool IsEmpty()
        {
            return (string.IsNullOrEmpty(this.Title));
        }

        #region Private Methods

      

        #endregion
    }
}