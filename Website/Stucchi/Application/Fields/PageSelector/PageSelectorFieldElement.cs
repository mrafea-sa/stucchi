using System;
using System.Configuration;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Fields.Config;

namespace SitefinityWebApp.Application.Fields.PageSelector
{
    /// <summary>
    /// A configuration element used to persist the properties of <see cref="PageSelectorFieldDefinition"/>
    /// </summary>
    public class PageSelectorFieldElement : FieldControlDefinitionElement, IPageSelectorFieldDefinition
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="PageSelectorFieldElement"/> class.
        /// </summary>
        /// <param name="parent">The parent.</param>
        public PageSelectorFieldElement(ConfigElement parent)
            : base(parent)
        {
        }
        #endregion

        #region FieldControlDefinitionElement Members
        /// <summary>
        /// Gets an instance of the <see cref="PageSelectorFieldDefinition"/> class.
        /// </summary>
        public override DefinitionBase GetDefinition()
        {
            return new PageSelectorFieldDefinition(this);
        }
        #endregion

        #region IFieldDefinition members
        public override Type DefaultFieldType
        {
            get
            {
                return typeof(PageSelectorField);
            }
        }
        #endregion

        #region IPageSelectorFieldDefinition Members
        /// <summary>
        /// Gets or sets the dynamic module type
        /// </summary>
        [ConfigurationProperty("DynamicModuleType")]
        public string DynamicModuleType
        {
            get
            {
                return (string)this["DynamicModuleType"];
            }
            set
            {
                this["DynamicModuleType"] = value;
            }
        }
        #endregion
    }
}