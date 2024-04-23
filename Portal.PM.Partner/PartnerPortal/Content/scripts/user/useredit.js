app.controller('UserEditController', ['$scope', '$http', '$uibModalInstance', 'user', 'userTypes',
    function ($scope, $http, $uibModalInstance, user, userTypes) {

        $scope.user = user;
        $scope.userTypes = userTypes;
        $scope.userType = {value: "0",name: "All"}
        var userEditCtl = this;
        userEditCtl.isLoading = true;
        $scope.showPwUpdate = false;
        $scope.showPwSend = false;
        $scope.password1 = "";

        //New User Setup
        $scope.userTypes[0].name = "Select one"
        $scope.showOwnerForm = false;

        if (user && user.ExternalApplications.Escapia) {
            userEditCtl.isLoading = true;
            var escapiaOwnerId = user.ExternalApplications.Escapia.id;
            console.log('escapiaOwnerId', escapiaOwnerId)
            var escapiaOwner = FetchEscapiaOwner(escapiaOwnerId);
            // console.log('func FetchEscapiaOwner escapiaOwner', escapiaOwner);
            userEditCtl.isLoading = false;
        }


        $scope.getIconClass = function (value) {
            //console.log("ng-check icon value: " + value);
            if (value)
                return "fas fa-check-circle text-success";
            else
                return "fas fa-times-circle text-danger";
        };



        $scope.ok = function () {
            $uibModalInstance.close($scope.user);
        };

        $scope.cancel = function () {
            $uibModalInstance.dismiss('cancel');
        };

        $scope.resetUserEdit = function () {
            this.showPwUpdate = false;
            this.showPwSend = false;
            this.password1 = "";
        }

        $scope.pwupdateshow = function () {
            var stShowPwUpdate = this.showPwUpdate;
            this.resetUserEdit();
            this.showPwUpdate = !stShowPwUpdate;
        };

        $scope.pwrandomize = function () {
            this.password1 = GenerateRandomPassword();
        };

        $scope.pwsendshow = function () {
            var stShowPwSend = this.showPwSend;
            this.resetUserEdit();
            this.showPwSend = !stShowPwSend;
        };

        $scope.pwupdate = function () {
            console.log('pwupdate');
            var newpassword = this.password1;
            this.resetUserEdit();
            UserUpdatePassword(this.user, newpassword);
            $uibModalInstance.dismiss('cancel');
        };

        $scope.pwsendreset = function () {
            console.log('pwsendreset');
            this.resetUserEdit();
            SendPasswordRest(this.user);
            $uibModalInstance.dismiss('cancel');
        };

        $scope.onchangeType = function () {
            console.log('userType',this.userType.value)
            console.log('userType', (this.userType.value == 2))
            this.showEmployeeForm = (this.userType.value == 1)
            this.showOwnerForm = (this.userType.value == 2)
            this.showVendorForm = (this.userType.value == 3)
        }

        $scope.clickSaveUser = function () {
            console.log('clickSaveUser');
            console.log('clickSaveUser', this.user);
            this.user.UserTypeID = this.userType.value;
            var userdata = {
                userid: 0,
                lastname: this.user.LastName,
                firstname: this.user.FirstName,
                email: this.user.Email,
                usertypeid: this.user.UserTypeID,
                password: this.user.Password,
                ipaddress: "",
                bambooid: 0,
            };

            console.log('userdata', userdata);

            $http.post("/Users/CreateUser", userdata)
                .then(function (response) {
                        console.log('response.data', response.data)
                    },
                    function (err) {
                        // reservationCtl.loading = false;
                    });

        }


        SendPasswordRest = function (user) {
            $http.post("/SendResetPassword/", {
                username: user.Email
            })
                .then(function (response) {
                    console.log('response.data', response.data)
                    },
                    function (err) {
                        reservationCtl.loading = false;
                    });
        };

        UserUpdatePassword = function (user, newpassword) {
            var userdata = {
                userid: user.UserID,
                lastname: user.LastName,
                firstname: user.FirstName,
                email: user.Email,
                usertypeid: user.UserTypeID,
                password: newpassword,
                ipaddress: "",
                bambooid: user.BambooHrUserID,
            };
            $http.post("/Users/UpdatePassword", userdata)
                .then(function (response) {
                        console.log('response.data', response.data)
                    },
                    function (err) {
                        reservationCtl.loading = false;
                    });
        };




    }
]);

function GenerateRandomPassword() {
    var length = 8,
        charset = "ABCDEFGHIJKLMNPQRSTUVWXYZ123456789",
        result = "";
    for (var i = 0, n = charset.length; i < length; ++i) {
        result += charset.charAt(Math.floor(Math.random() * n));
    }
    return result;
}
