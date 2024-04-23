var Forms = {
    init: function () {
        console.log("Init: Forms");
        $(".form-control, .form-input").each(function () {
            var i = $(this);
            if(i.hasClass("form-control"))
                i.removeClass("form-control").addClass("form-input").wrap($("<div/>").addClass("form-group"));
            //if (i.attr("name") === "pscitysummaryidnumber") debugger;
            if ((i.val() || "").trim() !== "") {
                //console.log(i.attr("name") + ": " + (i.val().trim() !== ""));
                i.addClass("filled").closest(".form-group").addClass("focused");
            } else {
                i.removeClass("filled");
            }
        });

        $(".form-group").each(function () {
            var i = $(this),
                f = $(".form-input", i),
                l = f.attr("placeholder") || f.data("placeholder"),
                lbl = $("<lable/>").addClass("form-label").text(l);
            if($(".form-label", i).length === 0)
                i.prepend(lbl);
        });
        $(".form-input, [type='checkbox'], [type='radio']").each(function () {
            var i = $(this),
                p = i.attr("placeholder") || i.data("placeholder"),
                v = i.val(),
                t = i.attr("type");
            if ("checkbox|radio".indexOf(t) > -1)
                i.attr("data-org-value", v === "on" || v == true ? true : false);//.tooltip({ title: t, trigger:"click" });
            else
                i.attr("data-org-value", v);

        });

        $("label [type='checkbox'], label [type='radio']").each(function () {
            var i = $(this),
                label = i.closest("label");
            if ($("span", label).length === 0) {
                i.after($("<span />"));
            }
        });

        $("select, [date-range-picker], [date-picker], [type='date'], [type='search']").addClass("charmed");
        /*
        $("[date-picker]").each(function () {
            var i = $(this),
                v = i.val();
            console.log(i.attr("name") + ": " + v);
            if (v) {
                i.val(moment(new Date(v)).format("mm/dd/yyyy"));
                console.log("FOUND " + i.attr("name") + ": " + i.val());
            }
        });
        */
        var DefaultCallbacks = {
            daterangepicker: function (picker) {
                console.info("Start: " + picker.startDate + " End: " + picker.endDate);
            }
        };
        $("[date-picker]").daterangepicker({
            //startDate: function () { return moment().format("MM/DD/YYYY", $(this).val()); },
            //startDate: $(this).val() || "",
            singleDatePicker: true,
            autoUpdateInput: false,
            //autoApply: true,
            locale: {
                format: "MM/DD/YYYY"
            }
        }, function (start, end, label) {
        });
        $("[date-picker]").on("apply.daterangepicker", function (e, picker) {
            //debugger;
            $(this).val(picker.startDate.format("MM/DD/YYYY")).trigger("change");
            DefaultCallbacks.daterangepicker(picker);

        });

        /*
        $("[type='checkbox'], [type='radio']").each(function () {
            var i = $(this),
                id = i.attr("name"),
                text = i.data("label") || i.closest("label").text().trim();
            //if (!i.closest("label").hasClass("form-input-checkbox")) {
            console.log("Label for: " + id + " #:" + $("label[for='" + id + "']").length);
            if ($("label[for='" + id + "']").length === 0) {
                //debugger;
                i.attr("id", id);
                console.log("check:" + id + " text: '" + text + "'");
                var
                    label = $("<label/>", { "for": id, "class": "form-input-checkbox" }),
                    span = $("<span/>"),
                    name = $("<ins/>)").append(text),
                    wrap = label.append(span).append(name);
                //debugger;
                i.closest("div").empty().append(i).append(wrap);
            }
        });
        */

    }
};

Forms.init();

$(document).ajaxComplete(function () { Forms.init(); });
$(document).on("load ready", function () { Forms.init(); });

$(document).on("focus change click keyup", ".form-input, .form-label", function() {
    var grp = $(this).closest('.form-group'),
        i = $(".form-input:first", grp),
        v = i.val().trim(),
        t = i.attr("type");
    grp.addClass('focused');
    if (v === "")
        i.removeClass('filled');
    else i.addClass("filled");
    if (!i.is(":focus")) {
        console.log("Input not in focus");
        i.focus();
    }
    if (t === "number") {
        var min = i.attr("min") || 0;
        if (v === "") i.val(min);
    }
    //return false;
});
$(document).on("focus", "input.form-input", function () {
    $(this).select();
});

$(document).on("blur", ".form-input",  function () {
    var i = $(this),
        inputValue = i.val().trim();
    if (inputValue === "") {
        i.removeClass('filled').closest('.form-group').removeClass('focused');
    } else {
        i.addClass('filled');
    }
});

$(document).on("click change", ".toggle", function(){
    Utilities.sound.play.toggle();
    var i = $(this),
        isOn = i.hasClass("fa-toggle-on"),
        t = i.data("toggle"),
        c = i.data("target"),
        chk = $("[name='" + i.data("check") + "']");
    i.toggleClass("fa-toggle-on fa-toggle-off text-default text-primary");
    chk.prop("checked", isOn);
    console.log("toggle: " + chk.val());
    if (t) {
        $(c).toggleClass("d-none");
    }
});
$(document).on("click", "[data-switch]", function () {
    var i = $(this),
        on = i.data("switch");
    if (on === true) {
        i.removeClass("fa-toggle-on").addClass("fa-toggle-off").data("switch", false);
    } else {
        i.removeClass("fa-toggle-off").addClass("fa-toggle-on").data("switch", true);
    }
    new Audio("/Content/sounds/button-46-toggle.mp3").play();
});

$(document).on("click", "input[type='checkbox'], [type='radio']", function () {
    new Audio("/Content/sounds/button-46-toggle.mp3").play();
});

$(document).on("keyup", ".form-input, .form-control", function (e) {
    var i = $(this),
        v = i.val(),
        o = i.data("org-value"),
        c = i.closest("form"),
        cmd = $(".row-action", c),
        isChange = false,
        key = e.keyCode || e.which,
        keys = [13, 27];

    console.log("You keyed: " + key);
    //debugger;
    
    if (keys.includes(key)) {
        console.log("Listener key triggered");
        e.preventDefault();
        e.stopPropagation();
    }

    if (key === 13) {
        //$(".btn-save:visible", c).click();
    } else if (key === 27) {
        //$(".btn-cancel:visible", c).click();
    }
});

$(document).on("keyup change", ".form-input, .form-control", function (e) {
    var i = $(this),
        v = i.val(),
        o = i.data("org-value"),
        c = i.closest("form"),
        cmd = $(".row-action", c),
        isChange = false;
    if (v !== o) {
        i.attr("data-is-update", true);
        isChange = true;
    } else {
        i.removeAttr("data-is-update");
        isChange = $("[data-is-update]", c).length > 0;
    }
    if (isChange) cmd.removeClass("d-none");
    else cmd.addClass("d-none");
});
$(document).on("change", "[type='checkbox'], [type='radio']", function (e) {
    var i = $(this),
        c = i.closest("form"),
        cmd = $(".row-action", c);

    i.attr("data-is-update", true);
    isChange = true;
    cmd.removeClass("d-none");
});