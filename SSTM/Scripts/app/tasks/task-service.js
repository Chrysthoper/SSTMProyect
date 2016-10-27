angular.module('TaskService', [])
.factory('TasksData', ['$http', function ($http) {
    var TasksData = {};

    TasksData.GetAll = function () {
        return $http.get('/api/task/');
    }

    TasksData.Create = function(form){
        return $http.post('/api/task', form);
    }

    return TasksData;
}]);