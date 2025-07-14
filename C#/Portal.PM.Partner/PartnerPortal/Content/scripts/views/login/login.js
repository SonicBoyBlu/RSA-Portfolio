$(function () {
    Auth.init();
});

var Auth = {
    config: {
        url: "https://beta.acmehouseco.com",
        token: "AcmeAuthToken",
        tokens: {
            homeAway: Login.config.Escapia.Token
        }
    },
    init: function () {
        Auth.renderBackground();
        $(window).on("resize", function () { Auth.centerLogin(); });
        Auth.centerLogin();
        setTimeout(function () {
            Forms.init();
        }, 500);

        var un = localStorage.getItem("username");
        if (un) {
            $("[name='Username']").val(un);
            $("[name='Password']").focus();
        } else
            $("[name='Username']").focus();

        Auth.cookie.delete();
        localStorage.removeItem("g-auth");
        localStorage.removeItem("g-user");
        firebase.auth().signOut();
        //sessionStorage.removeItem("user");
        //sessionStorage.removeItem("_tokens");
    },
    cookie: {
        set: function (token) {
            var exp = new Date();
            exp.setDate(exp.getDate() + 7);
            var cookie = Auth.config.token + "=" + token + "; domain=.acmehouseco.com; expires=" + exp + "; path=/";
            document.cookie = cookie;

            if (window.location.href.indexOf("localhost") > -1) {
                var local = Auth.config.token + "=" + token + "; expires=" + exp + "; path=/";
                document.cookie = local;
            }
        },
        get: function () {
            var name = Auth.config.token + "=";
            var decodedCookie = decodeURIComponent(document.cookie);
            var ca = decodedCookie.split(';');
            for (var i = 0; i < ca.length; i++) {
                var c = ca[i];
                while (c.charAt(0) === ' ') {
                    c = c.substring(1);
                }
                if (c.indexOf(name) === 0) {
                    return c.substring(name.length, c.length);
                }
            }
            return "";
        },
        delete: function () {
            document.cookie = Auth.config.token + "=; expires=Thu, 01 Jan 1970 00:00:01 GMT;";
        }
    },
    centerLogin: function () {
        $("#view-sitelogin").stop().animate({ opacity: 1, top: ((($(window).height() / 2) - ($("#view-sitelogin").height() / 2)) - ($(window).height() * .1)) + "px" }, "slow").css({ "left": (($(window).width() / 2) - ($("#view-sitelogin").outerWidth() / 2)) + "px" })
    },
    login: function () {
        var btn = $("#btn-login"),
            msg = $("#login-message"),
            gauth = localStorage.getItem("g-auth"),
            guser = localStorage.getItem("g-user");
        $("body").addClass("loggin-in");

        if (!btn.hasClass("disabled")) {
            if (guser) {
                console.log("G-Suite Authenticated.");
                var g = JSON.parse(guser);
                $.ajax({
                    url: "/GoogleSignIn",
                    method: "post",
                    data: g,
                    success: function (d) {

                        if (!d.Status) {
                            msg.addClass("alert alert-danger").text(d.ErrorMessage);
                        }
                        else {
                            Auth.cookie.set(d.Data);
                            window.location = "/";
                        }
                    },
                    error: function (xhr) {

                        toastr.error(xhr.responseText);
                    },
                    beforeSend: function () {
                        msg.removeClass("alert alert-danger").text("");
                        $(".form-control").addClass("disabled");
                        btn.addClass("disabled").html("<i class='fa fa-spinner fa-pulse'></i>");
                    },
                    complete: function () {
                        $(".form-control").removeClass("disabled");
                        btn.removeClass("disabled").text("Login");
                    }
                });
            } else {

                $.ajax({
                    method: "post",
                    data: { Username: $("[name='Username']").val(), Password: $("[name='Password']").val() },
                    success: function (d) {
                        //debugger;

                        if (!d.Status) {
                            msg.addClass("alert alert-danger text-danger").html("<i class='fa fa-exclamation-triangle'></i> " + d.ErrorMessage);
                            $("body").removeClass("loggin-in");
                        }
                        else {
                            $("#login-form").html("<div class='text-center' style='padding-bottom:30px;'><i class='fa fa-5x fa-cog fa-spin'></i></div>");
                            //Auth.cookie.set(d.Data);

                            debugger;
                            sessionStorage.setItem("user", d.Data);
                            var u = JSON.parse(d.Data);
                            localStorage.setItem("username", u.Email);

                            //Auth.escapia.getUser(u);

                            if (u.UserType === 2) {
                                //fill in Escapia details
                                Auth.escapia.getUser(u);
                            } else {
                                window.location = "/";
                            }
                        }
                    },
                    error: function (xhr) {

                        var res = xhr.responseText,
                            msg = "Oops, a server error has occured.";
                        if (res.indexOf("Cannot open database") > -1)
                            msg += " Database is offline!"
                        toastr.error(msg);
                    },
                    beforeSend: function () {
                        //debugger;
                        msg.removeClass("alert alert-danger").text("");
                        $(".form-input").addClass("disabled");
                        btn.addClass("disabled").html("<i class='fa fa-spinner fa-pulse'></i>");
                    },
                    complete: function () {
                        $(".form-input").removeClass("disabled");
                        btn.removeClass("disabled").text("Login");
                    }
                });
            }
        }
    },
    sendpwrest: function () {
        var btn = $("#btn-pwrestsend");
        var msg = $("#login-message");
        var username = $("[name='Username']").val();

        $.ajax({
            type: "POST",
            url: "/SendResetPassword/",
            data: {
                username: username
            },
            beforeSend: function () {
                //debugger;
                msg.removeClass("alert alert-danger").text("");
                btn.prop('disabled', true);
                btn.addClass("disabled").html("<i class='fa fa-spinner fa-pulse'></i>");
            },
            success: function (result) {
                console.log(result)
                if (!result.success) {
                    msg.addClass("alert alert-danger text-danger").html("<i class='fa fa-exclamation-triangle'></i> " + result.message);
                }

                if (result.success) {
                    $("#pwrest-form").hide();
                    msg.addClass("alert alert-success text-center").html("Check your email for password reset instructions");
                }

            },
            complete: function () {
                btn.hide();
                $("[name='Username']").hide();
            },
            error: function (xhr, error) {}
        });
    },
    pwrest: function () {
        var btn = $("#btn-pwrest");
        var msg = $("#pwrest-message");
        var password1 = $("#password1").val();
        var password2 = $("#password2").val();
        var id = $("#id").val();

        $.ajax({
            type: "POST",
            url: "/ResetPassword/",
            data: {
                id: id,
                password1: password1,
                password2: password2
            },
             beforeSend: function () {
                 //debugger;
                 msg.removeClass("alert alert-danger").text("");
                 btn.prop('disabled', true);
                 btn.addClass("disabled").html("<i class='fa fa-spinner fa-pulse'></i>");
             },
            success: function (result) {
                console.log(result)
                if (!result.success){
                    msg.addClass("alert alert-danger text-danger").html("<i class='fa fa-exclamation-triangle'></i> " + result.message);
                }

                if (result.success) {
                    $("#pwrest-form").hide();
                    msg.addClass("alert alert-success text-center").html("Your password was updated!" + "<p>Please log in with your new password.</p>");
                }

            },
            complete: function () {
                btn.prop('disabled', false);
                btn.removeClass("disabled").text("Login");
            },
            error: function (xhr, error) {
            }
        });

    },
    escapia: {
        renewToken: function (u) {

            //RenewToken();
        },
        getUser: function (u) {
            u = GetEscapiaUser(u);
            debugger;
            window.location = "/";
        }
    },
    renderBackground: function () {
        var id = Math.floor(Math.random() * 10) + 1;
        $(".bg-image").addClass("login-" + id);
        $("body").addClass("active");
    }
};

$(document).on("click", "#btn-login", function () {
    Auth.login();
});

$(document).on("keyup", function (e) {
    if (e.keyCode === 13) Auth.login();
});

$(document).on("click", ".loggin-in *", function (e) {
    e.preventDefault();
    e.stopPropagation();
});

$(document).on("click", "#btn-pwrest", function (e) {
     e.preventDefault();
     e.stopPropagation();
     Auth.pwrest();
});

$(document).on("click", "#a-pwforget", function (e) {
    e.preventDefault();
    e.stopPropagation();
    //hide
    $("#div-password").hide();
    $("#div-pwrestsend").show();
    //show
});

$(document).on("click", "#btn-pwrestsend", function (e) {
    e.preventDefault();
    e.stopPropagation();
    Auth.sendpwrest();
});
