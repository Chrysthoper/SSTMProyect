angular.module('NewTaskApp', ['ngMaterial', 'ngMessages', 'TaskService', 'UserService'])
  .controller('NewTaskCtrl', function ($scope, TasksData, $timeout, $q, $log, UsersData, $mdToast) {
  
    $scope.currentState = {};

    $scope.states = [];

    TasksData.GetTasksStates().
        success(function (data) {
            for (var key in data) {
                var s = { value: data[key], key: key };
                $scope.states.push(s);
            }
        }).
        error(function (data) {
            console.log(data);
        });

    function showSimpleToast() {
        $mdToast.show($mdToast.simple().textContent('Task Created!!!').position('bottom left'));
    };

    function NewTask() {
        $scope.currentState.key = 0;
        $scope.assignedToUser = null;
        $scope.assignedByUser = null;
        $scope.assignedTo = null;
        $scope.assignedBy = null;
        return {
            name: '',
            description: '',
            startDate: '',
            dueDate: '',
            completeDate: '',
            currentState: 0,
            currentProgress: 0,
            assignedBy: '',
            assignedTo: '',
            comments: ''
        };
    }
    
    $scope.task = NewTask();

    $scope.createNewTask = function (isValid) {
        if (isValid) {
            $('#div-loader').show();
            $scope.task.assignedBy = $scope.assignedByUser.id;
            $scope.task.assignedTo = $scope.assignedToUser.id;
            $scope.task.startDate = ($scope.task.startDate == '') ? new Date(1900, 1, 1) : $scope.task.startDate;
            $scope.task.dueDate = ($scope.task.dueDate == '') ? new Date(1900, 1, 1) : $scope.task.dueDate;
            $scope.task.completeDate = ($scope.task.completeDate == '') ? new Date(1900, 1, 1) : $scope.task.completeDate;
            TasksData.Create($scope.task).
            success(function (data) {
                $scope.task = NewTask();

                $('#div-loader').hide();
                showSimpleToast();
                console.log(data);
            }).
            error(function (data) {
                $('#div-loader').hide();
                console.log(data);
            });
        }
        else
            $mdToast.show($mdToast.simple().textContent('Something went wrong, please check the info').position('bottom left'));
    }

      //////AUTOCOMPLETE
      var self = this;
      self.simulateQuery = false;
      self.isDisabled = false;
      // list of states to be displayed
      $scope.users = []
      
      loadUsers();
      
      $scope.querySearch = function(query) {
          var results = query ? $scope.users.filter(createFilterFor(query)) : $scope.users, deferred;
          if (self.simulateQuery) {
              deferred = $q.defer();
              $timeout(function () {
                  deferred.resolve(results);
              },
                 Math.random() * 1000, false);
              return deferred.promise;
          } else {
              return results;
          }
      }


      $scope.assignedToUserChange = function (item) {
          $scope.assignedToUser = item;
          $log.info('Item changed to ' + JSON.stringify(item));
      }

      $scope.assignedByUserChange = function (item) {
          $scope.assignedByUser = item;
          $log.info('Item changed to ' + JSON.stringify(item));
      }

      //build list of states as map of key-value pairs
      function loadUsers() {
          UsersData.GetAll().
            success(function (data) {
                $scope.users = data.map(function (user) {
                    return {
                        id: user.id,
                        value: user.name.toLowerCase(),
                        display: user.name,
                        email: user.email,
                        image: '/Content/Images/user-default.png'
                    };
                });
            }).
            error(function (data) {
                console.log(data);
            });
          
      }
      //filter function for search query
      function createFilterFor(query) {
          var lowercaseQuery = angular.lowercase(query);
          return function filterFn(state) {
              return (state.value.indexOf(lowercaseQuery) === 0);
          };
      }

      $('#div-loader').hide();
  });

