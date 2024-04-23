app.controller('WorkOrderController', ['$scope', '$http', '$uibModal', function ($scope, $http, $uibModal) {

    var ctl = this;

    //Filters
    var showWorkOrdersWithZeroBalance = false;
    ctl.unitFilter = '';
    ctl.typeFilter = 'Property Care';
    ctl.balanceFilter = 'With Balance';

    //Defaults
    ctl.selectedTab = 'Owner';
    ctl.workOrders = [];
    ctl.loading = false;

    //Tabs
    var owner = 0;
    var comp = 1;
    var other = 2;
    var ignored = 3;
    var locked = 4;

    ctl.tabs = [
        { id: 'Owner', title: 'Owner', unFilteredInvoices: [], invoices: [], color: 'success', icon: 'fa fa-money-check-alt' },
        { id: 'Comp', title: 'Internal / No Charge', unFilteredInvoices: [], invoices: [], color: 'info', icon: 'fa fa-house-damage'},
        { id: 'Other', title: 'Other', unFilteredInvoices: [], invoices: [], color: 'warning', icon: 'fa fa-question' },
        { id: 'Ignored', title: 'Ignored', unFilteredInvoices: [], invoices: [], color: 'danger', icon: 'fa fa-eye-slash' },
        { id: 'Locked', title: 'Locked for QB', invoices: [], color: 'secondary', icon: 'fa fa-lock' }
    ];

    ctl.billTo = [
        { name: 'Owner', tab: owner },
        { name: 'No Charge/Internal', tab: comp },
        { name: 'Damage', tab: other },
        { name: 'Guest', tab: other },
        { name: 'Uncategorized', tab: other }
    ];

    ctl.editInBreezeway = function (invoice) {
        var modalInstance = $uibModal.open({
            templateUrl: "/WorkOrders/EditInBreezeway",
            controller: "EditInBreezewayController",
            size: '',
            resolve: {
                invoice: function () {
                    return invoice;
                }
            }
        });

        modalInstance.result.then(function (invoice) {
            deleteInvoice(invoice);
        });
    };

    ctl.testToast = function () {
        toastr.success('Test', 'Success');
    };

    ctl.date = {
        //startDate: moment('2019/10/1'),
        startDate: dateOldest || moment().startOf('month'),
        //startDate: moment().startOf('month'),
        endDate: moment().endOf('month')
    };

    ctl.loadWorkOrders = function () {
        setDefaultSummaryValues();
        clearTabs();
        ctl.loading = true;
        $http.get("/WorkOrders/GetWorkOrders?startDate=" + ctl.date.startDate.toJSON() + "&endDate=" + ctl.date.endDate.toJSON()).
            then(function (response) {
                ctl.tabs[0].unFilteredInvoices = response.data.owners;
                ctl.tabs[1].unFilteredInvoices = response.data.comp;
                ctl.tabs[2].unFilteredInvoices = response.data.other;
                ctl.tabs[3].unFilteredInvoices = response.data.ignored;
                ctl.tabs[4].unFilteredInvoices = response.data.locked;
                ctl.filterWorkOrders();
                setSummaryValues();                
                ctl.loading = false;
            },
                function (err) {
                    ctl.loading = false;
                });
    };

    ctl.updateBillTo = function (invoice) {

        $http.get("/WorkOrders/UpdateBillTo?ID=" + invoice.InvoiveNum + "&Type=" + invoice.BillTo).
            then(function (response) {
                moveOnBillToChanged(invoice);
            },
                function (err) {
                    console.log(err);
                });
    };

    ctl.toggleInvoiceLock = function (invoice) {
        $http.get("/WorkOrders/UpdateLock?ID=" + invoice.InvoiveNum + "&IsLocked=" + invoice.IsLocked).
            then(function (response) {
                invoice.IsLocked = !invoice.IsLocked;
                if (invoice.IsLocked) {
                    moveInvoiceToTab(ctl.tabs[locked], invoice);
                }
                else {
                    moveInvoiceToTab(ctl.tabs[owner], invoice);
                }
                setSummaryValues();
            },
            function (err) {
                console.log(err);
            });
    };

    ctl.toggleInvoiceIgnore = function (invoice) {
        $http.get("/WorkOrders/UpdateViewIgnore?ID=" + invoice.InvoiveNum + "&IsShown=" + invoice.IsIgnore).
            then(function (response) {
                invoice.IsIgnore = !invoice.IsIgnore;
                if (invoice.IsIgnore) {
                    moveInvoiceToTab(ctl.tabs[ignored], invoice);
                    toastr.success("Work Order #" + invoice.InvoiveNum + " Ignored");
                }
                else {
                    moveOnBillToChanged(invoice);
                    toastr.success("Work Order #" + invoice.InvoiveNum + " Restored");
                }

                setSummaryValues();
            },
                function (err) {                    
                    toastr.error("Work Order " + invoice.InvoiveNum + " request failed");
                });
    };

    ctl.syncCustomers = function () {
        toastr.warning("Running Customer Sync, please wait");
        $http.get("/WorkOrders/SyncCustomers").
            then(function (response) {
                toastr.success("Customer Sync Completed!");
            },
                function (err) {
                    toastr.error("Customer Sync Failed!");
                    console.log(err);
                });
    };

    deleteInvoice = function (invoice) {
        $http.get("/WorkOrders/DeleteRecord?ID=" + invoice.InvoiveNum).
            then(function (response) {
                removeInvoiceFromTabs(invoice);
                setSummaryValues();
                toastr.success("Work Order " + invoice.InvoiveNum + " Deleted");
                window.open("https://app.breezeway.io/task/" + invoice.InvoiveNum, "Breezeway");
            },
                function (err) {
                    console.log(err);
                });
    };

    //Move the invoice to the tab based on billTo
    moveOnBillToChanged = function (invoice) {

        var msg = "Update Work Order #" + invoice.InvoiveNum;
        var title = "Bill to: " + invoice.BillTo;

        if (invoice.BillTo === 'Owner') {
            moveInvoiceToTab(ctl.tabs[owner], invoice);
            toastr.success(title, msg);
        }
        else if (invoice.BillTo === 'No Charge/Internal') {
            moveInvoiceToTab(ctl.tabs[comp], invoice);
            toastr.info(title, msg);
        }
        else {
            moveInvoiceToTab(ctl.tabs[other], invoice);
            toastr.warning(title, msg);
        }

        setSummaryValues();
    };

    moveInvoiceToTab = function (tab, invoice) {
        if (!listContainsInvoice(tab.invoices, invoice)) {
            tab.invoices.push(invoice);
            tab.unFilteredInvoices.push(invoice);

            for (var i = 0; i < ctl.tabs.length; i++) {
                if (ctl.tabs[i].id !== tab.id) {
                    if (removeInvoiceFromList(ctl.tabs[i].invoices, invoice)) {
                        removeInvoiceFromList(ctl.tabs[i].unFilteredInvoices, invoice);
                        break;
                    }
                }
            }
        }
    };

    removeInvoiceFromTabs = function (invoice) {
        for (var i = 0; i < ctl.tabs.length; i++) {
            if (removeInvoiceFromList(ctl.tabs[i].invoices, invoice)) {
                removeInvoiceFromList(ctl.tabs[i].unFilteredInvoices, invoice);
                break;
            }
        }
    };

    var removeInvoiceFromList = function (list, invoice) {
        for (var i = 0; i < list.length; i++) {
            if (list[i].InvoiveNum === invoice.InvoiveNum) {
                list.splice(i, 1);
                return true;
            }
        }
        return false;
    };


    listContainsInvoice = function (invoices, invoice) {
        for (var i = 0; i < invoices.length; i++) {
            if (invoices[i] === invoice.InvoiveNum)
                return true;
        }
        return false;
    };

    ctl.dateRangePickerOptions = {
        locale: {
            format: 'MM/DD/YYYY'
        },
        eventHandlers: {
            'apply.daterangepicker': function (event, picker) { ctl.loadInvoices(); }
        }
    };

    ctl.filterWorkOrders = function () {
        for (var i = 0; i < ctl.tabs.length; i++) {
            filterWorkOrderTabItems(ctl.tabs[i]);
        }
    };

    filterWorkOrderTabItems = function (tab) {

        var lowerCaseUnitFilter = ctl.unitFilter.toLowerCase();
        tab.invoices = [];

        for (var i = 0; i < tab.unFilteredInvoices.length; i++) {

            if (meetsBalanceFilter(tab.unFilteredInvoices[i]) && meetsUnitFilter(tab.unFilteredInvoices[i], lowerCaseUnitFilter)) {
                tab.invoices.push(tab.unFilteredInvoices[i]);
            }
            tab.unFilteredInvoices[i].editable = true;
            tab.unFilteredInvoices[i].Expanded = true;
        }
    };

    meetsBalanceFilter = function (item) {
        if (ctl.balanceFilter === 'All') {
            return true;
        }
        else if (ctl.balanceFilter === 'With Balance') {
            return item.TotalAmount > 0.0;
        }
        else
            return item.TotalAmount === 0.0;
    };


    meetsUnitFilter = function (item, unitFilter) {
        if (!unitFilter || unitFilter === '')
            return true;

        if (item.InvoiveNum.toLowerCase().includes(unitFilter) ||
            item.CustomerName.toLowerCase().includes(unitFilter)) {
            return true;
        }

        return false;
    };

    ctl.workOrdersSum = function (workOrders) {
        var total = 0.0;
        if (workOrders) {
            for (var i = 0; i < workOrders.length; i++) {
                total += workOrders[i].TotalAmount;
            }
        }
        return total;
    };

    ctl.dateRangePickerOptions = {
        locale: {
            format: 'MM/DD/YYYY'
        },
        eventHandlers: {
            'apply.daterangepicker': function (event, picker) { ctl.loadWorkOrders(); }
        }
    };

    setSummaryValues = function () {
        //Bilable
        ctl.ownerTotalCount = ctl.tabs[owner].unFilteredInvoices.length;
        ctl.ownerTotalBillableCount = ctl.tabs[owner].invoices.length;
        ctl.ownerTotalAmount = ctl.workOrdersSum(ctl.tabs[owner].unFilteredInvoices);

        //Comp
        ctl.compTotalCount = ctl.tabs[comp].unFilteredInvoices.length;
        ctl.compTotalBillableCount = ctl.tabs[comp].invoices.length;
        ctl.compTotalAmount = ctl.workOrdersSum(ctl.tabs[comp].unFilteredInvoices);

        //Other
        ctl.otherTotalCount = ctl.tabs[other].unFilteredInvoices.length;
        ctl.otherTotalBillableCount = ctl.tabs[other].invoices.length;
        ctl.otherTotalAmount = ctl.workOrdersSum(ctl.tabs[other].unFilteredInvoices);

        //Locked
        ctl.lockedTotalCount = ctl.tabs[locked].unFilteredInvoices.length;
        ctl.lockedTotalBillableCount = ctl.tabs[locked].invoices.length;
        ctl.lockedTotalAmount = ctl.workOrdersSum(ctl.tabs[locked].unFilteredInvoices);

        ctl.lockedPercent = ctl.lockedTotalCount / (ctl.lockedTotalCount + ctl.ownerTotalCount) * 100;
    };

    clearTabs = function (invoice) {
        for (var i = 0; i < ctl.tabs.length; i++) {
            ctl.tabs[i].invoices = [];
            ctl.tabs[i].unFilteredInvoices = [];
        }
    };

    setDefaultSummaryValues = function () {
        ctl.ownerTotalCount = 0;
        ctl.ownerTotalBillableCount = 0;
        ctl.ownerTotalAmount = 0;

        ctl.compTotalCount = 0;
        ctl.compTotalBillableCount = 0;
        ctl.compTotalAmount = 0;

        ctl.otherTotalCount = 0;
        ctl.otherTotalBillableCount = 0;
        ctl.otherTotalAmount = 0;

        ctl.lockedTotalCount = 0;
        ctl.lockedTotalBillableCount = 0;
        ctl.lockedTotalAmount = 0;

        ctl.lockedPercent = 0.00;
    };

    init = function () {
        ctl.loadWorkOrders();
    };

    init();

    angularInvoiceRefresh = ctl.loadWorkOrders;

    

}]);


