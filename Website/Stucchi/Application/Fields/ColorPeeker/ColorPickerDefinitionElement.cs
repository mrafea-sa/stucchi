using System;
using System.Configuration;
using System.Linq;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Fields.Config;

namespace SitefinityWebApp.Application.Fields.ColorPeeker
{
    public class ColorPickerDefinitionElement : FieldControlDefinitionElement, IColorPickerDefinition
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ColorPickerDefinitionElement" /> class.
        /// </summary>
        /// <param name="parent">The parent.</param>
        public ColorPickerDefinitionElement(ConfigElement parent)
            : base(parent)
        {
        }
        #endregion

        #region FieldControlDefinitionElement members
        public override DefinitionBase GetDefinition()
        {
            return new ColorPickerDefinition(this);
        }
        #endregion

        #region IFieldDefinition members
        public override Type DefaultFieldType
        {
            get
            {
                return typeof(ColorPicker);
            }
        }
        #endregion

        #region IColorPickerDefinition
        /// <summary>
        /// Gets or sets the sample text.
        /// </summary>
        [ConfigurationProperty("SampleText")]
        public string SampleText
        {
            get
            {
                return (string)this["SampleText"];
            }
            set
            {
                this["SampleText"] = value;
            }
        }
        #endregion
    }
}
