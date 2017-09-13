var app = angular.module('ContactUsApp', ['ngResource', 'ngSanitize']);

app.controller('ContactUsController', ['$scope', '$resource', '$filter', '$q', '$sce', function ($scope, $resource, $filter, $q, $sce) {

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
            Country: "",
            Zip: "",
            Message: "",
            ReasonsForContact: {
                RequestForDrawing: { Status: false, Label: "Request for drawing" },
                RequestForCatalog: { Status: false, Label: "Request for catalog" },
                RequestForProductSample: { Status: false, Label: "Request for product sample" }
            },
            HasOptedForSubscription:false
        };
    }

    $scope.getFormRequest = function () {
        var request = angular.copy($scope.formRequest);
        var reasonsForContact = "";

        reasonsForContact += $scope.formRequest.ReasonsForContact.RequestForDrawing.Status ? $scope.formRequest.ReasonsForContact.RequestForDrawing.Label + " | " : "";
        reasonsForContact += $scope.formRequest.ReasonsForContact.RequestForCatalog.Status ? $scope.formRequest.ReasonsForContact.RequestForCatalog.Label + " | " : "";
        reasonsForContact += $scope.formRequest.ReasonsForContact.RequestForProductSample.Status ? $scope.formRequest.ReasonsForContact.RequestForProductSample.Label + " | " : "";

        request.HasOptedForSubscription = $scope.formRequest.HasOptedForSubscription ? "Yes" : "No";
        request.ReasonsForContact = reasonsForContact;

        return request;
    }

    $scope.initErrors = function () {
        $scope.formErrors = {
            IsSuccesssfullySent: { status: false, message: "" },
            FirstName: { status: false, message: "" },
            LastName: { status: false, message: "" },
            Company: { status: false, message: "" },
            General: { status: false, message: "" }
        };
    }

    $scope.newFormRequest = function () {
        $scope.initForm();
        $scope.formErrors.IsSuccesssfullySent.status = false;
    }

    $scope.submitFormRequest = function () {
        $scope.initErrors();

        var contactUsForm = $resource('/Api/Contact',
                                null,
                                {
                                    submitForm: {
                                        method: 'POST',
                                        headers: { 'Content-Type': 'application/json' },
                                    }
                                });

        return contactUsForm.submitForm({}, $scope.getFormRequest())
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