var System = {
    config: {
        name:"career-fair"
    },
    event: {
        
    },
    init: function (fetchForm) {
        //$("[type='tel']").mask("(999)-999-9999");
        //debugger;
        var ls = window.localStorage.getItem(System.config.name);
        System.event = ls ? JSON.parse(ls) : null;
        if (System.event) {
            var e = System.event;
            //System.config.id = id;
            //console.log("location: " + window.location.pathname);
            //*
            if (window.location.pathname.toLocaleLowerCase() != "/signup") {
                System.fetch.form();
            }
            //debugger;
            $("[type='email']").inputmask({ alias: "email" });

            $(".form-control, .form-select").each(function () {
                //debugger;
                var
                    i = $(this),
                    n = i.prop("name"),
                    c = i.closest("div"),
                    p = i.prop("placeholder") || i.data("placeholder");
                c.addClass("form-floating").append($("<label/>").attr("for", n).text(p));
            });


            $("[name='CpUserID']").val(e.CpUserID);
            $("#cp-name").text($("[name='CpUserID'] option:selected").text());
            //*/
            $("#event-name").text(e.name);
            $("#event-id, [name='EventID']").val(e.eventID);
            $("[name='SignupSheetID']").val(e.sheetID);
            //$("[type='tel']").mask("(999)-999-9999");
        }
        else {
            //debugger;
            if (window.location.pathname.toLocaleLowerCase() != "/") {
                System.form.reset();
                window.location = "/";
            }
        }
        $(".form-control:first").focus();
    },
    fetch: {
        form: function () {
            if (System.form.validate()) {
                //alert("Fetching event: " + localStorage.getItem(System.config.name));
                window.location = "/Signup";
            }
            else {
                alert("Error");
            }
        }
    },
    save: {
        candidate: function () {

        }
    },
    form: {
        reset: function () {
            localStorage.removeItem("form-intake");
            $("input textarea select").val("");
        },
        validate: function () {
            var
                form = $("form"),
                data = form.serializeObject();
            localStorage.setItem("form-intake", JSON.stringify(data));

            $(".error").removeClass("error");
            form.validate({
                rules: {
                    field: {
                        required: true
                    }
                },
                highlight: function (item) {
                    let
                        i = $(item),
                        c = i.closest("div");
                    //w.addClass("error");
                    debugger;
                    if (i.hasClass("valid")) {
                        c.removeClass("error");
                        //$(".error", c).remove();
                    } else {
                        c.addClass("error");
                    }
                }
            });
            $.validator.messages.required = '*';
            return true;
        }
    }
};

/*
$(document).on("keyup change", "input", function (e) {
    var r = $("[required]").val();
    //debugger;
});
*/

/*
$(document).on("click", "[type='submit']", function () {
    var
        i = $(this),
        f = $("form", i)
});
*/

$(document).on("keyup change", "[name='MajorName']", function (e) {
    let
        i = $(this),
        v = i.data("value"),
        t = i.text();
    console.log(t + " : " + v);
    $("[name='MajorID']").val(v);
});

$(document).on("keyup change", "form", function (e) {
    var form = $("form").serializeObject();
    //debugger;
    localStorage.setItem("form-intake", JSON.stringify(form));
});
$(document).on("keyup change blur", "[required]", function (e) {
    var i = $(this);
    if (i.val() == "") {
        i.closest("div").addClass("error");
    } else {
        i.closest("div").removeClass("error");
    }
});

$(document).on("change", "[name='CpUserID']", function () {
    var
        i = $(this),
        o = $("option:selected", i),
        ls = window.localStorage.getItem(System.config.name);
    if (ls) {
        //debugger;
        ls = JSON.parse(ls);
        ls.CpUserID = i.val();
        window.localStorage.setItem(System.config.name, JSON.stringify(ls));
    }
    $("#cp-name").text(o.text());
});

$(document).on("click", "[type='submit']", function (e) {
    e.preventDefault();
    e.stopPropagation();
    console.info("Submit form validation.");
    //debugger;
    var
        i = $(this),
        form = i.closest("form"),
        data = form.serializeObject();
    localStorage.setItem("form-intake", JSON.stringify(data));
    System.form.validate();
    if (!form.valid()) {
        alert("Form NOT valid");
    } else {
        $(".carousel").carousel("next");
        setTimeout(function () {
            //debugger;
            $("#btn-signup-admin-review").animate({ opacity: "100%" });
            $(".area-admin").removeClass("d-none");
            $(".area-candidate").addClass("d-none");

        }, 3000);
    }
});

$(document).on("click", "#btn-signup-submit", function (e) {
    //debugger;
});

$(document).on("click", "#btn-signup-admin-review", function () {
    //debugger;
    $(".carousel").carousel("prev");
});

$(document).on("click", "#btn-signup-admin-submit", function () {
    $(".area-admin").addClass("d-none");
    $(".area-candidate").removeClass("d-none");
    // AJAX save form to DB && Clear localStorage
    var form = $("form").serializeObject();
    $.ajax({
        //url: "/signup",
        method: "post",
        data: form,
        cache: false,
        headers:
        {
            "RequestVerificationToken": $('input:hidden[name="__RequestVerificationToken"]').val()
        },
        beforeSend: function () {
            //alert("Saving Signup Sheet");
            console.warn("Saving Signup Sheet");
            console.log(form);
        },
        success: function (d) {
            //debugger;
            //localStorage.removeItem("form-intake");
            System.form.reset();
        }
    });
});

$(function () {
    console.info("Rendering input masks....");
    //$("input", document).unmask();
    // American Phone format
    $("[type='tel']", document).inputmask("(999) 999-9999");
    
    $(document).on("focus click", "input", function () {
        //debugger;
        $(this).select();
    });

    //*
    //email mask
    $("[type='email']", document).inputmask({

        mask: "*{1,20}[.*{1,20}][.*{1,20}][.*{1,20}]@*{1,20}[.*{2,6}][.*{1,2}]",
        greedy: false,
        onBeforePaste: function (pastedValue, opts) {
            pastedValue = pastedValue.toLowerCase();
            return pastedValue.replace("mailto:", "");
        },
        definitions: {
            '*': {
                validator: "[0-9A-Za-z!#$%&'*+/=?^_`{|}~\-]",
                casing: "lower"
            }
        }
    });
    //*/
});