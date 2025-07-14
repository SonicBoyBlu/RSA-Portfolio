function GenerateRandomPassword() {
    var length = 8,
        charset = "ABCDEFGHIJKLMNPQRSTUVWXYZ123456789",
        result = "";
    for (var i = 0, n = charset.length; i < length; ++i) {
        result += charset.charAt(Math.floor(Math.random() * n));
    }
    return result;
}

function RenewToken() {
            // ** Delete me ** now hanlded in c# //
            console.warn("Escapia token expired");
            $.ajax({
                url: "https://hsapi.escapia.com/dragomanadapter/hsapi/auth/token",
                cache: false,
                headers: {
                    Authorization: Login.config.Escapia.Encoded,
                    Host: "dispatch.homeaway.com"
                },
                success: function (d) {
                    $.ajax({
                        url: Login.config.url,
                        data: {
                            token: d.authorizationHeaderValue
                        },
                        success: function (res) {
                            Auth.config.tokens.homeAway = d.authorizationHeaderValue;
                            Auth.escapia.getUser(u);
                        },
                        error: function (xhr, error) {
                        }
                    });
                },
                error: function (xhr, error) {
                    console.log(xhr);
                }
            });
}
