angular.module('LoginApp', [])
.controller('LoginController', function ($scope, $http, $window) {

    $scope.ShowError = false;
    $scope.EmailSend = false;
    $scope.ShowErrorRegistration = false;
    
    $scope.authentication = function () {

        $('#div-loader').show();

        $http.post('/Login/Authentication', $scope.formData)
            .success(function (data) {
                console.log(data);
                $window.location.href = '/';
            })
            .error(function (data) {
                $('#div-loader').hide();
                $scope.alert = data;
                $scope.ShowError = true;
                console.log('Error:' + data);
            });
    }

    $scope.register = function (isValid) {
        $('#div-loader').show();
        if (isValid) {
            $http.post('/Login/Registration', $scope.form)
            .success(function (data) {
                $('#div-loader').hide();
                console.log(data);
                $scope.EmailSend = true;
                $scope.ShowErrorRegistration = false;
                $scope.ShowError = false;
            })
            .error(function (data) {
                $('#div-loader').hide();
                $scope.alertRegistration = data;
                $scope.EmailSend = false;
                $scope.ShowErrorRegistration = true;
                $scope.ShowError = false;
                console.log('Error:' + data);
            });
        }
        else {
            $('#div-loader').hide();
            $scope.alertRegistration = 'Please Verify the information';
            $scope.ShowErrorRegistration = true;
        }
            
    }
    
    $('#div-loader').hide();

});