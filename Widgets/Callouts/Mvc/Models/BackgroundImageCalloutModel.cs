using _Core;
using _Core.Extensions;
using _Core.Mvc.Models;
using _Core.Helpers;
using CalloutsWidgets.Mvc.Models;
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


namespace CalloutsWidgets.Mvc.Models
{
    public class BackgroundImageCalloutModel : _BaseModel
    {
        /// <inheritdoc />
        public string Title { get; set; }
        /// <inheritdoc />
        public string MainContent { get; set; }
        /// <inheritdoc />
        public string AlignmentOptions { get; set; }

        /// <inheritdoc />
        public Guid BackgroundImage { get; set; }


        /// <inheritdoc />
        public override _BaseViewModel GetViewModel()
        {
            var viewModel = new BackgroundImageCalloutViewModel()
            {
                Title = this.Title,
                MainContent = this.MainContent,
                AlignmentOptions = this.AlignmentOptions, 
                BackgroundImageMediaAsset = DynamicContentExtractor.GetMediaModel(this.BackgroundImage),
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
