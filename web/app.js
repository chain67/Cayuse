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

app.controller('HomeController',  ['$scope', function($scope) {
  $scope.message = 'Cayuse Weather App Home';
}]);


app.controller('WeatherController',  ['$scope', '$http', function($scope, $http) {
    
        $scope.submit = function() {
          if ($scope.zipCode) {
            $scope.zipCode = this.zipCode;
           

        $scope.weather = $http({
        url: 'http://localhost:50507/api/weather/', 
        method: "GET",
        params: {zipCode: $scope.zipCode}
                }).then(function(response){$scope.weather = response.data;});
         
           }
        };
 
   }]);