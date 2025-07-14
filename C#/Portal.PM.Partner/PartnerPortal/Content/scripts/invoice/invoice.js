app.controller('InvoiceController', ['$scope', '$http', function ($scope, $http) {

        var invoiceCtl = this;
        invoiceCtl.invoices = [];
        invoiceCtl.uploadedInvoices = [];
        invoiceCtl.errorInvoices = [];
        invoiceCtl.authenticated = false;
        invoiceCtl.loading = false;
        invoiceCtl.batchSize = 1;
        invoiceCtl.threadCount = 1;
        invoiceCtl.batchInvoices = [];
        invoiceCtl.uploading = false;
        invoiceCtl.batchesSent = 0;
        invoiceCtl.uploadedInvoicesCount = 0;
        invoiceCtl.canceled = false;
        invoiceCtl.query = '';

        invoiceCtl.date = {
            startDate: moment().startOf('month'),
            endDate: moment().endOf('month')
        };

        invoiceCtl.loadInvoices = function () {
            invoiceCtl.loading = true;
            $http.get("WorkOrders/GetInvoicesForImport?startDate=" + invoiceCtl.date.startDate.toJSON() + "&endDate=" + invoiceCtl.date.endDate.toJSON()).
                then(function (response) {
                    invoiceCtl.invoices = response.data.invoices;
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

        invoiceCtl.getQuickBookStatus = function () {
            $http.get("QuickBooks/QuickBookStatus").
                then(function (response) {
                    invoiceCtl.authenticated = response.data.authenticated;
                },
                    function (err) {
                    });
        };

        invoiceCtl.verifyItems = function () {
            $http.get("QuickBooks/VerifyItems").
                then(function (response) {
                    invoiceCtl.verifyErrors = response.data.results;
                },
                    function (err) {
                    });
        };


        invoiceCtl.uploadInvoices = function () {
            invoiceCtl.batchInvoices = invoiceCtl.getSelectedInvoices();
            invoiceCtl.uploading = true;
            invoiceCtl.invoicesToUploadCount = invoiceCtl.batchInvoices.length;
            invoiceCtl.errorInvoices = [];
            invoiceCtl.batchesSent = 0;
            invoiceCtl.uploadedInvoicesCount = 0;
            invoiceCtl.uploadInvoiceBatch();
            invoiceCtl.canceled = false;
            invoiceCtl.uploadPercentage = 0;
        };

        invoiceCtl.uploadInvoiceBatch = function () {

            for (var i = 0; i < invoiceCtl.threadCount && invoiceCtl.batchInvoices.length > 0; i++) {
                var invoicesToUpload = [];
                moveTopInvoices(invoiceCtl.batchInvoices, invoicesToUpload, invoiceCtl.batchSize);
                upload(invoicesToUpload);
                invoiceCtl.batchesSent++;
            }
        };

        invoiceCtl.cancelUpload = function () {
            invoiceCtl.canceled = true;
        };

        var upload = function (invoicesToUpload) {

            if (invoiceCtl.canceled) {
                invoiceCtl.batchInvoices = [];
                return;
            }

            if (invoicesToUpload.length > 0) {
                $http.post("QuickBooks/UploadToQuickBooks", invoicesToUpload).
                    then(function (response) {

                        var savedInvoices = response.data.uploadedInvoices;

                        for (var i = 0; i < savedInvoices.length; i++) {
                            if (savedInvoices[i].OperationSucceeded) {
                                removeFromListByInvoiceNum(invoiceCtl.invoices, savedInvoices[i].Entity.InvoiveNum);
                                invoiceCtl.uploadedInvoices.push(savedInvoices[i].Entity);
                            }
                            else {
                                savedInvoices[i].Entity.ErrorMessage = savedInvoices[i].ErrorMessage;
                                invoiceCtl.errorInvoices.push(savedInvoices[i].Entity);
                            }
                        }

                        invoiceCtl.uploadedInvoicesCount += invoiceCtl.batchSize;
                        invoiceCtl.uploadPercentage = Math.round(invoiceCtl.uploadedInvoicesCount / invoiceCtl.invoicesToUploadCount * 100);

                        if (invoiceCtl.uploadedInvoicesCount >= invoiceCtl.invoicesToUploadCount) {
                            invoiceCtl.uploading = false;
                        }

                        if (!invoiceCtl.canceled) {
                            invoiceCtl.uploadInvoiceBatch();
                        }
                        else {
                            invoiceCtl.uploading = false;
                        }

                    },
                        function (err) {
                            alert(err);
                        });
            }
        };

        moveInvoices = function (fromList, toList, invoices) {
            for (var i = 0; i < invoices.length; i++) {
                toList.push(invoices[i]);
                removeFromList(fromList, invoices[i]);
            }
        };

        moveTopInvoices = function (fromList, toList, count) {
            var moveCount = Math.min(fromList.length, count);
            for (var i = moveCount - 1; i >= 0; i--) {
                toList.push(fromList[i]);
                removeFromList(fromList, fromList[i]);
            }
        };


        invoiceCtl.uploadInvoice = function (invoice) {
            var invoices = [];
            invoices.push(invoice);
            $http.post("QuickBooks/UploadToQuickBooks", invoices).
                then(function (response) {
                    invoiceCtl.uploadedInvoices.push(invoice);
                    removeFromList(invoiceCtl.invoices, invoice);
                },
                    function (err) {
                    });
        };

        invoiceCtl.selectedCount = function () {
            var selectedCount = 0;
            for (var i = 0; i < invoiceCtl.invoices.length; i++) {
                if (invoiceCtl.invoices[i].Selected) {
                    selectedCount++;
                }
            }
            return selectedCount;
        };

        invoiceCtl.getSelectedInvoices = function () {
            var selectedInvoices = [];
            for (var i = 0; i < invoiceCtl.invoices.length; i++) {
                if (invoiceCtl.invoices[i].Selected) {
                    selectedInvoices.push(invoiceCtl.invoices[i]);
                }
            }
            return selectedInvoices;
        };

        invoiceCtl.selectedTotal = function () {
            var total = 0;
            for (var i = 0; i < invoiceCtl.invoices.length; i++) {
                if (invoiceCtl.invoices[i].Selected) {
                    total += invoiceCtl.invoices[i].LineItemsTotalTaxIncluded;
                }
            }
            return total;
        };

        invoiceCtl.uploadedTotal = function () {
            var total = 0;
            for (var i = 0; i < invoiceCtl.uploadedInvoices.length; i++) {
                total += invoiceCtl.uploadedInvoices[i].LineItemsTotalTaxIncluded;
            }
            return total;
        };

        invoiceCtl.pendingTotal = function () {
            var total = 0;
            for (var i = 0; i < invoiceCtl.invoices.length; i++) {
                total += invoiceCtl.invoices[i].LineItemsTotalTaxIncluded;
            }
            return total;
        };

        invoiceCtl.uploadedCount = function () {
            return invoiceCtl.uploadedInvoices.length;
        };

        invoiceCtl.pendingCount = function () {
            return invoiceCtl.invoices.length;
        };

        invoiceCtl.errorCount = function () {
            return invoiceCtl.errorInvoices.length;
        };

        var removeFromList = function (list, invoice) {
            var index = list.indexOf(invoice);
            if (index > -1) {
                list.splice(index, 1);
            }
        };

        var removeFromListByInvoiceNum = function (list, invoiceNum) {
            for (var i = 0; i < list.length; i++) {
                if (list[i].InvoiveNum === invoiceNum) {
                    list.splice(i, 1);
                    break;
                }
            }
        };

        invoiceCtl.selectInvoices = function (count, select = true) {
            for (var i = 0; i < invoiceCtl.invoices.length; i++) {
                invoiceCtl.invoices[i].Selected = i < count || count === -1 ? select : false;
            }
        };

        invoiceCtl.search = function (row) {
            var lowerQuery = invoiceCtl.query.toLowerCase();
            return (row.InvoiveNum.toLowerCase().indexOf(lowerQuery || '') !== -1 ||
                    row.CustomerName.toLowerCase().indexOf(lowerQuery || '') !== -1);
        };

        invoiceCtl.getQuickBookStatus();
        invoiceCtl.loadInvoices();
    }]);
