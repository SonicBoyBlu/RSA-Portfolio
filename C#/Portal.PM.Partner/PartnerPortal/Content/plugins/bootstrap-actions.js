// popovers
$(document).on("show.bs.popover", function () {
    $(".popover").popover("hide");
});
$(document).on("click", "*[data-toggle='popover']", function (e) {
    $("*[data-toggle='popover']").not(this).popover('hide');
});

// modals
$(document).on("shown.bs.modal", function () {
    console.log("Init modal");
    Utilities.Modals.init();
});
