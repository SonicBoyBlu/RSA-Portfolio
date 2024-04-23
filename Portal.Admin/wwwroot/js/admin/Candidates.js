var Candidates = {
    init: function () {
        if(!Candidates.config.modals.detail)
            Candidates.config.modals.detail = new bootstrap.Modal(Candidates.config.modals.id);
    },
    config: {
        modals: {
            id: "#mdl-candidate-detail",
            detail: null
        },
        url: "/Candidates/Details/"
    },
    fetch: {
        detail: function (userid, view) {
            //debugger;
            view = (view || "Account");
            console.log("Userid: " + userid + " - view: " + view);

            Candidates.config.modals.detail.show();
            var mdl = {
                    body: $("#candidate-detail-body"),
                    title: $("#candidate-detail-header")
                };
            // Title
            $.ajax({
                url: Candidates.config.url + "Header",
                //method: "post",
                data: { UserID: userid },
                success: function (d) {
                    mdl.title.html(d);
                    $(Candidates.config.modals.id).attr("data-ref-id", userid);
                    //$("#mdl-candidate-detail").attr("data-ref-id", userid);
                    //Candidates.config.modals.detail.attr("data-user-ref-id", userid);
                },
                beforeSend: function () {
                    //Candidates.config.modals.detail.removeAttr("data-user-ref-id");
                    mdl.title.html("Loading...");
                },
                complete: function () {

                }
            });
            Candidates.fetch.tab(userid, view);
        },
        tab: function (userid, view) {
            $(".modal .nav-item a").removeClass("active");
            $(".modal .nav-item").removeClass("active");

            $(".modal .nav-item a[href='" + view + "']").addClass("active");

            //debugger;
            var body = $("#candidate-detail-body", document);

            view = (view || "Account");

            // Tab/Body
            $.ajax({
                url: Candidates.config.url + view,//, .replace("#", ""),
                //method: "post",
                data: { UserID: userid },
                success: function (d) {
                    body.html(d);
                },
                beforeSend: function () {
                    body.html("Loading...");
                },
                complete: function () {

                }
            });
        }
    }
};

$(function () {
    Candidates.init();
})

$(document).on("click", "[data-candidate-user-id]", function () {
    var i = $(this),
        id = i.data("candidate-user-id"),
        view = i.data("view-type") || "Account";
    //debugger;
    Candidates.fetch.detail(id, view);
});

//*
$(document).on("click", ".modal .nav-tabs .nav-link", function (e) {
    //e.preventDefault();
    //$(".modal .nav-item a").removeClass("active");
    //$(".modal .nav-item").removeClass("active");
    var i = $(this),
        //id = 127697, // i.data("candidate-user-id"),
        id = $(Candidates.config.modals.id, document).attr("data-ref-id"),
        view = i.attr("href").replace("#", "");
    //i.addClass("active");
    //i.closest(".nav-item").addClass("active");
    console.log("View: " + view + " ID:" + id);
    //debugger;
    Candidates.fetch.tab(id, view);
});
//*/

//Candidates.fetch.detail(127976, "test");