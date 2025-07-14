let reservationRefreshedOn = null;

app.controller('NavigationController', ['$scope', '$http', '$uibModal', function ($scope, $http, $uibModal) {

    var ctl = this;
    ctl.badges = { ActionItemsCount: 0, TaskCount: 0, ReservationRefreshedOn:null };

    ctl.getBadgeCounts = function () {
        $http.get("/Dashboard/GetBadgeCounts").
            then(function (response) {
                ctl.badges = response.data.Counts;
                reservationRefreshedOn = ctl.badges.ReservationRefreshedOn;
            },
                function (err) {
                    console.log('failed to get badge counts');
                });
    };


    ctl.createActionItem = function () {
        var modalInstance = $uibModal.open({
            templateUrl: "/ActionItems/NewActionItemModal",
            controller: "CreateActionItemController",
            size: 'lg'
            /*
            opened: new Promise(function (resolve, reject) {
                Utilities.Modals.render.wrapper();
            })
            */
        });

        modalInstance.result.then(function () {
            ctl.getBadgeCounts();
        });
    };



    ctl.getBadgeCounts();





}]);
