var iFrames = {
    init: function () {
        var s =$(".app-content-wrapper > .container").eq(0),
            w = s.innerWidth() - 20,
            h = $(".app-sidebar").eq(0).height();
        $("#iframe-report").width(w).height(h);
        console.log("rendering iFrame report");
    }
};
$(function () {
    iFrames.init();
});

$(window).resize(function () {
    iFrames.init();
});