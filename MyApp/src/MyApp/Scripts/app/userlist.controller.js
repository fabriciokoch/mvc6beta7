(function () {
  'use strict';

  angular
      .module('app')
      .controller('UserListController', UserListController);

  UserListController.$inject = ['$http', '$location', 'canAdd'];

  function UserListController($http, $location, canAdd) {
    var vm = this;
    vm.canAdd = canAdd;
    vm.Users = [];
    vm.Error = '';

    vm.selectUsers = function () {
      vm.Error = '';
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
        vm.Error = 'Could not get users.';
      });
    };

    vm.userDetails = function (id) {
      $location.path('/Users/' + id);
    };

    vm.addUser = function () {
      vm.Error = '';
      $http.post('/api/users/new/' + vm.NewUser.UserName + '/' + vm.NewUser.Password).
      then(function (response) {
        console.log('ok');
        console.log(response);
        vm.selectUsers();
      }, function (response) {
        console.log('error');
        console.log(response);
        vm.Error = 'Could not add User.';
      });
    };

    vm.selectUsers();
  }
})();

