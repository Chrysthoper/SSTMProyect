angular.module('TasksApp', ['ngMaterial', 'TaskService'])
.controller('TasksController', function ($scope, TasksData) {
    $scope.tasks = [];

    TasksData.GetAll().
        success(function (data) {
            $scope.tasks = data;
        }).
        error(function (data) {

        });

});