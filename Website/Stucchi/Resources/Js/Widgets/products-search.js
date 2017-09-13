var app = angular.module('ProductsApp', ['ngResource', 'ngSanitize']);

app.controller('ProductsController', ['$scope', '$resource', '$filter', '$q', '$sce', function ($scope, $resource, $filter, $q, $sce) {

    $scope.pageNo = 0;
    $scope.filters = null;
    $scope.prodResult = null;

    $scope.setFilters = function () {
        $scope.pageNo = 0;
        $scope.initializeProductList();
    }

    $scope.getFilters = function () {
        var listOfFilters = {
            Applications: "",
            BodySizes: "",
            Connections: "",
            WorkingPressures: "",
            Materials: ""
        };
        if ($scope.filters == null) return listOfFilters;

        listOfFilters.Applications = $scope.getFiltersByList($scope.filters.Applications).join();
        listOfFilters.BodySizes = $scope.getFiltersByList($scope.filters.BodySizes).join();
        listOfFilters.Connections = $scope.getFiltersByList($scope.filters.Connections).join();
        listOfFilters.WorkingPressures = $scope.getFiltersByList($scope.filters.WorkingPressures).join();
        listOfFilters.Materials = $scope.getFiltersByList($scope.filters.Materials).join();
        return listOfFilters;
    }

    $scope.getFiltersByList = function (listOfItems) {
        var listOfFilters = [];
        if (listOfItems == null) return listOfFilters;

        for (var idx = 0; idx < listOfItems.length; idx++) {
            if (listOfItems[idx].isSetForFiltering == true) listOfFilters.push(listOfItems[idx].Id);
        }
        return listOfFilters;
    }

    $scope.initializeFilterList = function () {
        var filterResources = $resource('/Api/Filters',
                                null,
                                {
                                    getProductFilters: {
                                        method: 'GET'
                                    }
                                });

        return filterResources.getProductFilters()
            .$promise.then(function (result) {
                $scope.filters = result;
            });
    }

    $scope.initializeProductList = function () {
        var filtersList = $scope.getFilters();

        var filterProducts = $resource('/Api/Products',
                                null,
                                {
                                    getProducts: {
                                        method: 'GET', params: {
                                            applications: filtersList.Applications,
                                            bodySizes: filtersList.BodySizes,
                                            connections: filtersList.Connections,
                                            workingPressures: filtersList.WorkingPressures,
                                            materials: filtersList.Materials,
                                            pageNo: $scope.pageNo
                                        }
                                    }
                                });

        return filterProducts.getProducts()
            .$promise.then(function (result) {
                $scope.prodResult = result;
                $scope.pageNo++;
            });
    }

    $scope.loadMoreProducts = function () {
        $scope.initializeProductList();
    }

    $scope.initializeApplication = function () {
        $q.all([$scope.initializeFilterList(),
                $scope.initializeProductList()])
            .then(function () {

            });
    }


    $scope.initializeApplication();
}]);