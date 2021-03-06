﻿var app = angular.module("customerModule", ['ngRoute']);

app.config(['$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider) {
    $routeProvider.when('/',
        {
            templateUrl: '/Home/IndexTemplate',
        });
    $routeProvider.when('/Customer',
        {
            templateUrl: '/Customer/IndexTemplate',
            controller: 'ShowCustomersController'
        });
    $routeProvider.when('/Customer/Edit/:id',
        {
            templateUrl: '/Customer/EditTemplate',
            controller: 'EditCustomerController'
        });
    $routeProvider.when('/Customer/Create',
        {
            templateUrl: '/Customer/CreateTemplate',
            controller: 'CreateCustomerController'
        });
    $routeProvider.otherwise(
        {
            redirectTo: '/'
        });

    $locationProvider.html5Mode(true);
    $locationProvider.hashPrefix = '!';
}]);

app.service('CustomerService', ['$http', function ($http) {

    this.getCustomers = function () {
        return $http.get("/api/CustomerApi");
    };

    this.getCustomer = function (id) {
        return $http.get("/api/CustomerApi/" + id);
    }

    this.post = function (Customer) {
        var request = $http({
            method: "post",
            url: "/api/CustomerApi",
            data: Customer
        });
        return request;
    };

    this.put = function (id, Customer) {
        var request = $http({
            method: "put",
            url: "/api/CustomerApi/" + id,
            data: Customer
        });
        return request;
    };

}]);

app.controller('ShowCustomersController', ['$scope', '$location', '$http', 'CustomerService', function ($scope, $location, $http, CustomerService) {

    var isServerLoaded = document.getElementById('hdnServerLoaded');

    if (!isServerLoaded) {
        loadData();
    }

    function loadData()
    {
        CustomerService.getCustomers().success(function (result) {
            $scope.Customers = result;
            $scope.Show = result.length > 0;
        });
    }

    $scope.edit = function (id) {
        $location.path("/Customer/Edit/" + id);
    }
}]);

app.controller('EditCustomerController', ['$scope', '$routeParams', '$location', '$http','$filter', 'CustomerService', function ($scope, $routeParams, $location, $http,$filter, CustomerService) {

    var isServerLoaded = document.getElementById('hdnServerLoaded');

    if (!isServerLoaded) {
        loadData();
    }

    function loadData() {
        CustomerService.getCustomer($routeParams.id).success(function (result) {
            $scope.Customer = result;
        });
    }

    $scope.backToList = function () {
        $location.path("/");
    }

    $scope.save = function (id) {
        if ($scope.edit_form.$valid) {
            CustomerService.put($routeParams.id, $scope.Customer).success(function (data, status) {
                $scope.backToList();
            }).error(function (data, status) {
                $scope.Error = "Failed to update customer";
            });
        }
    }

}]);

app.controller('CreateCustomerController', ['$scope', '$location', '$http', 'CustomerService', function ($scope, $location, $http, CustomerService) {
    $scope.backToList = function () {
        $location.path("/Customer");
    }

    $scope.save = function () {

        if ($scope.create_form.$valid) {
            
            var Customer = {
                FirstName: $scope.FirstName,
                Surname: $scope.Surname,
                Telephone: $scope.Telephone
            };

            CustomerService.post(Customer).success(function (data, status) {
                $scope.backToList();
            }).error(function (data, status) {
                $scope.Error = "Failed to save customer";
            });
        }
    }
}]);

app.directive('valRequired', function () {
    return {
        restrict: 'A',
        require: 'ngModel',

        link: function (scope, elem, attrs, ngModel) {
            scope.$watch(attrs.ngModel, function (value) {
                var isValid = value != null && value.length > 0;
                ngModel.$setValidity(attrs.ngModel, isValid);
            });
        }
    };
});

app.directive('valmsgFor', function () {
    return {
        restrict: 'A',
        require: '^form',
        link: function (scope, elem, attrs, ctrl) {
            var modelField = ctrl.$name + '.' + attrs.valmsgFor + '.$valid';
            scope.$watch(modelField, function (isValid) {
                if (isValid) {
                    elem.attr('class', 'field-validation-valid');
                } else {
                    elem.attr('class', 'field-validation-invalid');
                }
            });
        }
    };
});