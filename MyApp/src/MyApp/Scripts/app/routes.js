(function () {
  'use strict';

  angular
    .module('app')
    .config(config);

  config.$inject = ['$routeProvider', '$locationProvider'];

  function config($routeProvider, $locationProvider) {
    $routeProvider
          .when('/', {
            templateUrl: 'user-list.html',
            controller: 'UserListController',
            controllerAs: 'vm'
          })
          .when('/Users/:id?', {
            templateUrl: 'user-details.html',
            controller: 'UserDetailsController',
            controllerAs: 'vm'
          })
  }

})();
