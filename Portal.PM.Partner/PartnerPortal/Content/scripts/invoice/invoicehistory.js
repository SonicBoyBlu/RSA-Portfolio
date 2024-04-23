 app.controller('InvoiceHistoryController', ['$scope', '$http', function ($scope, $http) {

        var invoiceCtl = this;
        invoiceCtl.workOrders = [];
        invoiceCtl.loading = false;

        invoiceCtl.date = {
            startDate: moment().startOf('year'),
            endDate: moment().endOf('year')
        };

        invoiceCtl.loadInvoices = function () {
            invoiceCtl.loading = true;
            $http.get("/WorkOrders/GetUploadedInvoices?startDate=" + invoiceCtl.date.startDate.toJSON() + "&endDate=" + invoiceCtl.date.endDate.toJSON()).
                then(function (response) {
                    invoiceCtl.workOrders = response.data.workOrders;
                    invoiceCtl.loading = false;
                },
                    function (err) {
                        invoiceCtl.loading = false;
                    });
        };

        invoiceCtl.dateRangePickerOptions = {
            locale: {
                format: 'MM/DD/YYYY'
            },
            eventHandlers: {
                'apply.daterangepicker': function (event, picker) { invoiceCtl.loadInvoices(); }
            }
        };

        invoiceCtl.uploadedTotal = function () {
            var total = 0;
            for (var i = 0; i < invoiceCtl.workOrders.length; i++) {
                total += invoiceCtl.workOrders[i].TotalAmount;
            }
            return total;
        };

        invoiceCtl.uploadedCountTotal = function () {
            var total = 0;
            for (var i = 0; i < invoiceCtl.workOrders.length; i++) {
                total += invoiceCtl.workOrders[i].Count;
            }
            return total;
        };

        //invoiceCtl.search = function (row) {
        //    var lowerQuery = invoiceCtl.query.toLowerCase();
        //    return (row.InvoiveNum.toLowerCase().indexOf(lowerQuery || '') !== -1 ||
        //            row.CustomerName.toLowerCase().indexOf(lowerQuery || '') !== -1);
        //};

        invoiceCtl.loadInvoices();
    }]);
