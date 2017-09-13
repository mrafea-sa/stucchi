using System;
using System.Linq;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.UI.Fields.Definitions;

namespace SitefinityWebApp.Application.Fields.ColorPeeker
{
    public class ColorPickerDefinition : FieldControlDefinition, IColorPickerDefinition
    {
        #region Constuctors
        /// <summary>
        /// Initializes a new instance of the <see cref="ColorPickerDefinition" /> class.
        /// </summary>
        public ColorPickerDefinition()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ColorPickerDefinition" /> class.
        /// </summary>
        /// <param name="configDefinition">The config definition.</param>
        public ColorPickerDefinition(ConfigElement configDefinition)
            : base(configDefinition)
        {
        }
        #endregion

        #region IColorPickerDefinition members
        /// <summary>
        /// Gets or sets the sample text.
        /// </summary>
        public string SampleText
        {
            get
            {
                return this.ResolveProperty("SampleText", this.sampleText);
            }
            set
            {
                this.sampleText = value;
            }
        }
        #endregion

        #region Private members
        private string sampleText;
        #endregion
    }
}