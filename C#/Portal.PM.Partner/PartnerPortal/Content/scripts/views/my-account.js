app.controller('MyAccount', ['$scope', '$http', '$uibModal', function ($scope, $http, $uibModal) {

    var accountCtl = this;
    accountCtl.account = [];
    accountCtl.loading = true;

    $scope.getIconClass = function (value) {
        if (value)
            return "fas fa-check-circle fa-lg text-success";
        else
            //return "fas fa-exclamation-triangle fa-lg text-warning";
            return "fas fa-times-circle fa-lg text-danger";
    };


    FetchAccount = function () {
        accountCtl.account = JSON.parse(sessionStorage.getItem("user"));
        accountCtl.loading = false;
    };

    FetchAccount();

}]);
