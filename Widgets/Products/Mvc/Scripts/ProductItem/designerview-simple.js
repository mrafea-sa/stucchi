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
                $scope.properties.Content.PropertyValue = tinyMCE.get('txtContent').getContent();

            });

            //Watch methods
      
            //Read methods
            propertyService.get()
                .then(function (data) {
                    if (data) {
                        $scope.properties = propertyService.toAssociativeArray(data.Items);
                        tinyMCE.get('txtContent').setContent($scope.properties.Content.PropertyValue);

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