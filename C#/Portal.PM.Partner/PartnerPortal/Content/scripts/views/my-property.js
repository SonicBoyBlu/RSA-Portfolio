app.controller('MyProperty', ['$scope', '$http', '$uibModal', function ($scope, $http, $uibModal) {

    var propCtl = this;
    propCtl.owner = [];
    propCtl.units = [];
    propCtl.unit = [];
    propCtl.loading = true;

    $scope.getIconClass = function (value) {
        if (value)
            return "fas fa-check-circle fa-lg text-success";
        else
            //return "fas fa-exclamation-triangle fa-lg text-warning";
            return "fas fa-times-circle fa-lg text-danger";
    };


    FetchOwner = function () {
        propCtl.owner = JSON.parse(sessionStorage.getItem("user"));
    };

    FetchUnits = function () {
        var jsonData = JSON.parse(sessionStorage.getItem("user"));
        propCtl.units = jsonData.ExternalApplications.Escapia.Unit;
        propCtl.loading = false;
    };

    FetchOwner();
    FetchUnits();

}]);
