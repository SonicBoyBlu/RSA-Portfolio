var app = angular.module('partnerPortal', ['daterangepicker', 'ui.bootstrap', 'ui.mask', 'ngSanitize', 'ngFileUpload']);

var handleJsonError = function (response) {
    if (response.data && response.data.Error) {
        toastr.error('Error', response.data.Error);
    }
    else {
        toastr.error('Error', "An unspecified error occured, request could not be completed");
    }
};