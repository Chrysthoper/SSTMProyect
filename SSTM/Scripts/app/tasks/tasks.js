angular.module('TasksApp', ['ngMaterial', 'TaskService'])
.controller('TasksController', function ($scope, TasksData) {
    $scope.tasks = [];

    $scope.currentState = {};

    TasksData.GetAll().
        success(function (data) {
            $scope.tasks = data;
            $('#div-loader').hide();
        }).
        error(function (data) {
            console.log(data);
            $('#div-loader').hide();
        });

    $scope.states = [];

    TasksData.GetTasksStates().
        success(function (data) {
            for (var key in data) {
                var s = { value: data[key], key: key };
                $scope.states.push(s);
            }
            $scope.states.unshift({ value: 'Show All', key: -1 });
        }).
        error(function (data) {
            console.log(data);
        });

    $scope.filterCurrentState = function (element) {
        if ($scope.currentState == -1)
            return true;
        return (element.currentState == $scope.currentState);
    };

    

});