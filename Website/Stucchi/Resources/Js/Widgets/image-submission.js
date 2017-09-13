var app = angular.module('ImageSubmissionApp', ['ngResource', 'ngSanitize']);

app.controller('ImageSubmissionController', ['$scope', '$resource', '$filter', '$q', '$sce', function ($scope, $resource, $filter, $q, $sce) {

    console.log("asas", arguments);

    $scope.formErrors = null;
    $scope.formRequest = null;

    $scope.initForm = function () {
        $scope.formRequest = {
            FullName: "",
            Company: "",
            Email: "",
            PhoneNumber: "",
            Comments: ""
        };
    }

    $scope.initErrors = function () {
        $scope.formErrors = {
            IsSuccesssfullySent: { status: false, message: "" },
            FullName: { status: false, message: "" },
            Company: { status: false, message: "" },
            PhoneNumber: { status: false, message: "" },
            Email: { status: false, message: "" },
            Comments: { status: false, message: "" }
        };
    }

    $scope.newFormRequest = function () {
        $scope.initForm();
        $scope.formErrors.IsSuccesssfullySent.status = false;
    }

    $scope.submitFormRequest = function () {
        $scope.initErrors();

        var imageSubmissionForm = $resource('/Api/ImageSubmission',
                                null,
                                {
                                    submitForm: {
                                        method: 'POST',
                                        headers: { 'Content-Type': 'application/json' },
                                    }
                                });

        return imageSubmissionForm.submitForm({}, $scope.formRequest)
            .$promise
                .then(function (result) {
                    $scope.formErrors.IsSuccesssfullySent.status = true;
                })
                .catch(function (err) {
                    for (var idx = 0; idx < err.data.length; idx++) {
                        $scope.formErrors[err.data[idx].ControlId].status = true;
                        $scope.formErrors[err.data[idx].ControlId].message = err.data[idx].ErrorMessage;

                    }
                });
    }

    $scope.initForm();
}]);