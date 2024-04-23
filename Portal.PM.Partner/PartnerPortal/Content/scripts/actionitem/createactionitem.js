app.controller('CreateActionItemController', ['$rootScope', '$scope', '$uibModalInstance', 'Upload', '$timeout', function ($rootScope, $scope, $uibModalInstance, Upload, $timeout) {
    $scope.subject = '';
    $scope.message = '';
    $scope.selectedFiles = [];
    $scope.showSuccessMessage = false;

    $scope.ok = function () {
        $uibModalInstance.close();
    };

    $scope.cancel = function () {
        $uibModalInstance.dismiss();
    };

    $scope.createActionItem = function () {

        if (!$scope.canCreate()) {
            toastr.error("Please enter a subject and message body.");
            return;
        }

        var actionItem = {
            Subject: $scope.subject,
            Body: $scope.message
        };

        Upload.upload({
            url: "/ActionItems/CreateActionItem",
            data: {
                files: $scope.selectedFiles,
                ticket: actionItem
            }
        }).then(function (response) {
            $timeout(function () {
                //$scope.subject = '';
                $scope.message = '';
                $scope.selectedFiles = [];
                $rootScope.$broadcast('actionitem-created', { actionItem: response.data.ActionItem });
                toastr.success("Action item - " + $scope.subject + " has been created.");
                $scope.showSuccessMessage = true;
                $scope.timeout = $timeout($scope.closeModal, 1000);
            });
        }, function (response) {
            handleJsonError(response);
        });

    };

    $scope.canCreate = function () {

        return $scope.message !== '' && $scope.subject !== '';
    };

    $scope.closeModal = function () {
        $uibModalInstance.close();
    };
    

    $scope.selectFiles = function (files) {
        $scope.selectedFiles = files;
    };

    $scope.removeFile = function (file) {
        for (var i = 0; i < $scope.selectedFiles.length; i++) {
            if ($scope.selectedFiles[i].name === file.name) {
                $scope.selectedFiles.splice(i, 1);
            }
        }
    };
}]);
