angular.module('UserService', [])
.factory('UsersData', ['$http', function ($http) {
    var UsersData = {};

    UsersData.GetAll = function () {
        return $http.get('/api/user/');
    }

    return UsersData;
}]);