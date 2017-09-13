Type.registerNamespace("SitefinityWebApp.Application.Fields.ColorPeeker");

SitefinityWebApp.Application.Fields.ColorPeeker.ColorPicker = function (element) {
    SitefinityWebApp.Application.Fields.ColorPeeker.ColorPicker.initializeBase(this, [element]);
    this._element = element;
    this._labelElement = null;
    this._textBoxElement = null;
}

SitefinityWebApp.Application.Fields.ColorPeeker.ColorPicker.prototype = {
    initialize: function () {
        /* Here you can attach to events or do other initialization */      
        SitefinityWebApp.Application.Fields.ColorPeeker.ColorPicker.callBaseMethod(this, "initialize");
    },

    dispose: function () {
        /*  this is the place to unbind/dispose the event handlers created in the initialize method */   
        SitefinityWebApp.Application.Fields.ColorPeeker.ColorPicker.callBaseMethod(this, "dispose");
    },

    /* --------------------------------- public methods ---------------------------------- */

    /* --------------------------------- event handlers ---------------------------------- */

    /* --------------------------------- private methods --------------------------------- */

    _getTextValue: function(){
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
    get_value: function () {    
        var val = this._getTextValue();
        return val;
    },

    set_value: function (value) {
        this._clearTextBox();
        if (value !== undefined && value != null && this._textBoxElement != null) {
            this._textBoxElement.value = value;
        }
        this._value = value;
    },
    
    get_textBoxElement: function () {
        return this._textBoxElement;
    },

    set_textBoxElement: function (value) {
        this._textBoxElement = value;
    }    
};

SitefinityWebApp.Application.Fields.ColorPeeker.ColorPicker.registerClass("SitefinityWebApp.Application.Fields.ColorPeeker.ColorPicker", Telerik.Sitefinity.Web.UI.Fields.FieldControl);