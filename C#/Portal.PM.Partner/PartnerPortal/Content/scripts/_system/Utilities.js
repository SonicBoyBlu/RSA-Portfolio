///Convert form data to JavaScript object
function objectifyForm(formArray) { //serialize data function

    var returnArray = {};
    for (var i = 0; i < formArray.length; i++) {
        returnArray[formArray[i]['name']] = formArray[i]['value'];
    }
    return returnArray;
}


var Utilities = {
    cache: {

    },
    init: function () {
        console.info("Utilities loaded");
    },
    format: {
        currency: function (number) {
            if (isNaN(number)) return "$0.00";
            else return "$" + (number).toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,');
        }
    },
    sort: {
        reservationsAsc: function (data) {
            return data.sort(function (a, b) {
                var x = a.stayDateRange.startDate; var y = b.stayDateRange.startDate;
                return ((x < y) ? -1 : ((x > y) ? 1 : 0));
            });
        },
        reservationsDesc: function (data) {
            return data.sort(function (a, b) {
                var x = a.stayDateRange.startDate; var y = b.stayDateRange.startDate;
                return ((x > y) ? -1 : ((x < y) ? 1 : 0));
            });
        }
    },
    calc: {
        nights: function (startDate, endDate) {
            var oneDay = 24 * 60 * 60 * 1000,    // hours*minutes*seconds*milliseconds
                firstDate = new Date(startDate),
                secondDate = new Date(endDate),
                diffDays = Math.round(Math.abs((firstDate.getTime() - secondDate.getTime()) / (oneDay)));
            return diffDays;
        }
    },
    reservations: {
        getType: function (id) {
            var type = "Hold";
            if (!Utilities.cache.reservationTypes) {
                $.ajax({
                    url: "https://hsapi.escapia.com/dragomanadapter/hsapi/ListReservationTypes",
                    headers: ajaxHeaders.homeAway,
                    async: false,
                    success: function (d) {
                        Utilities.cache.reservationTypes = d;
                    }
                });
            }
            try {
                var obj = $.grep(Utilities.cache.reservationTypes, function (obj) { return obj.nativePMSID === id; })[0];
                type = obj.type;
            }
            catch { var i = 0; }
            return type;
        },
        getTypeClass: function (id) {
            var css = "dark";
            switch (id) {
                case 1: css = "danger"; break;
                case 0: css = "success"; break;
                case 16: css = "warning"; break;
                case 17: css = "info"; break;
                case 19: css = "info"; break;
            }
            return css;
        }
    },
    sound: {
        play: {
            toggle: function () {
                Utilities.sound.exec("switch/switch-8.mp3");

            }
        },
        exec: function (sound) {
            var baseUrl = "https://www.soundjay.com/";
            new Audio(baseUrl + sound).play();

        }
    }
};
$(function () {
    Utilities.init();
});

$(document).on("click change hover focus", "select option, option", function () {
    try {
        console.log("Select: " + $(this).text());
        new Audio("/Content/sounds/button-50-tick.mp3").play();
    }
    catch (ex) { console.error(ex.message); }
});

$(document).on("focus", "input, textarea", function () {
    var i = $(this),
        t = i.attr("type");
    console.log("is type-clone: " + !i.hasClass("type-clone"));
    if (!i.hasClass("type-clone") && "checkbox|radio|number".indexOf(t) < 0) // t !== "checkbox" && t !== "radio")
        i.putCursorAtEnd();
});
$(document).on("focus", ".type-clone", function () {
    var i = $(this),
        v = i.val(),
        p = i.data("prefix");
    if (p && !v) i.val(p);
    //i.val(i.val());
    i.putCursorAtEnd();
});
$(document).on("keyup change", ".type-clone", function () {
    var i = $(this),
        p = i.data("prefix"),
        v = i.val(),
        //v = (p || "") + i.val(),
        t = i.attr("type"),
        c = $(i.data("target"));
    switch (t) {
        case "url":
            if (v.indexOf("http") < 0)
                v = "http://" + v;
            c.html("<a href='" + v + "' target='_blank' title='Click to view site'><i class='fa fa-globe'></i> " + v + "</a>");
            break;
        case "tel": c.html("<a href='tel:" + v + "' target='_blank' title='Click to call'><i class='fa fa-phone'></i> " + v + "</a>"); break;
        case "email": c.html("<a href='mailto:" + v + "' target='_blank' title='Click to email'><i class='fa fa-envelope'></i> " + v + "</a>"); break;
        default:
            c.text(v);
            break;
    }

});