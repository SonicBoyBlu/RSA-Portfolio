var WindowMinMobileSize = 991.98;

$(function () {
    // Collapse sidebar for small laptop
    if ($(window).width() <= WindowMinMobileSize) {
        $('.app-sidebar').addClass('collapsed');
    }

    sidebarToggle();
});

$(window).resize(function () {
    // Collapse sidebar for small laptop on resize
    if ($(window).width() <= WindowMinMobileSize) {
        $('.app-sidebar').addClass('collapsed');
    } else {
        $('.app-sidebar').removeClass('collapsed');
    }

    // Remove body hidden on resize
    if ($(window).width() <= 767.98 && $('.app-sidebar').hasClass('collapsed')) {
        $('body').removeClass('hidden');
    }

    // Change Datepicker direction on resize
    //AcmeInclude.datePicker();
});

sidebarToggle = function() {
    $('.sidebar-toggle').on('click', function () {

        if ($(window).width() >= 768) {
            if ($('.app-sidebar').hasClass('collapsed')) {
                $('.app-sidebar').addClass('slide-left');
                setTimeout(function () {
                    $('.app-sidebar').removeClass('collapsed');
                    $('.app-sidebar').removeClass('slide-left');
                }, 300);
                setTimeout(function () {
                    $('.app').css('overflow', 'initial');
                }, 1000);
            } else {
                $('.app').css('overflow', 'hidden');
                $('.app-sidebar').addClass('slide-left');
                setTimeout(function () {
                    $('.app-sidebar').removeClass('slide-left');
                    $('.app-sidebar').addClass('collapsed');
                }, 300);
            }
        } else if ($(window).width() <= 767.98) {
            $('.app-sidebar').toggleClass('collapsed');
        }

        if ($(window).width() <= 767.98 && $('.app-sidebar:not(.collapsed)')) {
            $('body').addClass('hidden');
        }

        if ($(window).width() <= 767.98 && $('.app-sidebar').hasClass('collapsed')) {
            $('body').removeClass('hidden');
        }
    });
};

$(".sidebar-nav .collapse").on("show.bs.collapse", function () {
    var i = $(this),
        id = i.attr("id"),
        a = $("a[href='#" + id  + "']"),
        icn = $(".fa-chevron-circle-down", a);
    icn.addClass("open");
});
$(".sidebar-nav .collapse").on("hide.bs.collapse", function () {
    var i = $(this),
        id = i.attr("id"),
        a = $("a[href='#" + id + "']"),
        icn = $(".fa-chevron-circle-down", a);
    icn.removeClass("open");
});