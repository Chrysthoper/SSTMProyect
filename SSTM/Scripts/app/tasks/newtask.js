angular.module('NewTaskApp', ['ngMaterial', 'ngMessages', 'TaskService'])
  .controller('NewTaskCtrl', function ($scope, TasksData) {
  
      newTask = {
          name: '',
          description: '',
          startDate: '',
          dueDate: '',
          completeDate: '',
          currentState: 0,
          currentProgress: 0,
          assignedBy: 0,
          assignedTo: 0,
          comments: ''
      };
      $scope.task = newTask;

      $scope.createNewTask = function () {
          $scope.task.startDate = ($scope.task.startDate == '') ? new Date(1900, 1, 1) : $scope.task.startDate;
          $scope.task.dueDate = ($scope.task.dueDate == '') ? new Date(1900, 1, 1) : $scope.task.dueDate;
          $scope.task.completeDate = ($scope.task.completeDate == '') ? new Date(1900, 1, 1) : $scope.task.completeDate;
          TasksData.Create($scope.task).
            success(function (data) {
                $scope.task = newTask;
                console.log(data);
            }).
            error(function (data) {
                console.log(data);
            });
      }

  });

