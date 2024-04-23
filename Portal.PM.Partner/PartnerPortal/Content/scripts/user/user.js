function FetchEscapiaOwner(escapiaOwnerId) {
    console.log('in FetchEscapiaOwner', escapiaOwnerId)

    $.get("/Users/FetchEscapiaOwner?id=" + escapiaOwnerId).then(
        function (data) {
            return data;
        },
        function () {
           toastr.error("There was a problem fetching this user.  Please try again.", "Oops, an error has occured.");
           return null;
        }
    );

}



app.controller('UserController', ['$scope', '$http', '$uibModal', function ($scope, $http, $uibModal) {

    var userCtl = this;
    userCtl.users = [];
    userCtl.usersOG = [];
    userCtl.user = [];
    userCtl.userTypes = [];
    userCtl.userType = {value: "0",name: "All"}
    userCtl.userSearch = '';
    userCtl.loading = true;
    userCtl.loadUsers = function () {
        userCtl.loading = true;
        FetchUsers();
    };

    $scope.getIconClass = function (value) {
        //console.log("ng-check icon value: " + value);
        if (value)
            return "fas fa-check-circle text-success";
        else
            return "fas fa-times-circle text-danger";
    };

    $scope.gridFilter = function () {
        console.log('gridFilter')
        userCtl.users = userCtl.usersOG;
        userCtl.users = FilterType(userCtl.users);
    };


    userCtl.editUser = function (user, userTypes) {
        var modalInstance = $uibModal.open({
            templateUrl: "/Users/UserModal",
            controller: "UserEditController as userEditCtl",
            size: 'lg',
            // Use resolve to pass data to the
            // modal controller.  Here we pass the
            // selected user object.
            resolve: {
                user: function () {
                    return user;
                },
                userTypes: function () {
                    return userTypes;
                }
            }
        });

        modalInstance.result.then(function (user) {
            //This is where data is passed back to this controller
            //from the modal controller when the "OK" button is clicked.
            // alert(user.userNumber);
        }).catch(function (res) {
            // Add error handling here.
            // console.log(res);
        });

    };

     userCtl.newUser = function (user, userTypes) {
         var modalInstance = $uibModal.open({
             templateUrl: "/Users/UserNewModal",
             controller: "UserEditController as userEditCtl",
             size: 'lg',
             // Use resolve to pass data to the
             // modal controller.  Here we pass the
             // selected user object.
             resolve: {
                 user: function () {
                     return user;
                 },
                 userTypes: function () {
                     return userTypes;
                 }
             }
         });

         modalInstance.result.then(function (user) {
             //This is where data is passed back to this controller
             //from the modal controller when the "OK" button is clicked.
             // alert(user.userNumber);
         }).catch(function (res) {
             // Add error handling here.
             // console.log(res);
         });

     };


    FetchUsers = function () {
        console.log('FetchUsers');
        $http.post("/Users/FetchUsers")
            .then(function (response) {
                userCtl.users = response.data;
                userCtl.usersOG = response.data;
                userCtl.loading = false;
            },
            function (err) {
            userCtl.loading = false;
        });
    };

    FetchUserTypes = function () {
        console.log('FetchUserTypes');
        $http.get("/Users/FetchUserTypes")
            .then(function (response) {
                    userCtl.userTypes = response.data;
                    console.log('userCtl.userTypes', userCtl.userTypes);
                },
                function (err) {
                    // userCtl.loading = false;
                });
    };

    FilterType = function (users) {
        userCtl.userSearch = '';
        if (userCtl.userType.value == '0')
            return users;
        return users.filter(item => item.UserType == userCtl.userType.value);
    };

    userCtl.loadUsers();
    FetchUserTypes();

}]);
