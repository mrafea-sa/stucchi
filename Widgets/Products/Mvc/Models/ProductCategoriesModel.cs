using _Core;
using _Core.Extensions;
using _Core.Mvc.Models;
using _Core.Helpers;
using ProductsWidgets.Mvc.Models;
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


namespace ProductsWidgets.Mvc.Models
{
    public class ProductCategoriesModel : _BaseModel
    {
        /// <inheritdoc />
        public string Title { get; set; }

        /// <inheritdoc />
        public string ItemsSelectedItems { get; set; }

        /// <inheritdoc />
        public string ItemsSelectedIds { get; set; }

        /// <inheritdoc />
        public override _BaseViewModel GetViewModel()
        {
            var viewModel = new ProductCategoriesViewModel()
            {
                Title = this.Title,
                ItemsSelectedItems = string.IsNullOrEmpty(ItemsSelectedIds) ? new ArrayList() : GetProcessedItemsSelectedItems(JsonConvert.DeserializeObject(ItemsSelectedIds)),
            };

            return viewModel;
        }

        /// <inheritdoc />
        public override bool IsEmpty()
        {
            return (string.IsNullOrEmpty(this.Title));
        }

        #region Private Methods

      

        private ArrayList GetProcessedItemsSelectedItems(dynamic items)
        {
            ArrayList processedList = new ArrayList();
            foreach (dynamic item in items)
            {
                Guid itemId = Guid.Parse((string)item);
                DynamicContent itemContent = DynamicContentOps.GetGenericItemById(ModuleTypes.ProductsCategoryName, itemId);

                dynamic itemToBeAdded = new ExpandoObject();

            itemToBeAdded.Content = itemContent.GetValue<Lstring>("Content");
            itemToBeAdded.ImageMediaAssets = DynamicContentExtractor.GetMediaAssets(itemContent, new string[] { "Image" });

                processedList.Add(itemToBeAdded);
            }

            return processedList;
        }


        #endregion
    }
}