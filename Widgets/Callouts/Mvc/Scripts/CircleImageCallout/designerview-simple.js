(function ($) {

    var customModule = angular.module('simpleModule', []);

    angular.module('designer').requires.push('simpleModule');
    angular.module('designer').requires.push('sfFields');
    angular.module('designer').requires.push('sfSelectors');

    customModule.controller('SimpleCtrl', ['$scope', 'propertyService',
        function ($scope, propertyService) {
            $scope.feedback.showLoadingIndicator = true;
            $(".sf-backend-wrp .modal-dialog").css("width", "800px");

            //Initialize methods
            $scope.initialize = function () {
            };


            //Save method
            $scope.feedback.savingHandlers.push(function () {
            });

            //Watch methods
            $scope.$watch('properties.ItemsSelectedItems.PropertyValue', function (newValue, oldValue) {
                if (newValue) {
                    $scope.ItemsSelectedItems = JSON.parse(newValue);
                }
            });

            $scope.$watch('ItemsSelectedItems', function (newValue, oldValue) {
                if (newValue) {
                    $scope.properties.ItemsSelectedItems.PropertyValue = JSON.stringify(newValue);
                }
            });

            $scope.$watch('properties.ItemsSelectedIds.PropertyValue', function (newValue, oldValue) {
                if (newValue) {
                    $scope.ItemsSelectedIds = JSON.parse(newValue);
                }
            });

            $scope.$watch('ItemsSelectedIds', function (newValue, oldValue) {
                if (newValue) {
                    $scope.properties.ItemsSelectedIds.PropertyValue = JSON.stringify(newValue);
                }
            });

      
            //Read methods
            propertyService.get()
                .then(function (data) {
                    if (data) {
                        $scope.properties = propertyService.toAssociativeArray(data.Items);
                        $scope.initialize();
                    }
                },

                function (data) {
                    $scope.feedback.showError = true;
                    if (data) $scope.feedback.errorMessage = data.Detail;
                })

                .finally(function () {
                    $scope.feedback.showLoadingIndicator = false;
                });
        }]);
})(jQuery);