var app = angular.module('HelpDeskApp', ['ngResource', 'ngSanitize']);

app.controller('HelpDeskController', ['$scope', '$resource', '$filter', '$q', '$sce', function ($scope, $resource, $filter, $q, $sce) {

    $scope.formErrors = null;
    $scope.formRequest = null;

    $scope.initForm = function () {
        $scope.formRequest = {
            FirstName: "",
            LastName: "",
            Address: "",
            Address2: "",
            Company: "",
            City: "",
            Phone: "",
            State: "",
            Email: "",
            Zip: "",
            Message: ""
        };
    }

    $scope.initErrors = function () {
        $scope.formErrors = {
            IsSuccesssfullySent: { status: false, message: "" },
            FirstName: { status: false, message: "" },
            LastName: { status: false, message: "" },
            Company: { status: false, message: "" },
            Email: { status: false, message: "" },
            General: { status: false, message: "" }
        };
    }

    $scope.newFormRequest = function () {
        $scope.initForm();
        $scope.formErrors.IsSuccesssfullySent.status = false;
    }

    $scope.submitFormRequest = function () {
        $scope.initErrors();

        var helpdeskForm = $resource('/Api/HelpDesk',
                                null,
                                {
                                    submitForm: {
                                        method: 'POST',
                                        headers: { 'Content-Type': 'application/json' },
                                    }
                                });

        return helpdeskForm.submitForm({}, $scope.formRequest)
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