var _modals = {
    config: {

    },
    init: function () {
        console.info("Init: modal");
        //_modals.render.wrapper();
        Forms.init();
    },
    render: {
        wrapper: function() {
            $(".modal .modal-content").each(function () {
                var i = $(this);
                if ($(".modal-content-wrapper", i).length === 0) {
                    var html = i.clone();
                    i.empty().prepend($("<div/>", {
                        class: "modal-content-wrapper"
                    }));
                    $(".modal-content-wrapper", i).html(html);
                }
            });
        }
    }
};

$(function () {
    console.log("Init modals.js");
    Utilities.Modals = _modals;
});

$(document).on("shown.bs.modal", ".modal", function () {
    //console.clear();
    console.log("Rendering Modal Wrapper");
    _modals.render.wrapper();
});

