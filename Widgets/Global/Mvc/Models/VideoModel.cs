using _Core;
using _Core.Extensions;
using _Core.Mvc.Models;
using _Core.Helpers;
using GlobalWidgets.Mvc.Models;
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


namespace GlobalWidgets.Mvc.Models
{
    public class VideoModel : _BaseModel
    {
        /// <inheritdoc />
        public string Title { get; set; }
        /// <inheritdoc />
        public string VideoID { get; set; }
        /// <inheritdoc />
        public Guid VideoThumb { get; set; }

        /// <inheritdoc />
        public string Description { get; set; }


        /// <inheritdoc />
        public override _BaseViewModel GetViewModel()
        {
            var viewModel = new VideoViewModel()
            {
                Title = this.Title,
                VideoID = this.VideoID,
                VideoThumbMediaAsset = DynamicContentExtractor.GetMediaModel(this.VideoThumb),
                Description = this.Description,
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
