(function () {
    'use strict';

    angular
      .module('ImageSubmissionApp')
      .directive('uploadFile', ['$scope', '$resource', function ($scope, $resource) {

          return {
              restrict: 'EA',
              templateUrl: '/Resources/Js/Widgets/upload-file.html',
              scope: {
                  fileType: '@',
                  fileUrl: '@',
                  label: '@',
                  recommendedSize: '@',
                  recommendedWidth: '@',
                  recommendedHeight: '@',
                  information: '@',
                  onStartUpload: '&',
                  onSuccess: '&',
                  onRemove: '&',
                  onError: '&'
              },
              link: function (scope, element, attrs, ctrl) {
                  element
                    .bind('mouseenter', function () {
                        if (ctrl.fileUploaded && !ctrl.showControls) {
                            ctrl.showControls = true;
                        }

                        scope.$apply();
                    })
                    .bind('mouseleave', function () {
                        if (ctrl.fileUploaded && ctrl.showControls) {
                            ctrl.showControls = false;
                        }

                        scope.$apply();
                    })
                    .ready(function () {

                    });
              },
              controllerAs: 'vm',
              controller: function ($scope) {
                  var vm = this;

                  if ($scope.fileUrl) {
                      vm.fileUploaded = true;
                      vm.progressValue = 0;
                      vm.showControls = true;

                      vm.fileUrl = $scope.fileUrl;
                  } else {
                      vm.fileUploaded = false;
                      vm.progressValue = 0;
                      vm.showControls = false;
                  }

                  vm.RequestsEntity = RequestsEntity;

                  vm.fileType = $scope.fileType;
                  vm.label = $scope.label;
                  vm.recommendedSize = $scope.recommendedSize;
                  vm.recommendedWidth = $scope.recommendedWidth;
                  vm.recommendedHeight = $scope.recommendedHeight;
                  vm.information = $scope.information;

                  vm.uploadFileClickHandler = uploadFileClickHandler;
                  vm.removeClickHandler = removeClickHandler;

                  /*------------------------------------------------------------------
                  Event Handling
                  ------------------------------------------------------------------*/

                  function uploadFileClickHandler(files) {
                      if (!files || !files.length) {
                          return;
                      }

                      var parameters = {
                          file: files[0],
                          type: vm.fileType,
                          onSuccess: function (response) {
                              vm.fileUploaded = true;
                              vm.progressValue = 0;
                              vm.showControls = true;

                              // Add a timestamp just to bypass browser's cache
                              vm.fileUrl = response.data.fileUrl + '?timestamp=' + +new Date();

                              if (angular.isFunction($scope.onSuccess)) {
                                  $scope.onSuccess({ response: response });
                              }
                          },
                          onError: function (error) {
                              vm.progressValue = 0;

                              if (angular.isFunction($scope.onError)) {
                                  $scope.onError({ error: error });
                              }
                          },
                          onPercentageUpdate: function (evt) {
                              vm.progressValue = Math.min(100, parseInt(100 * evt.loaded / evt.total));
                          }
                      };

                      if (angular.isFunction($scope.onStartUpload)) {
                          parameters.onStartUpload = $scope.onStartUpload;
                      }

                      vm.fileUploaded = false;
                      vm.progressValue = 1;
                      vm.showControls = false;

                      UploadFileCommand.execute(parameters);
                  }

                  function removeClickHandler() {
                      vm.fileUploaded = false;
                      vm.progressValue = 0;
                      vm.showControls = false;

                      vm.fileUrl = undefined;

                      if (angular.isFunction($scope.onRemove)) {
                          $scope.onRemove();
                      }
                  }
              }
          }
      }]
      )
});
