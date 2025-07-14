$(document).on("click", "[data-user-id]", function () {
    ResetUserEdit();
    Users.fetch.user($(this).data("user-id"));
});


$(document).on("click", ".btn-employee-edit", function (e) {
    e.preventDefault();
    $("#mdl-employee-edit").modal('toggle');
    Users.fetch.userUpdate();
});

$(document).on("click", ".user-add", function () {
    $("#UserTypeID option:eq(0)").prop("selected", true);
    Users.fetch.clearUserForm();
    Users.fetch.userBambooList();
    $("#mdl-employee-edit").modal("show");
});


$(document).on("click", "#form-secondary-show", function (e) {
    e.preventDefault();
    Users.fetch.clearUserForm();
    $(".form-secondary").show();
});



$(document).on("change", "#UserTypeID", function () {
    Users.fetch.clearUserForm();
    $("#BambooList option:eq(0)").prop("selected", true);
    var usertypeid = parseInt(this.value);
    if (usertypeid === 1) {
        $(".form-bamboo-list").show();
    }
    if (usertypeid > 1) {
        // here
    }
});

$(document).on("change", "#BambooList", function () {
    Users.fetch.clearUserForm();
    $(".form-bamboo-list").show();
    var userid = parseInt(this.value);
    if (isNaN(userid))
        return;
    Users.fetch.userBamboo(userid);
    $(".form-secondary").show();
});


