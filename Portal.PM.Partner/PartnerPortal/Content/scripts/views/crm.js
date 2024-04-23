var CRM = {
    AjaxHandler: null,
    config: {
        tabs: {}
    },
    init: function () {
        console.log("Init CRM: tab " + CRM.config.tabs.startup);

        // Render Mobile nav select wheel
        $("#tabs-contact-details a").each(function () {
            var i = $(this);
            $("#lst-tabs-contact-details").append($("<option />").val(i.attr("href")).text(i.text()));
        });

        // Open initial tab
        $("#nav-crm-main a[href='" + CRM.config.tabs.startup + "']").tab("show");
        //CRM.fetch.tab();
        this.render.lists.contactType.sub();

        this.render.formCommands();

    },
    lists: {
        contactSubTypes: {}
    },
    fetch: {
        tab: function (tab) {
            if (tab) {
                $("[data-toggle='tab'][href='#tab-" + tab + "']").tab("show");
                CRM.fetch.tab();
                return false;
            }

            var c = $("#crm-content-wrapper-main .tab-pane.active"),
                id = c.attr("id");

            tab = id.replace("tab-", "");
            sessionStorage.setItem("current-tab", tab);
            CRM.AjaxHandler =
                $.ajax({
                    url: "/CRM/Fetch/" + tab,
                    data: {

                    },
                    success: function (d) {
                        var title = "CRM: " + tab,
                            tableID = "#tbl-contacts-list";
                        document.title = title;
                        //$("#view-title").html(title);
                        c.html(d);
                    },
                    beforeSend: function () {
                        if (CRM.AjaxHandler !== null)
                            CRM.AjaxHandler.abort();
                        c.html("<div class='text-center'><i class='fa fa-cog fa-spin fa-5x'></i></div>");
                    }
                });
        },
        detail: function (id) {
            var mdl = $("#mdl-contact-detail");
            $.ajax({
                url: "/CRM/Fetch/Contact/" + id,
                cache: false,
                //url: "@Url.Action("FetchContact","CRM")",
                //data: { ContactID: id },
                success: function (d) {
                    $(".modal-body", mdl).html(d);
                    $("[readonly]", mdl).attr("title", "Click to edit");
                    //$("#contact-header").html($("#contact-modal-title").html());
                    //$("#contact-modal-title").remove();

                    var cType = $("#nav-crm-main .nav-link.active").data("contact-type-id"),
                        tab = $("#tabs-contact-details .nav-link[data-contact-type-id='" + cType +"']");
                    if (tab) $(tab).tab("show");
                    console.log("Show contact detail tab: " + cType);

                    //$("[data-toggle='tooltip']").tooltip({ placement:"top" });
                    Forms.init();
                    CRM.render.lists.contactType.sub();

                    // Add action row to each form region
                    CRM.render.formCommands();
                },
                beforeSend: function () {
                    //console.clear();
                    $("#mdl-footer-contact-detail-commands").addClass("d-none");
                    $("#contact-header").html();
                    mdl.modal({ show: true, keyboard: false });
                    $(".contact-name", mdl).html("Contact Detail");
                    $(".modal-body", mdl).html("<div class='text-center'><i class='fa fa-cog fa-spin fa-5x'></i></div>");
                },
                xhr: function () {
                    console.log("Reporting XHR progress");
                    var xhr = new window.XMLHttpRequest();

                    // Upload progress
                    xhr.upload.addEventListener("progress", function (evt) {
                        if (evt.lengthComputable) {
                            var percentComplete = evt.loaded / evt.total;
                            //Do something with upload progress
                            console.log(percentComplete);
                        } else console.error("Not Computable: upload");
                    }, false);

                    // Download progress
                    xhr.addEventListener("progress", function (evt) {
                        if (evt.lengthComputable) {
                            var percentComplete = evt.loaded / evt.total;
                            // Do something with download progress
                            console.log(percentComplete);
                        } else console.error("Not Computable: Progress");
                    }, false);

                    return xhr;
                }
                /*
                , xhr: function () {
                    console.log("Reporting XHR progress");
                    var xhr = $.ajaxSettings.xhr();
                    xhr.onprogress = function e() {
                        // For downloads
                        if (e.lengthComputable) {
                            console.log(e.loaded / e.total);
                        }
                    };
                    xhr.upload.onprogress = function (e) {
                        // For uploads
                        if (e.lengthComputable) {
                            console.log(e.loaded / e.total);
                        }
                    };
                    return xhr;
                }
                */
            });

        }
    },
    render: {
        formCommands: function () {
            $("form").each(function () {
                var i = $(this);
                // TODO: do not append to ID=0 rows (IE new templates)
                if ($("#command-row-save", i).length === 0) {
                    console.log("Adding Command Row");
                    i.append($("#command-row-save").html());
                }
            });
        },
        table: function (tableID) {
            TableSort.render.table(tableID);
        },
        toolbar: function () {
            var c = $(".fixed-table-toolbar .btn-group"),
                icons = $("[name='fullscreen'], .bs-print, .btn-export-vendors", c);
            if (!$("#navigation-main").is(":visible")) {
                icons.hide();
            } else {
                icons.show();
            }
        },
        lists: {
            contactType: {
                sub: function () {
                    //debugger;
                    var lst = $("select[name='ContactSubTypeID']"),
                        t = $("select[name='ContactTypeID']").val(),
                        s = [],
                        org = lst.data("org-value");
                    /*
                    console.log("Sub-Types:");
                    console.log(CRM.lists.contactSubTypes);
                    console.log("rendering contact sub-type list for: " + t);
                    */
                    //debugger;
                    lst.empty();
                    s = $.grep(CRM.lists.contactSubTypes, function (x) {
                        //console.log(x);
                        return x.parentTypeID == t;
                    });
                    //console.log("sub-types:");
                    //console.log(s);
                    $(s).each(function () {
                        var o = this;
                        lst.append($("<option />").text(o.label).val(o.subTypeID));
                    });
                    //debugger;
                    if (org)
                        $("[value='" + org + "']", lst).prop("selected", true).attr("selected","selected");
                }
            }
        }
    },
    toggle: {
        headerEditMode: function (show) {
            var c = $("form[id='contact-name']"),
                display = $(".header-display", c),
                edit = $(".header-edit", c);
            //debugger;
            if (show === true) {
                edit.removeClass("d-none");
                display.addClass("d-none");
            } else if (show === false) {
                edit.addClass("d-none");
                display.removeClass("d-none");
            } else {
                edit.toggleClass("d-none");
                display.toggleClass("d-none");
            }
        }
    },
    save: {
        detail: function (container) {
            var c = container,
                data = c.serializeObject(),
                type = c.data("form-type"),
                postData = { type: type, form: JSON.stringify(data) };
            console.log(postData);
            $.ajax({
                url: "/CRM/Save/Contact",
                method: "post",
                data: postData,
                success: function (d) {
                    //debugger;
                    if (d.IsSuccess) {
                        var r = d.Data;
                        toastr.success(d.Body, d.Title);
                        $(".row-action", c).addClass("d-none");
                        $(".form-input", c).each(function () {
                            var f = $(this);
                            f.attr("data-org-value", f.val());
                        });
                        $("a[href^='tel:'").attr("href", "tel:" + $("[type='tel']").val());
                        //debugger;
                        //$("form[data-form-type='" + type + "'] [name='PersonID']").val(r.PersonID);
                        if (r.PeopleID) $("[name='PeopleID']").val(r.PeopleID);
                        if (r.VendorID) $("[name='VendorID']").val(r.VendorID);
                        if (r.ExtendedLeadID) $("[name='ExtendedLeadID']").val(r.ExtendedLeadID);
                        if (r.ExtendedPotentialID) $("[name='ExtendedPotentialID']").val(r.ExtendedPotentialID);
                        if (r.PropertyID) $("[name='PropertyID']").val(r.PropertyID);
                        $(".bootstrap-table table:visible").bootstrapTable("refresh");
                    }
                    else
                        toastr.error(d.Message, d.Title);
                }
            });

        }
    }
};

$(document).on("click", ".modal", function (e) {
    var c = $(e.target).closest("form");
    //console.log(c.attr("class"));
    //console.log(c);
    if (c.attr("id") !== "contact-name")
        CRM.toggle.headerEditMode(false);
});

$(document).on("click", ".header-display", function () {
    CRM.toggle.headerEditMode(true);
    /*
    var i = $(this),
        p = i.closest("form"),
        edit = $(".header-edit", p);
    i.addClass("d-none");
    edit.removeClass("d-none");
    */
});

$(document).on("click", ".header-edit .btn-cancel", function () {
    debugger;
    CRM.toggle.headerEditMode(false);
    /*
    var i = $(this),
        p = i.closest("form"),
        display = $(".header-display", p),
        edit = $(".header-edit", p);
    edit.addClass("d-none");
    display.removeClass("d-none");
    */
});

$(document).on("click", ".btn-save", function () {
    var i = $(this),
        c = i.closest("form");
    CRM.save.detail(c);
});

$(document).on("click", ".row-action .btn-cancel", function () {
    var i = $(this),
        c = i.closest("form");
    $("[data-org-value]", c).each(function(){
        var f = $(this),
            o = f.data("org-value");
        f.val(o);
        f.trigger("change");
    });
    $(".row-action", c).addClass("d-none");
});

$("#mdl-crm-new-person").on("hide.bs.modal", function () {
    $(".btn-cancel", $("#mdl-crm-new-person")).trigger("click");
});


$(window).on("load resize", function () {
    CRM.render.toolbar();
});

$("#nav-crm-main", document).on("shown.bs.tab", function (e) {
    CRM.fetch.tab();
});

$(document).on("change", "#lst-tabs-contact-details", function () {
    var t = "#tabs-contact-details a[href='" + $(this).val() + "']";
    $(t).tab("show");
});

$(document).on("change", "[name='PhoneNumberTypeID']", function () {
    var i = $(this),
        v = i.val(),
        c = i.closest("form"),
        icn = $("a .fa", c);
    if (v === 2) icn.removeClass("fa-phone").addClass("fa-mobile-alt");
    else icn.removeClass("fa-mobile-alt").addClass("fa-phone");
});

$(document).on("change keyup", "[name='Number'],[name='Address']", function () {
    var i = $(this),
        v = i.val(),
        c = i.closest("form"),
        lnk = $("a.btn", c),
        href = lnk.attr("href");
    //console.log(lnk.attr("href"));
    if (href.indexOf("mailto:") > -1) lnk.attr("href", "mailto:" + v);
    else if (href.indexOf("tel:") > -1) lnk.attr("href", "tel:" + v);
});

$(document).on("click", "[data-property-id='0']", function (e) {
    $("#mdl-crm-new-property").modal("show");
});

$(document).on("click", "[data-contact-id]", function (e) {
    if (e.target.localName !== "a") {
        var i = $(this),
            id = parseInt(i.data("contact-id") || i.data("property-id"));
        //console.log("Loading contact detail ID:" + id);
        //console.log(this.caller);
        if (id > 0)
            CRM.fetch.detail(id);
        else {
            if (i.data("property-id"))
                $("#mdl-crm-new-property").modal("show");
            else
                $("#mdl-crm-new-person").modal("show");
        }
    }
});

$(document).on("click", "[data-action='new-item']", function () {
    var i = $(this),
        add = i.clone(),
        t = i.data("target"),
        tmp = $("[data-template='" + t + "']").html(),
        ul = i.closest("ul"),
        type = i.data("form-type"),
        f = $("form[data-form-type='" + type + "']"),
        //type = $("form[data-'" + i.closest("form").data("form-type") +"']");
        li = $("<li/>").append($("<form/>").attr("data-form-type", type).append(tmp));
    if ($(".form-input:not('.filled'):visible", f).length > 0) {
        //if ($("[name='ID'][value='0']:visible", f).length > 0) {
        //console.log("incomplete form");
        toastr.warning("Please finish your edits before creating new items.");
    } else {
        i.closest("li").remove();
    //debugger;
        if ($("[name='IsPrimary']:checked", ul).length === 0)
            $("[name='IsPrimary']", li).prop("checked", true);
        ul.append(li).append($("<li/>").append(add));
    }
});

$(document).on("click", "[data-action='remove-item']", function () {
    var i = $(this),
        tip = "Remove this ",
        c = i.closest("form"),
        t = c.data("form-type"),
        id = $("input[name='ID']", c).val();
    i.attr("data-toggle", "popover");
    switch (t) {
        case "contact-phone": tip += "phone number"; break;
        case "contact-email": tip += "email address"; break;
        default: "item"; break;
    }

    var cancel = $("<div/>").addClass("btn btn-link btn-sm").attr("data-toggle", "popover").attr("data-trigger", "click").append("Cancel"),
        confirm = $("<div/>").addClass("btn btn-outline-danger").attr("data-action", "remove-item-confirm").attr("data-id", id).attr("data-type", t).append("Yes"),
        content = $("<div/>").addClass("text-right").append(cancel).append(confirm);
    i.popover({
        html: true,
        trigger: "click",
        title: "<span class='text-danger'><i class='fa fa-trash-alt text-danger'></i> " + tip + "?</span>",
        content: content
    });
    i.popover("toggle");
});
$(document).on("click", "[data-action='remove-item-confirm']", function () {
    var i = $(this),
        id = i.data("id"),
        t = i.data("type"),
        c = i.closest(".popover"),
        title = $(".popover-header", c),
        body = $(".popover-body", c),
        li = $("form[data-form-type='" + t + "'] [value='" + id + "']").closest("li");
    title.html($("<span/>").addClass("text-danger").append(title.text().replace("Remove", "Removing").replace("?", "...")));
    body.html("<div class='text-center'><i class='fa fa-spin fa-cog'></i></div>");
    console.log("Remove phone: " + id);
    //debugger;
    if (id == 0) {
        c.popover("hide");
        li.fadeOut().remove();
    } else {
        $.ajax({
            url: "/CRM/Delete/" + t,
            data: {ID: id, type: t},
            success: function (d) {
                c.popover("hide");
                li.fadeOut().remove();
                toastr.success("Item Removed successfully.");
            }
        });
    }
});

$(document).on("change", ".cmd-filters [type='checkbox']:visible", function () {
    console.clear();
    var i = $(this),
        v = i.val(),
        c = i.closest("fieldset");
    if (v == 0) {
        $(".cmd-filters:visible [type='checkbox']:visible", c).prop("checked", i.is(":checked"));
    } else {
        $(".cmd-filters:visible [type='checkbox'][value='0']:visible", c).prop("checked", i.is(":checked"));
    }
    var chk = $(".cmd-filters:visible [type='checkbox']:checked"),
        ids = [],
        types = [],
        type = $("#nav-crm-main .nav-link.active").data("contact-type-id"),
        data = null;
    chk.each(function () {
        var _i = $(this),
            _t = _i.data("type"),
            _v = _i.val();
        console.log("type: " + _t + " val: " + _v);
        if (_t !== undefined) {
            ids.push({ type: _t, v: parseInt(_v) });
            if (types.indexOf(_t) < 0)
                types.push(_t);
        }
    });
    //console.info("types");
    //console.log(types);
    $(types).each(function () {
        if (!data) data = {};
        //console.log("this type: " + this);
        data[this] = [];
    });
    /*
    console.log("type: ContactSubTypeID");
    console.log(data.ContactSubTypeID);
    console.info("data empty");
    console.log(data);

    console.info("IDs");
    console.log(ids);
    */
    $(ids).each(function () {
        //console.log(this);
        //console.log($(this));
        data[this.type].push(this.v);
    });
    /*
    console.info("data filled");
    console.log(data);
    debugger;
    switch (type) {
        case "4": data = { ContactSubTypeID: ids, PropertyTypeID: ids }
        default: data = { ContactSubTypeID: ids }
    }
    */

    console.log("CRM Filter data");
    console.log(data === {});
    console.log(data);
    if (!data) data = { PersonID: 0 };
    $(tableName).bootstrapTable("filterBy", data);
});


$(document).on("click change", "#chart-pipeline-stage li>div, select[name='Stage']", function () {
    var i = $(this),
        v = parseInt(i.data("value") || i.val()),
        t = i.attr("title") || $("option:selected", i).text(),
        sel = $("select[name='Stage']"),
        chrt = $("#chart-pipeline-stage");
    if (i.not("select")) {
        console.log("chart changed to: " + v);
        $("option", sel).removeAttr("selected");
        $("option[value='" + v + "']", sel).prop("selected", true);
    } 
    $("li>div", chrt).removeClass("active current");
    $("li>div[data-value='" + v + "']", chrt).addClass("current");
    $("li>div", chrt).each(function () {
        var x = $(this),
            n = parseInt(x.data("value"));
        if (n <= v)
            x.addClass("active");
    });
    $(".lable-pipeline-status").text(t);
    //$(".btn-save", i.closest("form")).trigger("click");
    CRM.save.detail(i.closest("form"));
});

$(function () {
    CRM.init();
});