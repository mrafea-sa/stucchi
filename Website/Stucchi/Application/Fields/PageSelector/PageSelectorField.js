Type.registerNamespace("Telerik.Sitefinity.Web.UI.Fields");
Type.registerNamespace("SitefinityWebApp.Application.Fields.PageSelector");

String.prototype.trunc = String.prototype.trunc ||
      function (n) {
          return this.length > n ? this.substr(0, n - 1) + '&hellip;' : this;
      };

SitefinityWebApp.Application.Fields.PageSelector.PageSelectorField = function (element) {
    SitefinityWebApp.Application.Fields.PageSelector.PageSelectorField.initializeBase(this, [element]);
    this._labelElement = null;
    this._textBoxElement = null;

    this._element = element;
    this._selectLink = null;
    this._selectLinkClickDelegate = null;
    this._selectedPageLabel = null;
    this._onLoadDelegate = null;
    this._doneLinkClickDelegate = null;
    this._cancelLinkClickDelegate = null;

    this._popupDialog = null;
    this._pageSelector = null;
    this._window = null;

    this._culture = null;
    this._uiCulture = null;
}

SitefinityWebApp.Application.Fields.PageSelector.PageSelectorField.prototype = {
    initialize: function () {
        /* Here you can attach to events or do other initialization */
        SitefinityWebApp.Application.Fields.PageSelector.PageSelectorField.callBaseMethod(this, "initialize");

        this._selectLinkClickDelegate = Function.createDelegate(this, this._selectLinkClicked);
        if (this._selectLink) {
            $addHandler(this._selectLink, "click", this._selectLinkClickDelegate);
        }

        this._onLoadDelegate = Function.createDelegate(this, this._onLoad);
        Sys.Application.add_load(this._onLoadDelegate);

        this._window = jQuery(this.get_popupDialog()).dialog({
            autoOpen: false,
            modal: true,
            width: 360,
            height: "auto",
            closeOnEscape: true,
            resizable: false,
            draggable: false,
            zIndex: 5000,
            dialogClass: "sfSelectorDialog"
        });

        this._doneLinkClickDelegate = Function.createDelegate(this, this._doneLinkClicked);
        $addHandler(this.get_pageSelector()._doneButton, "click", this._doneLinkClickDelegate);

        this._cancelLinkClickDelegate = Function.createDelegate(this, this._cancelLinkClicked);
        $addHandler(this.get_pageSelector()._cancelButton, "click", this._cancelLinkClickDelegate);
    },

    dispose: function () {

        /*  this is the place to unbind/dispose the event handlers created in the initialize method */
        SitefinityWebApp.Application.Fields.PageSelector.PageSelectorField.callBaseMethod(this, "dispose");

        if (this._selectLink) {
            $removeHandler(this._selectLink, "click", this._selectLinkClickDelegate);
        }

        if (this._selectLinkClickDelegate) {
            delete this._selectLinkClickDelegate;
        }

        Sys.Application.remove_load(this._onLoadDelegate);
        if (this._onLoadDelegate) {
            delete this._onLoadDelegate;
        }

        if (this._doneLink)
            $removeHandler(this._doneLink, "click", this._doneLinkClickDelegate);

        if (this._doneLinkClickDelegate) {
            delete this._doneLinkClickDelegate;
        }

        if (this._cancelLink)
            $removeHandler(this._cancelLink, "click", this._cancelLinkClickDelegate);

        if (this._cancelLinkClickDelegate) {
            delete this._cancelLinkClickDelegate;
        }
    },

    refreshUI: function () {
    },

    /* --------------------------------- public methods ---------------------------------- */

    // Sets the value of the page selector field control.
    set_value: function (value) {

        this._value = value;

        // Extract the Titles path
        if (value) {
            var splitArr = value.split(";");
            jQuery(this._selectedPageLabel).show();
            jQuery(this._selectedPageLabel).html(splitArr[1]);
        }
        else {
            jQuery(this._selectedPageLabel).hide();
        }
    },

    /* --------------------------------- event handlers ---------------------------------- */

    _onLoad: function (sender, args) {
    },

    _selectLinkClicked: function (sender, args) {
        
        var guidValidation = /^[0-9a-f]{8}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{12}$/i;

        if (this._value) {
            var splitArr = this._value.split(";");
           
            if (guidValidation.test(splitArr[2])) {
                // select internal page
                var pagesSelector = this.get_pageSelector().get_pageSelector();
                pagesSelector.set_selectedItems([{ Id: splitArr[2] }]);
                jQuery(this.get_pageSelector().get_extPagesSelector().get_urlTextBox().get_textElement()).val("");
                jQuery(this.get_pageSelector().get_extPagesSelector().get_titleTextBox()).val("");
            }
            else {
                // select external page
                this.get_pageSelector().get_tabstrip().set_selectedIndex(1);
                var splitArr = this._value.split(";");
                jQuery(this.get_pageSelector().get_extPagesSelector().get_urlTextBox().get_textElement()).val(splitArr[0]);
                jQuery(this.get_pageSelector().get_extPagesSelector().get_titleTextBox()).val(splitArr[1]);
            }
        }

        this._window.dialog("open");
    },

    _doneLinkClicked: function (sender, args) {

        var pagesSelector = this.get_pageSelector().get_pageSelector();
        if (this.get_pageSelector().get_tabstrip().get_selectedIndex() == 0) {
            // internal page selected
            var selectedPage = pagesSelector.get_selectedItem();
            if (selectedPage) {
                this._value = selectedPage.FullUrl + ";" + selectedPage.TitlesPath + ";" + selectedPage.Id;

                //-----
                jQuery(this._selectedPageLabel).html(selectedPage.TitlesPath);
                jQuery(this._selectedPageLabel).show();
            }
            else {
                this._value = null;
                jQuery(this._selectedPageLabel).hide();
            }

        } else {
            // external page selected
            var extURL = jQuery(this.get_pageSelector().get_extPagesSelector().get_urlTextBox().get_textElement()).val();
            var extTitle = jQuery(this.get_pageSelector().get_extPagesSelector().get_titleTextBox()).val();

            if (extURL) {
                this._value = extURL + ";" + extTitle;
                jQuery(this._selectedPageLabel).html(extTitle);
                jQuery(this._selectedPageLabel).show();
            }
            else {
                jQuery(this._selectedPageLabel).hide();
            }
        }

        this._window.dialog("close");

    },

    _cancelLinkClicked: function (sender, args) {
        this._window.dialog("close");
    },

    /* --------------------------------- private methods --------------------------------- */

    _getTextValue: function () {
        if (this._textBoxElement) {
            return this._textBoxElement.value;
        }
        return null;
    },

    _clearTextBox: function () {
        if (this._textBoxElement != null) {
            this._textBoxElement.value = "";
        }
    },

    /* --------------------------------- properties -------------------------------------- */
    get_selectLink: function () {
        return this._selectLink;
    },

    set_selectLink: function (value) {
        this._selectLink = value;
    },

    get_selectedPageLabel: function () {
        return this._selectedPageLabel;
    },

    set_selectedPageLabel: function (value) {
        this._selectedPageLabel = value;
    },

    get_popupDialog: function () {
        return this._popupDialog;
    },

    set_popupDialog: function (value) {
        this._popupDialog = value;
    },

    get_pageSelector: function () {
        //this._pageSelector.set_uiCulture(this.get_uiCulture());
        //this._pageSelector.set_culture(this.get_culture());
        return this._pageSelector;
    },

    set_pageSelector: function (value) {
        this._pageSelector = value;
    },

    get_culture: function () {
        return this._culture;
    },

    set_culture: function (culture) {
        this._culture = culture;
    },

    get_uiCulture: function () {
        return this._uiCulture;
    },

    set_uiCulture: function (culture) {
        this._uiCulture = culture;
    }
};

SitefinityWebApp.Application.Fields.PageSelector.PageSelectorField.registerClass("SitefinityWebApp.Application.Fields.PageSelector.PageSelectorField", Telerik.Sitefinity.Web.UI.Fields.FieldControl);