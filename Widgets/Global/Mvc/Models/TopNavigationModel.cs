using _Core;
using _Core.Extensions;
using _Core.Helpers;

using _Core.Helpers;

using _Core.Mvc.Models;

using _Core.Mvc.Models;

using Global.Mvc.Models;
using System;
using System.Collections;

using System.Collections;

using System.Collections.Generic;
using System.Dynamic;

using System.Dynamic;

using System.Linq;
using System.Web;
using Telerik.Sitefinity.Web;


namespace GlobalWidgets.Mvc.Models
{
    public class TopNavigationModel : _BaseModel
    {
        /// <inheritdoc />
        public string Title { get; set; }

        /// <inheritdoc />
        public override _BaseViewModel GetViewModel()
        {
            var viewModel = new TopNavigationViewModel()
            {
                Title = this.Title,
                MainNavigationItems = GetProcessedMainNavigationItems()
            };

            return viewModel;
        }

        /// <inheritdoc />
        public override bool IsEmpty()
        {
            return (string.IsNullOrEmpty(this.Title));
        }

        #region Private Methods

        private ArrayList GetProcessedMainNavigationItems()
        {
            ArrayList navigationItems = new ArrayList();

            SiteMapProvider provider = SiteMapBase.GetSiteMapProvider("FrontEndSiteMap");
            SiteMapNode root = provider.RootNode;

            foreach (var pageNode in root.ChildNodes)
            {
                PageSiteNode pageSiteNode = pageNode as PageSiteNode;
                if (!pageSiteNode.ShowInNavigation || !pageSiteNode.IsPublished()) continue;

                dynamic itemToBeAdded = new ExpandoObject();
                itemToBeAdded.Title = pageSiteNode.Title;
                itemToBeAdded.Url = Utility.ConvertRelativeUrlToAbsoluteUrl(pageSiteNode.Url);

                ArrayList children = new ArrayList();

                if (pageSiteNode.HasChildNodes)
                {
                    foreach (var childPageNode in pageSiteNode.ChildNodes)
                    {
                        PageSiteNode pageChildSiteNode = childPageNode as PageSiteNode;
                        if (!pageChildSiteNode.ShowInNavigation || !pageChildSiteNode.IsPublished()) continue;

                        dynamic childToBeAdded = new ExpandoObject();
                        childToBeAdded.Title = pageChildSiteNode.Title;
                        childToBeAdded.Url = Utility.ConvertRelativeUrlToAbsoluteUrl(pageChildSiteNode.Url);

                        children.Add(childToBeAdded);
                    }
                }

                itemToBeAdded.Children = children;
                navigationItems.Add(itemToBeAdded);
            }

            return navigationItems;
        }

        #endregion Private Methods
    }
}
