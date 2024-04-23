// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

window.transitionToPage = function (href) {
    document.querySelector('body').style.opacity = 0
    setTimeout(function () {
        alert("href: " + href);
        window.location.href = href
    }, 500)
}

document.addEventListener('DOMContentLoaded', function (event) {
    document.querySelector('body').style.opacity = 1
    //var jobid = sessionStorage.getItem("jobIdLast");
    //debugger;
});

$(document).on("click", "#tbl-job-list [id]", function () {
    var id = $(this).attr("id");
    //sessionStorage.setItem("jobIdLast", id);
    //debugger;
    /*
     $("#tbl-job-list [id]").removeClass("active");
     $(this).addClass("active");
     */
});

$(function () {
    //alert(isEmbed ? "internal" : "External");
    var native = window.self == window.top;
    //alert("Embedded: " + (native ? "Internal" : "External"));
    if (!native) {
        $("header").css({ "display": "none" });
    }

    $("#tbl-job-list [id='" + window.location.hash.substr(1) + "']").addClass("active");
});