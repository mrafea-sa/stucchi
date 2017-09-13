using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Fields;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;

namespace SitefinityWebApp.Application.Fields.ColorPeeker
{
    /// <summary>
    /// A simple field control used to save a string value.
    /// Use the path to this class when you add the field control
    /// SitefinityWebApp.Application.Fields.ColorPeeker.ColorPicker
    /// </summary>
    [FieldDefinitionElement(typeof(ColorPickerDefinitionElement))]
    public class ColorPicker : FieldControl
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ColorPicker" /> class.
        /// </summary>
        public ColorPicker()
        {
        }
        #endregion

        #region Properties
        protected override WebControl TitleControl
        {
            get
            {
                return this.TitleLabel;
            }
        }

        protected override WebControl DescriptionControl
        {
            get
            {
                return this.DescriptionLabel;
            }
        }

        protected override WebControl ExampleControl
        {
            get
            {
                return this.ExampleLabel;
            }
        }

        /// <summary>
        /// Obsolete. Use LayoutTemplatePath instead.
        /// </summary>
        protected override string LayoutTemplateName
        {
            get
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Gets the layout template's relative or virtual path.
        /// </summary>
        public override string LayoutTemplatePath
        {
            get
            {
                if (string.IsNullOrEmpty(base.LayoutTemplatePath))
                    return ColorPicker.layoutTemplatePath;
                return base.LayoutTemplatePath;
            }
            set
            {
                base.LayoutTemplatePath = value;
            }
        }

        /// <summary>
        /// Gets the reference to the label control that represents the title of the field control.
        /// </summary>
        /// <remarks>
        /// This control is mandatory only in write mode.
        /// </remarks>
        protected internal virtual Label TitleLabel
        {
            get
            {
                return this.Container.GetControl<Label>("titleLabel", true);
            }
        }

        /// <summary>
        /// Gets the reference to the label control that represents the description of the field control.
        /// </summary>
        /// <remarks>
        /// This control is mandatory only in write mode.
        /// </remarks>
        protected internal virtual Label DescriptionLabel
        {
            get
            {
                return Container.GetControl<Label>("descriptionLabel", true);
            }
        }

        /// <summary>
        /// Gets the reference to the label control that displays the example for this
        /// field control.
        /// </summary>
        /// <remarks>
        /// This control is mandatory only in the write mode.
        /// </remarks>
        protected internal virtual Label ExampleLabel
        {
            get
            {
                return this.Container.GetControl<Label>("exampleLabel", true);
            }
        }

        /// <summary>
        /// Gets the text box control.
        /// </summary>
        /// <value>The text box control.</value>
        protected virtual TextBox TextBoxControl
        {
            get
            {
                return this.Container.GetControl<TextBox>("fieldBox", true);
            }
        }

        [TypeConverter(typeof(ObjectStringConverter))]
        public override object Value
        {
            get
            {
                return this.TextBoxControl.Text;
            }
            set
            {
                this.TextBoxControl.Text = value as string;
            }
        }

        public string Text { get; set; }
        #endregion

        #region Methods
        protected override void InitializeControls(GenericContainer container)
        {
            this.TitleLabel.Text = this.Title;
            this.ExampleLabel.Text = this.Example;
            this.DescriptionLabel.Text = this.Description;

            this.TextBoxControl.Text = this.Text;
        }

        public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
        {
            List<ScriptDescriptor> descriptors = new List<ScriptDescriptor>();

            ScriptControlDescriptor descriptor = base.GetScriptDescriptors().Last() as ScriptControlDescriptor;

            if (this.TextBoxControl != null)
            {
                descriptor.AddElementProperty("textBoxElement", this.TextBoxControl.ClientID);
            }

            descriptors.Add(descriptor);

            return descriptors.ToArray();
        }

        public override IEnumerable<ScriptReference> GetScriptReferences()
        {
            List<ScriptReference> scripts = new List<ScriptReference>(base.GetScriptReferences());

            scripts.Add(new ScriptReference(ColorPicker.ScriptReference));

            return scripts;
        }

        public override void Configure(IFieldDefinition definition)
        {
            base.Configure(definition);

            IColorPickerDefinition fieldDefinition = definition as IColorPickerDefinition;

            if (fieldDefinition != null)
            {
                if (!string.IsNullOrEmpty(fieldDefinition.SampleText))
                {
                    this.Text = fieldDefinition.SampleText;
                }
            }
        }
        #endregion

        #region Private members
        public static readonly string layoutTemplatePath = "~/Application/Fields/ColorPeeker/ColorPicker.ascx";
        public static readonly string ScriptReference = "~/Application/Fields/ColorPeeker/ColorPicker.js";
        #endregion
    }
}