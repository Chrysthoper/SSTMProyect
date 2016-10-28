angular.module('UsersApp', ['ngMaterial', 'UserService', 'TaskService'])
.controller('UsersCtrl', function ($scope, UsersData, $mdDialog, TasksData) {
    $scope.users = [];
    
    UsersData.GetAll().
        success(function (data) {
            $('#div-loader').hide();
            $scope.users = data.map(function (user) {
                user.image = '/Content/Images/user-default.png';
                return user
            });
        }).
        error(function (data) {
            console.log(data);
            $('#div-loader').hide();
        });

    
    $scope.showDialogUsers = function (ev, UserID) {
        
        TasksData.GetModalForTask().
            success(function (data) {

                alert = $mdDialog.alert({
                    parent: angular.element(document.body),
                    targetEvent: ev,
                    template: data,
                    bindToController: true,
                    controller: 'TasksCtrl',
                    locals: {
                        UserID: UserID
                    }
                });

                $mdDialog
                    .show(alert)
                    .finally(function () {
                        alert = undefined;
                    });
            }).
            error(function (data) {
                console.log(data);
            });

        $scope.closeDialog = function () {
            $mdDialog.hide();
        };

    };

    
})
.controller('TasksCtrl', function ($scope, $mdDialog, TasksData, locals) {
    $scope.UserID = locals.UserID;
    $scope.tasks = [];
    $('#div-loader').show();
    TasksData.GetAllFromUser($scope.UserID).
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
});;