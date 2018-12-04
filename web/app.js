// MODULE
var weatherApp = angular.module('weatherApp', []);


// CONTROLLERS
weatherApp.controller('mainController', ['$scope','$filter',function ($scope, $filter) {
    
    $scope.zipCode = '';
    $scope.characters = 5;
    
    
}]);


