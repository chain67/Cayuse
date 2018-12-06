var app = angular.module('myApp', ['ngRoute']);

app.config(function($routeProvider) {
  $routeProvider

  .when('/', {
    templateUrl : 'pages/home.html',
    controller  : 'HomeController'
  })

    .when('/weather', {
    templateUrl : 'pages/weather.html',
    controller  : 'WeatherController'
  })

  .otherwise({redirectTo: '/'});
});

app.controller('HomeController', function($scope) {
  $scope.message = 'Hello from HomeController';
});



app.controller('WeatherController', function($scope, $http) {
        
        $scope.weather = $http({
        url: 'http://localhost:50507/api/weather/', 
        method: "GET",
        params: {zipCode: $scope.zipCode}
                }).then(function(response){$scope.weather = response.data;});
         
        
 
   });