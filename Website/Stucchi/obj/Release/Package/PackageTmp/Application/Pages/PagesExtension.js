// called by the Detail Form View when it has loaded
function OnModuleMasterViewLoadedCustom(sender, args) {
    // the sender here is DetailFormView
    var currentForm = sender;

    PagesExtension.initialize(currentForm);
}

var PagesExtension = (function ($) {

    var extender = function (masterView) {
        this._masterView = masterView;
        this._items = null;
    };

    extender.prototype = {
        init: function () {
            var deleg = createDelegate(this, this.onDataBinding);
            this._masterView.get_binder().add_onDataBinding(deleg);

            var onItemDataBoundDeleg = createDelegate(this, this.onItemDataBound);
            this._masterView.get_binder().add_onItemDataBound(onItemDataBoundDeleg);

            var onDataBoundDeleg = createDelegate(this, this.onDataBound);
            this._masterView.get_binder().add_onDataBound(onDataBoundDeleg);
        },

        onDataBinding: function (sender, args) {
            var ids = this.getItemsIds(sender, args);

            if (ids && ids.length > 0) {
                // get the taxa items using the service
                this._items = this.getItemsPageSpeedInsights(ids);
            }
        },

        onItemDataBound: function (sender, args) {
            if (this._items && this._items.length > 0) {
                if (args) {
                    this.setColumnData("PageSpeedInsights", this._items, args.get_itemIndex(), args.get_key().Id, args.get_itemElement());
                }
            }
        },

        onDataBound: function (sender, args) {
            $('.sfPageSpeedInsights').css('margin-left', '10px');
            $('.sfPageSpeedInsights').css('width', '120px');
        },

        // get the items tags Ids - called on DataBound
        getItemsIds: function (sender, args) {
            // all items, which will be bound
            var dataItems = args.get_dataItem().Items;
            var ids = [];
            if (dataItems && dataItems.length > 0) {
                for (var i = 0; i < dataItems.length; i++) {
                    // get the tags Ids for each item from the property
                    var id = dataItems[i].Id;
                    // populate the array
                    ids.push(id);
                }
            }

            return ids;
        },

        getItemsPageSpeedInsights: function (ids) {
            var _self = this;
            // url to our service
            var serviceUrl = "/Sitefinity/Services/PagesServiceCustom.svc/";
            // stringify the data to be sent
            var itemsData = JSON.stringify(ids);
            var items = null;
            // post request to get the data
            jQuery.ajax({
                type: "POST",
                async: false,
                url: serviceUrl + "GetItemsPageSpeedInsights",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: itemsData,
                success: function (data) {
                    items = data;
                },
                error: function (data) {
                    console.log(data);
                }
            });

            // return the item models
            return items;
        },

        // set the grid column content
        // selectorName: the selector to be used to find the column element (the id of the element in the column's client template
        // items: the items to use collection
        // itemIndex: the item position in the grid/table
        // id: the id of the item
        // itemElement: the item row element - the <tr> of the grid
        setColumnData: function (selectorName, items, itemIndex, id, itemElement) {
            var PageSpeedInsights = "";
            // find this item
            for (var i = 0; i < items.length; i++) {
                if (items[i].Id == id) {
                    PageSpeedInsights = items[i].PageSpeedInsights;
                    break;
                }
            }
            // find the grid row and column and set its content
            // example: element column for first item in the grid -> #element0
            $(itemElement).find("#" + selectorName + itemIndex).html(PageSpeedInsights);

            $(itemElement).find(".pagespeed-red").attr('style', 'font-size:12px; background-color: #dd4b39; color: #fff; text-align: center; padding: 0px; width: 40px; float:left; ');
            $(itemElement).find(".pagespeed-green").attr('style', 'font-size:12px; background-color: #009a2d; color: #fff; text-align: center; padding: 0px; width: 40px; float:left');
            $(itemElement).find(".pagespeed-orange").attr('style', 'font-size:12px; background-color: #fda100; color: #fff; text-align: center; padding: 0px; width: 40px; float:left');
            $(itemElement).find(".pagespeed-mobile").css('margin-right', '5px');

            $(itemElement).find(".pagespeed-updated").attr('style', 'font-size:12px; clear:both');

        }
    };

    return {
        initialize: function (masterView) {
            if (!masterView) {
                throw new Error("masterView is not defined");
            }

            var masterViewExtender = new extender(masterView);
            masterViewExtender.init();
        }
    };

}(jQuery));

// --- Helpers ---

// From Microsoft.Ajax - Function.createDelegate
function createDelegate(a, b) {
    return function () { return b.apply(a, arguments) }
};