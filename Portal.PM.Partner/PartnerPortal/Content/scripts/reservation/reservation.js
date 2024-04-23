app.controller('ReservationController', ['$scope', '$http', '$uibModal', function ($scope, $http, $uibModal) {

    var reservationCtl = this;
    reservationCtl.reservationsOG = [];
    reservationCtl.reservations = [];
    reservationCtl.reservation = [];
    reservationCtl.stayTypes = [];
    reservationCtl.paymentStatuOptions = [{
        value: "allpaid",
        name: "All Payment Status"
    }, {
        value: "fullypaid",
        name: "Fully Paid"
    }, {
        value: "unpaid",
        name: "Unpaid"
    }];
    reservationCtl.loading = true;
    reservationCtl.tabtask = 'arriving';
    reservationCtl.showHold = false;
    reservationCtl.showHistoricReservation = false;
    reservationCtl.showOwnerStays = false;
    reservationCtl.showStayLegend = false;
    reservationCtl.reloadingReservationData = false;
    reservationCtl.paymentStatus = 'allpaid';
    reservationCtl.loadReservations = function () {
        let formdata = InitializeForm(reservationCtl.tabtask, null, null, null, null, null, reservationCtl.showHistoricReservation);
        FetchReservations(formdata);
    };
    reservationCtl.reservationRefreshedOn = [];

    reservationCtl.editReservation = function (reservation) {
        var modalInstance = $uibModal.open({
            templateUrl: "/Reservation/ReservationModal",
            controller: "ReservationEditController as reservationEditCtl",
            size: 'lg',
            // Use resolve to pass data to the
            // modal controller.  Here we pass the
            // selected reservation object.
            resolve: {
                reservation: function () {
                    return reservation;
                }
            }
        });

        modalInstance.result.then(function (reservation) {
            //This is where data is passed back to this controller
            //from the modal controller when the "OK" button is clicked.
            // alert(reservation.ReservationNumber);
        }).catch(function (res) {
            // Add error handling here.
            // console.log(res);
        });

    };

    reservationCtl.getBadgeCounts = function () {
        $http.get("/Dashboard/GetBadgeCounts").
        then(function (response) {
                var badges = response.data.Counts;
                reservationCtl.reservationRefreshedOn = badges.ReservationRefreshedOn;
            },
            function (err) {
                console.log('failed to get badge counts');
            });
    };



    $scope.getIconClass = function (value) {
        if (value)
            return "fas fa-check-circle fa-lg text-success";
        else
            //return "fas fa-exclamation-triangle fa-lg text-warning";
            return "fas fa-times-circle fa-lg text-danger";
    };
    $scope.getCheckAlertClass = function (value) {
        if (value)
            return "alert alert-success";
        else
            return "alert alert-danger";
    };

    $scope.getReservation = function (reservationNumber) {
        reservationCtl.reservation = reservationCtl.reservations.filter(function (el) {
            return el.ReservationNumber === reservationNumber;
        })[0];
        let mdl = $("#mdl-reservation-edit");
        mdl.modal("show");
    };

    $scope.loadReservations = function (task) {
        document.getElementById('frmSearch').value = '';
        select_box = document.getElementById("frmPaymentStatus");
        select_box.selectedIndex = 0;
        reservationCtl.tabtask = task;
        reservationCtl.loadReservations();
    };

    $scope.searchReservations = function () {
        $('a.resv-tab').removeClass('active');
        $('#tab-all').addClass('active');

        var frmSeach = angular.element(document.getElementById("frmSearch")).val();
        var frmPaymentStatus = angular.element(document.getElementById("frmPaymentStatus")).val();
        reservationCtl.tabtask = "all";
        let formdata = InitializeForm(reservationCtl.tabtask, reservationCtl.frmStartDate, null, null, frmSeach, frmPaymentStatus, reservationCtl.showHistoricReservation);
        FetchReservations(formdata);
    };

    $scope.gridFilter = function (term) {
        reservationCtl.reservations = reservationCtl.reservationsOG;
        reservationCtl.reservations = FilterShowHold(reservationCtl.reservations);
        reservationCtl.reservations = FilterPaymentStatus(reservationCtl.reservations);
        reservationCtl.reservations = FilterStayType(reservationCtl.reservations);
    };

    $scope.reloadReservationData = function () {
        if (reservationCtl.reloadingReservationData) {
            alert('Force Reservation already started .... ');
            return;
        }

        var strMsg = "Refresh was last run " + reservationCtl.reservationRefreshedOn + ".\nForce reservation data re-load?"
        if (confirm(strMsg)) {
            ReloadReservationFromAPI();
        }

    };

    FilterByReservationNumber = function (reservationnumber) {
        var reservations = [];
        reservations = reservationCtl.reservationsOG.filter(item => item.ReservationNumber === reservationnumber);
        return reservations;
    };


    FilterShowHold = function (reservations) {
        var results = reservations.filter(item => item.ReservationNumber.startsWith("RES-"));

        if (reservationCtl.showHold) {
            results = reservations.filter(item => item.ReservationNumber.startsWith("HLD-"));
        }
        return results;
    };


    FilterPaymentStatus = function(reservations) {

        if (reservationCtl.paymentStatus === 'allpaid')
            return reservations;

        var paymentstatusfull = reservationCtl.paymentStatus === 'fullypaid';
        return reservations.filter(item => item.PaymentStatusFull === paymentstatusfull);
    };

    FilterStayType = function (reservations) {
        if (reservationCtl.showOwnerStays)
            return reservations.filter(item => item.Reservation.ReservationType == 'Owner');

        return reservations.filter(item => item.Reservation.ReservationType != 'Owner');
    };

    FetchStayType = function () {
        return $http.get("Reservation/FetchReservationTypes")
            .then(function (response) {
                reservationCtl.stayTypes = [];
                if (response.data) {
                    reservationCtl.stayTypes = response.data;
                }
            });
    };

    FetchReservationByNumber = function (reservationNumber) {
        return $http.get("Reservation/FetchReservationJson?reservationNumber=" + reservationNumber)
        .then(function (response) {
            reservationCtl.reservation = null;
            if (response.data.ReservationNumber) {
                reservationCtl.reservation = response.data;
            }
        });
    };

    FetchReservations = function(formdata) {

        reservationCtl.loading = true;
        reservationCtl.reservationsOG = [];
        reservationCtl.reservation = [];
        reservationCtl.reservations = [];

        var fetchedReservations = $http.post("Reservation/FetchReservationsJson", formdata)
        .then(function (response) {
            reservationCtl.reservationsOG = response.data;
            reservationCtl.reservations = reservationCtl.reservationsOG;
        },
        function (error) {
                return null;
        });

        fetchedReservations.then(function () {
           if (reservationCtl.reservations.length > 0){
               $scope.gridFilter('frmShowHold');
               reservationCtl.loading = false;
               return;
           }

           if (reservationCtl.reservations.length === 0) {
               var fetchedReservation = FetchReservationByNumber(formdata.custom.searchterm);
               fetchedReservation.then(function () {
                   if (reservationCtl.reservation) {
                       reservationCtl.reservations.push(reservationCtl.reservation);
                       $scope.gridFilter('frmShowHold');
                       reservationCtl.loading = false;
                        return;
                   }

                   if (!reservationCtl.reservation) {
                       reservationCtl.loading = false;
                       return;
                   }
               });
           }
        });

    };

    ReloadReservationFromAPI = function() {
        reservationCtl.reloadingReservationData = true;
        $http.get("/Reservation/FetchReservationsEscapiaJson")
            .then(function (response) {
                   reservationCtl.reloadingReservationData = false;
                   reservationCtl.getBadgeCounts();
                },
                function (err) {
                    // reservationCtl.loading = false;
                });
    };


    function InitializeForm(task, startdate, enddate, dateRangeSearchMethod, searchterm, paymentstatus, showHistoricReservation) {

        // API > dateRangeSearchMethod 'Overlap', 'StartDate', 'EndDate'
        if (!dateRangeSearchMethod)
            dateRangeSearchMethod = 'StartDate';

        dateRangeSearchMethod = SetDateRangeSearchMethod(task);
        var objDaterange = SetDateRange(task, startdate, enddate, showHistoricReservation);

        return {
            specification: {
                staydaterangesearchspecification: {
                    daterange: {
                        startdate: objDaterange.startdate,
                        enddate: objDaterange.enddate
                    },
                    dateRangeSearchMethod: dateRangeSearchMethod
                }
            },
            custom: {
                task: task,
                searchterm: searchterm,
                paymentstatus: paymentstatus
            },
            pagesize: 250,
            pagenumber: 1
        };
    }

    function SetDateRangeSearchMethod(task) {

        var searchMethod = 'StartDate';

        if (task === "all") {
            searchMethod = 'StartDate';
        }
        if (task === "arriving") {
            searchMethod = 'StartDate';
        }
        if (task === "inhome") {
            searchMethod = 'StartDate';
        }
        if (task === "departed") {
            searchMethod = 'StartDate';
        }

        return searchMethod;
    }


    function SetDateRange(task, startdate, enddate, showHistoricReservation) {

        let daterange = {
            startdate: startdate,
            enddate: enddate
        };

        if (task === "all") {
            daterange.startdate = moment().subtract(3, 'month');
            daterange.enddate = moment().add(10, 'year');
        }

        if (task === "all" && showHistoricReservation) {
            daterange.startdate = moment().subtract(6, 'year');
            daterange.enddate = moment().add(10, 'year');
        }

        if (task === "arriving") {
            daterange.startdate = moment().startOf('day');
            daterange.enddate = moment().add(15, 'day');
        }

        if (task === "inhome") {
            daterange.startdate = moment().subtract(7, 'day');
            daterange.enddate = moment().add(30, 'day');
        }

        if (task === "departed") {
            daterange.startdate = moment().subtract(15, 'day');
            daterange.enddate = moment().subtract(1, 'day');
        }

        if (startdate) {
            daterange.startdate = moment(startdate).startOf('day');
            daterange.enddate = moment(startdate).add(1, 'day');
        }

        daterange.startdate = daterange.startdate.format("MM/DD/YYYY");
        daterange.enddate = daterange.enddate.format("MM/DD/YYYY");

        return daterange;

    }

    $scope.datePickerOptions = {
        singleDatePicker: true
    };

    reservationCtl.loadReservations();
    reservationCtl.getBadgeCounts();
    FetchStayType();

}]);
