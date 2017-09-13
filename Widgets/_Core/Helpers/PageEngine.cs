using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Sitefinity.Web;

namespace _Core.Helpers
{
    public class PageEngine
    {
        public static IList<PageModel> GetProcessedSitemapItems(PageSiteNode pageItemNode)
        {
            IList<PageModel> navigationItems = new List<PageModel>();

            foreach (var pageNode in pageItemNode.ChildNodes)
            {
                PageSiteNode pageSiteNode = pageNode as PageSiteNode;
                string firstChildUrl = "#";

                if (!pageSiteNode.IsPublished() || !pageSiteNode.ShowInNavigation || pageSiteNode.NodeType == Telerik.Sitefinity.Pages.Model.NodeType.InnerRedirect) continue;


                PageModel itemToBeAdded = new PageModel();
                itemToBeAdded.Name = pageSiteNode.Title;
                itemToBeAdded.Id = pageSiteNode.Id.ToString();

                IList<PageModel> children = new List<PageModel>();

                if (pageSiteNode.HasChildNodes)
                {
                    children = GetProcessedSitemapItems(pageSiteNode as PageSiteNode);
                    firstChildUrl = GetFirstChildUrl(pageSiteNode as PageSiteNode);
                }

                if (pageSiteNode.NodeType == Telerik.Sitefinity.Pages.Model.NodeType.Group && children.Count == 0) continue;

                if (pageSiteNode.NodeType == Telerik.Sitefinity.Pages.Model.NodeType.Group && children.Count > 0)
                    itemToBeAdded.Url = Utility.ConvertRelativeUrlToAbsoluteUrl(firstChildUrl);
                else
                    itemToBeAdded.Url = Utility.ConvertRelativeUrlToAbsoluteUrl(pageSiteNode.Url);

                itemToBeAdded.PageItems = children;
                navigationItems.Add(itemToBeAdded);
            }

            return navigationItems;
        }

        private static string GetFirstChildUrl(PageSiteNode pageItemNode)
        {
            foreach (var pageNode in pageItemNode.ChildNodes)
            {
                PageSiteNode pageSiteNode = pageNode as PageSiteNode;
                if (!pageItemNode.IsPublished()) continue;

                return pageSiteNode.Url;
            }

            return "#";
        }

    }
}
