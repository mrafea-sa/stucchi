using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.UI.Fields.Definitions;

namespace SitefinityWebApp.Application.Fields.PageSelector
{
    /// <summary>
    /// A control definition for the simple image field
    /// </summary>
    public class PageSelectorFieldDefinition : FieldControlDefinition, IPageSelectorFieldDefinition
    {
        #region Constuctors
        /// <summary>
        /// Initializes a new instance of the <see cref="PageSelectorFieldDefinition"/> class.
        /// </summary>
        public PageSelectorFieldDefinition()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PageSelectorFieldDefinition"/> class.
        /// </summary>
        /// <param name="element">The configuration element used to persist the control definition.</param>
        public PageSelectorFieldDefinition(ConfigElement element)
            : base(element)
        {
        }
        #endregion

        #region IPageSelectorFieldDefinition members
        public string DynamicModuleType
        {
            get
            {
                return this.ResolveProperty("DynamicModuleType", this.dynamicModuleType);
            }
            set
            {
                this.dynamicModuleType = value;
            }
        }
        #endregion

        #region Private members
        private string dynamicModuleType;
        #endregion
    }
}