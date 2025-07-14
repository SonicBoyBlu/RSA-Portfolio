app.controller('ReservationEditController', ['$scope', '$http' ,'$uibModalInstance', 'reservation',
function ($scope, $http, $uibModalInstance, reservation) {

    $scope.reservation = reservation;
    var reservationEditCtl = this;
    reservationEditCtl.isLoading = true;
    reservationEditCtl.hasError = false;
    reservationEditCtl.showContent = function () {
        return reservationEditCtl.hasError || reservationEditCtl.isLoading ? false : true;
    };

    $scope.isLoading = true;

    if (!reservation.Unit) {
        reservationEditCtl.isLoading = true;
        reservationEditCtl.hasError = false;
        $http.get("Reservation/FetchReservationJson?reservationNumber=" + reservation.ReservationNumber)
            .then(function (response) {
                let data = response.data;
                // Update obj with full data
                reservation.Unit = data.Unit;
                reservation.Notes = data.Notes;
                reservation.Reservation = data.Reservation;
                reservation.PalmSpringsContractNumberOptions = data.PalmSpringsContractNumberOptions;
                reservationEditCtl.isLoading = false;
                // Update @scope
                $scope.reservation = reservation;
            },
            function (err) {
                toastr.error("There was a problem fetching this reservation.  Please try again.", "Oops, an error has occured.");
                reservationEditCtl.isLoading = false;
                reservationEditCtl.hasError = true;
            });
    }

    $scope.updateDashboard = function (element) {
        console.log('element', element);
    };

    $scope.ok = function () {
        $uibModalInstance.close($scope.reservation);
    };

    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };

    $scope.getIconClass = function (value) {
        if (value === true)
            return "fas fa-check-circle fa-lg text-success";
        else
            return "fas fa-times-circle fa-lg text-danger";
    };
    $scope.getCheckAlertClass = function (value) {
        //console.log("ng-check label value: " + value);
        if (value)
            return "alert alert-success";
        else
            return "alert alert-danger";
    };

    $scope.$watch('reservation', function () {
        var isDirty = $scope.formReservatioEdit.$dirty;
        if (!isDirty)
            return;

        //reservation.Reservation.GuestArrivalInstructionsSent = moment(reservation.Reservation.GuestArrivalInstructionsSent);
        //reservation.Reservation.ConciergeNotified = moment(reservation.Reservation.ConciergeNotified);

        reservation.SentArrivalInstructions = moment(reservation.Reservation.GuestArrivalInstructionsSent).isValid();
        reservation.HasConcierge = moment(reservation.Reservation.ConciergeNotified).isValid();

        //reservation.SentArrivalInstructions = reservation.Reservation.GuestArrivalInstructionsSent.isValid();
        //reservation.HasConcierge = reservation.Reservation.ConciergeNotified.isValid();

        reservation.IsPSCityContract = (reservation.Reservation.PSCitySummaryIdNumber && reservation.Reservation.PSCitySummaryIdNumber.length > 2);
        reservation.UploadGuestIdentification = reservation.Reservation.UploadGuestIdentification;


        $http.post("/Reservation/UpdateReservation", reservation)
        .then(function (response) {
            //
        },
        function (err) {
            //
        });

    }, true);

    $scope.datePickerOptions = {
        singleDatePicker: true
    };

}]);
