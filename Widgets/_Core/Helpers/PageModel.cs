using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Core.Helpers
{
    public class PageModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }

        public IList<PageModel> PageItems { get; set; }

        public PageModel()
        {
            PageItems = new List<PageModel>();
        }
    }
}
