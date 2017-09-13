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
using Telerik.Sitefinity.Modules.Events;
using Telerik.Sitefinity.Modules.News;
using Telerik.Sitefinity.News.Model;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Events.Model;


namespace GlobalWidgets.Mvc.Models
{
    public class NewsAndEventsModel : _BaseModel
    {
        /// <inheritdoc />
        public string Title { get; set; }

        /// <inheritdoc />
        public override _BaseViewModel GetViewModel()
        {
            var viewModel = new NewsAndEventsViewModel()
            {
                Title = this.Title,
                EventsItems = this.GetProcessedEventsItems(),
                NewsItems = this.GetProcessedNewsItems()
            };

            return viewModel;
        }

        /// <inheritdoc />
        public override bool IsEmpty()
        {
            return (string.IsNullOrEmpty(this.Title));
        }

        #region Private Methods

        private ArrayList GetProcessedNewsItems()
        {
            NewsManager newsManager = new NewsManager();
            IList<NewsItem> newsItems = newsManager.GetNewsItems().Where(pred => pred.Status == ContentLifecycleStatus.Live).OrderBy(pred => pred.PublicationDate).ToList();

            ArrayList processedList = new ArrayList();
            foreach (NewsItem item in newsItems)
            {
                dynamic itemToBeAdded = new ExpandoObject();

                itemToBeAdded.Title = item.Title;
                itemToBeAdded.Content = item.Content;

                processedList.Add(itemToBeAdded);
            }

            return processedList;
        }

        private ArrayList GetProcessedEventsItems()
        {
            EventsManager eventsManager = new EventsManager();
            IList<Event> eventItems = eventsManager.GetEvents().Where(pred => pred.Status == ContentLifecycleStatus.Live && pred.EventEnd >= DateTime.Now).OrderBy(pred => pred.EventStart).ToList();

            ArrayList processedList = new ArrayList();
            foreach (Event item in eventItems)
            {
                dynamic itemToBeAdded = new ExpandoObject();

                itemToBeAdded.Title = item.Title;
                itemToBeAdded.Content = item.Content;
                itemToBeAdded.Location = string.Format("{0} - {1}, {2}", item.Street, item.City, item.State);
                itemToBeAdded.EventStart = item.EventStart.ToString("MMMM dd, yyy");
                itemToBeAdded.EventStartEncoded = item.EventStart.ToString("o");
                itemToBeAdded.EventStartMonth = item.EventStart.ToString("MMM");
                itemToBeAdded.EventStartDay = item.EventStart.ToString("dd");
                itemToBeAdded.EventEnd = "-";

                if (item.EventEnd.HasValue) itemToBeAdded.EventEnd = item.EventEnd.Value.ToString("MMMM dd, yyy");

                processedList.Add(itemToBeAdded);
            }

            return processedList;
        }

        #endregion Private Methods
    }

}
