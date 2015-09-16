(function () {
  'use strict';

  angular
      .module('app')
      .controller('UserDetailsController', UserDetailsController);

  UserDetailsController.$inject = ['$http', '$routeParams'];

  function UserDetailsController($http, $routeParams) {
    var vm = this;

    vm.getUser = function (userId) {
      var req = {
        method: 'GET',
        url: '/api/users/' + userId,
        headers: {
          'Content-Type': 'application/x-www-form-urlencoded'
        }
      }
      $http(req).success(function (data, status, headers, config) {
        vm.User = data;
      }).error(function (data, status, headers, config) {
        console.log(data);
      });
    };

    vm.addClaim = function () {
      $http.post('/api/users/' + vm.User.Id + '/' + vm.NewClaim).
      then(function (response) {
        vm.User = response.data;
      }, function (response) {
      });
    };

    vm.remove = function (claimValue) {
      $http.delete('/api/users/' + vm.User.Id + '/' + claimValue).
      then(function (response) {
        vm.getUser($routeParams.id);
      }, function (response) {
      });
    };

    vm.getUser($routeParams.id);

  }
})();
