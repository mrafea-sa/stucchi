using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Fields;
using Telerik.Sitefinity.Web.UI.Fields.Enums;

namespace SitefinityWebApp.Application.Fields.PageSelector
{
    /// <summary>
    /// A simple field control used to save a string value.
    /// Use the path to this class when you add the field control
    /// SitefinityWebApp.Application.Fields.PageSelector.PageSelectorField
    /// </summary>
    [FieldDefinitionElement(typeof(PageSelectorFieldElement))]
    public class PageSelectorField : FieldControl
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="PageSelectorField" /> class.
        /// </summary>
        public PageSelectorField()
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
                    return PageSelectorField.layoutTemplatePath;
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

        public string Text { get; set; }

        //--------------------------------------- Page Selector Field Properties -----------------------------------


        /// <summary>
        /// Gets a reference to the link that opens the page selector.
        /// </summary>
        protected virtual HyperLink SelectLink
        {
            get
            {
                return this.Container.GetControl<HyperLink>("selectLink", this.DisplayMode == FieldDisplayMode.Write);
            }
        }

        /// <summary>
        /// Gets a reference to the &gt;div&lt; tag that contains the select popup dialog.
        /// </summary>
        protected virtual HtmlGenericControl PopupDialog
        {
            get
            {
                return this.Container.GetControl<HtmlGenericControl>("popupDialog", this.DisplayMode == FieldDisplayMode.Write);
            }
        }


        /// <summary>
        /// Gets an instance of the image selector
        /// </summary>
        protected virtual Telerik.Sitefinity.Web.UI.PagesSelector PageSelector
        {
            get
            {
                return this.Container.GetControl<Telerik.Sitefinity.Web.UI.PagesSelector>("GenericPageSelector1", true);
            }
        }

        /// <summary>
        /// Gets a reference to the label that displays the currently selected page.
        /// </summary>
        protected virtual Label SelectedPageLabel
        {
            get
            {
                if (this.DisplayMode == FieldDisplayMode.Write)
                    return this.Container.GetControl<Label>("selectedPageLabel_write", true);
                else
                    return this.Container.GetControl<Label>("selectedPageLabel_read", false);
            }
        }

        /// <summary>
        /// Gets or sets the UI culture.
        /// </summary>
        /// <value>The UI culture.</value>
        public string UICulture
        {
            get
            {
                if (this.uiCulture == null)
                {
                    this.uiCulture = AppSettings.CurrentSettings.DefaultFrontendLanguage.ToString();
                }
                return this.uiCulture;
            }
            set
            {
                this.uiCulture = value;
            }
        }

        #endregion

        #region Methods
        protected override void InitializeControls(GenericContainer container)
        {
            this.ConstructControl();
        }

        public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
        {
            List<ScriptDescriptor> descriptors = new List<ScriptDescriptor>();

            ScriptControlDescriptor descriptor = base.GetScriptDescriptors().Last() as ScriptControlDescriptor;

            if (this.DisplayMode == FieldDisplayMode.Write)
            {
                descriptor.AddElementProperty("selectLink", this.SelectLink.ClientID);
                descriptor.AddElementProperty("selectedPageLabel", this.SelectedPageLabel.ClientID);
                descriptor.AddElementProperty("popupDialog", this.PopupDialog.ClientID);

                if (Telerik.Sitefinity.Abstractions.AppSettings.CurrentSettings.Multilingual)
                {
                    descriptor.AddProperty("uiCulture", this.UICulture);
                    descriptor.AddProperty("culture", this.UICulture);
                }

                descriptor.AddComponentProperty("pageSelector", this.PageSelector.ClientID);
            }

            descriptors.Add(descriptor);

            return descriptors.ToArray();
        }

        public override IEnumerable<ScriptReference> GetScriptReferences()
        {
            List<ScriptReference> scripts = new List<ScriptReference>(base.GetScriptReferences());

            scripts.Add(new ScriptReference(PageSelectorField.ScriptReference));

            return scripts;
        }

        //public override void Configure(IFieldDefinition definition)
        //{
        //    base.Configure(definition);

        //    IPageSelectorFieldDefinition fieldDefinition = definition as IPageSelectorFieldDefinition;
        //}
        #endregion

        #region Private and protected methods

        /// <summary>
        /// The method that is used to set the properties of the contained controls.
        /// </summary>
        protected internal virtual void ConstructControl()
        {
            this.TitleLabel.Text = this.Title;
            this.DescriptionLabel.Text = this.Description;

            if (this.Value != null)
            {
                Guid pageId;
                if (Guid.TryParse(this.Value.ToString(), out pageId))
                {
                    var pageManager = PageManager.GetManager();
                    var page = pageManager.GetPageNodes().Where(n => n.Id == pageId).FirstOrDefault();
                    if (page != null)
                    {
                        this.SelectedPageLabel.Text = page.Title;
                    }
                }
            }

            switch (this.DisplayMode)
            {
                case FieldDisplayMode.Read:
                    this.SelectedPageLabel.TabIndex = this.TabIndex;
                    break;
                case FieldDisplayMode.Write:
                    this.ExampleLabel.Text = this.Example;
                    this.TabIndex = 0;
                    break;
            }
        }

        #endregion

        #region Private members
        public static readonly string layoutTemplatePath = "~/Application/Fields/PageSelector/PageSelectorField.ascx";
        public static readonly string ScriptReference = "~/Application/Fields/PageSelector/PageSelectorField.js";
        private string uiCulture = Thread.CurrentThread.CurrentUICulture.Name;
        #endregion
    }
}