(function () {
  'use strict';

  angular
      .module('app')
      .controller('UserDetailsController', UserDetailsController);

  UserDetailsController.$inject = ['$http', '$routeParams', '$location'];

  function UserDetailsController($http, $routeParams, $location) {
    var vm = this;
    vm.Error = '';
    vm.getUser = function (userId) {
      vm.Error = '';
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
        vm.Error = 'Could not get user.';
      });
    };

    vm.addClaim = function () {
      vm.Error = '';
      $http.post('/api/users/' + vm.User.Id + '/' + vm.NewClaim).
      then(function (response) {
        vm.User = response.data;
      }, function (response) {
        vm.Error = 'Could not add Claim.';
      });
    };

    vm.remove = function (claimValue) {
      vm.Error = '';
      $http.delete('/api/users/' + vm.User.Id + '/' + claimValue).
      then(function (response) {
        vm.getUser($routeParams.id);
      }, function (response) {
        vm.Error = 'Could not remove Claim.';
      });
    };

    vm.back = function () {
      $location.path('/');
    };

    vm.getUser($routeParams.id);

  }
})();
