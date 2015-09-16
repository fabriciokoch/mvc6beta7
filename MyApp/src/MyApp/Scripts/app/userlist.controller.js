(function () {
  'use strict';

  angular
      .module('app')
      .controller('UserListController', UserListController);

  UserListController.$inject = ['$http', '$location'];

  function UserListController($http, $location) {
    var vm = this;
    vm.Users = [];

    vm.selectUsers = function () {
      var req = {
        method: 'GET',
        url: '/api/users',
        headers: {
          'Content-Type': 'application/x-www-form-urlencoded'
        }
      }
      $http(req).success(function (data, status, headers, config) {
        vm.Users = data;
      }).error(function (data, status, headers, config) {
        console.log(data);
      });
    };

    vm.userDetails = function (id) {
      $location.path('/Users/' + id);
    };

    vm.selectUsers();
  }
})();
