(function () {
  'use strict';

  angular.module('app', ['ngRoute']);
  //angular.module('app')
  //  .run(['$rootScope', '$location', 'authService', function ($rootScope, $location, authService) {
  //    $rootScope.$on('$routeChangeStart', function (event, next) {
  //      //$location.path('/Login');

  //      //se a pagina requer roles
  //      //verifica se o usuario possui as roles necessarias
  //      if (next.roles !== undefined) {
  //        if (!authService.temPermissao(next.roles))
  //          $location.path('/Login');
  //      }
  //    });
  //  }]);

})();
