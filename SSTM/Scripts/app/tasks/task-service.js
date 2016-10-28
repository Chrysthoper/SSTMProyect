angular.module('TaskService', [])
.factory('TasksData', ['$http', function ($http) {
    var TasksData = {};

    TasksData.GetAll = function () {
        return $http.get('/api/task/');
    }

    TasksData.Create = function(form){
        return $http.post('/api/task', form);
    }

    TasksData.GetTasksStates = function () {
        return $http.get('/api/taskstates/');
    }

    TasksData.GetModalForTask = function () {
        return $http.get('/home/TemplateTasks/');
    }

    TasksData.GetAllFromUser = function (id) {
        return $http.get('/api/task/' + id);
    }

    return TasksData;
}]);