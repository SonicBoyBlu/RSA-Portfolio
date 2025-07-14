var Locations = {
    init: function (stateid) {
        console.log("Initializing Locations.js...");
        this.fetch.states(stateid);
    },
    fetch: {
        states: function (id) {
            var c = $("#view-locations-list");
            console.log("Fetch Location List...");
            $.ajax({
                method: "get",
                url: "/Locations/List/States",
                cache: false,
                data: { ID: id },
                success: function (d) {
                    //debugger;
                    c.html(d);
                },
                beforeSend: function () {
                    c.html('<div class="loading-img">Loading...</div>');
                },
                error: function () {
                    c.addClass("alert-danger");
                    c.html("Oops, an error has occured.")
                }
            });
        }
    }
};

$(document).on("click", "[data-state-id]", function (e) {
    e.preventDefault();
    var i = $(this),
        id = i.data("state-id");
    console.log("State ID: " + id);
    //alert("State ID: " + id);
    //window.location = "/Locations?id=" + id;
    Locations.fetch.states(id);
});
