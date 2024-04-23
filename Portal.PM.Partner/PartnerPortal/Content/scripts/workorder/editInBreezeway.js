app.controller('EditInBreezewayController', ['$scope', '$uibModalInstance', 'invoice', function ($scope, $uibModalInstance, invoice) {

    $scope.invoice = invoice;

    $scope.ok = function () {
        $uibModalInstance.close($scope.invoice);
    };

    $scope.cancel = function () {
        $uibModalInstance.dismiss();
    };
}]);
