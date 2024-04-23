var TableSort = {
    config: {
        queryParams: null,
        defaultQueryParams: {
            showInactive: $("#chk-show-inactive").is(":checked"),
            statusID: $("#lst-status-id").val(),
            contactTypeID: $("#data-contact-type-id").val()
        }
    },
    render: {
        table: function (tableID) {
            /*https://examples.bootstrap-table.com/*/
            $(tableID).bootstrapTable({
                //url:"CRM/Data/JSON/Vendors",
                pagination: true,
                search: true,
                rememberOrder: true,
                pageSize: 50,
                showButtonIcons: true,
                showButtonText: false,
                showSearchButton: true,
                showSearchClearButton: true,
                showRefresh: true,
                showToggle: true,
                showFullscreen: true,
                showPrint: true,
                showExport: true,
                queryParams: TableSort.config.queryParams || TableSort.config.defaultQueryParams
                /*
                queryParams: function (p) {
                    console.log(TableSort.config.queryParams || TableSort.config.defaultQueryParams);
                    return
                    //TableSort.config.queryParams ||
                    TableSort.config.defaultQueryParams
                        /*
                        {
                            showInactive: $("#chk-show-inactive").is(":checked"),
                            statusID: $("#lst-status-id").val(),
                            contactTypeID: $("#data-contact-type-id").val()
                        }
                        * /
                }
                */
            });
        }
    },
    format: {
        date: {
            lastActive: function (v, row) {
                return moment(v).format("MM/DD/YYYY ");
            }
        },
        currency: function (v, row) {
            return !v ? '-' : '$' + v.toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,');
        },
        CRM: {
            type: function (v, row) {
                if (v)
                    return v.replace("Owner - ", "");
                else return v;
            }
        }
    }
};
