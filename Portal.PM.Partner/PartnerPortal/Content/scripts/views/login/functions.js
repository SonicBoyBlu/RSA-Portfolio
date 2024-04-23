// u.EscapiaDetails = escapia;
// u.EscapiaDetails.Owner = data.results[0];
// u.EscapiaDetails.Property = data.results[0].ownedUnits[0];
// u.EscapiaDetails.Property.nativePMSID = overridePropertyID;
// u.EscapiaPropertyID = overridePropertyID;
// u.EscapiaUserID = data.results[0].nativePMSID;
//var overridePropertyID = 133161;



function GetEscapiaUser(u) {

    console.log('GetEscapiaUser called functions.js');
    // sliced int seperate function taken from ryans code
    //return;
    // Auth.escapia.renewToken(u);

    console.log('Auth.config.tokens.homeAway', Auth.config.tokens.homeAway);
    console.log('emailAddress', Auth.config.tokens.homeAway);

    $.ajax({
        url: "https://hsapi.escapia.com/dragomanadapter/hsapi/SearchOwners",
        method: "post",
        headers: {
            "Authorization": 'Bearer ' + Auth.config.tokens.homeAway,
            "x-homeaway-hasp-api-endsystem": "EscapiaVRS",
            "x-homeaway-hasp-api-pmcid": Login.config.Escapia.PMCID,
            "x-homeaway-hasp-api-version": 10
        },
        data: {
            specification: {
                emailAddress: u.Email
            },
            pageSize: 1,
            pageNumber: 1
        },
        async: false,
        success: function (data) {
            // is active logic
            console.log('SearchOwners data', data)

            debugger;
            if (!data) {
                alert('Owner not found in Escapia');
                window.location = "/logout";
                return;
            }

            if (data.totalCount == 0) {
                alert('No Properties found for Owner ' + u.Email);
                window.location = "/logout";
                return;
            }

            // TODO: quick/dirty user login work around

            // TEMP OVERRIDE
            debugger;
            var overridePropertyID = 133161;
            //u.EscapiaDetails.Property.nativePMSID = overridePropertyID;
            //u.EscapiaPropertyID = overridePropertyID;
            // *** END OVERRIDE
            var escapia = {
                Owner: data.results[0],
                //Property: d.results[0].ownedUnits[0],
                Property: {
                    nativePMSID: overridePropertyID
                }
            };
            u.EscapiaUserID = data.results[0].nativePMSID;
            u.EscapiaPropertyID = overridePropertyID;
            u.EscapiaDetails = escapia;
            /*
            u.EscapiaDetails.Owner = data.results[0];
            u.EscapiaDetails.Property = data.results[0].ownedUnits[0];
            u.EscapiaUserID = data.results[0].nativePMSID;
            u.EscapiaPropertyID = data.results[0].ownedUnits[0].nativePMSID;
            */
            sessionStorage.setItem("user", JSON.stringify(u));

            window.location = "/";
        },
        error: function () {
            alert('Escapia API Error : SearchOwners')
            // Auth.escapia.renewToken(u);
        }
    });

    return u;
}
