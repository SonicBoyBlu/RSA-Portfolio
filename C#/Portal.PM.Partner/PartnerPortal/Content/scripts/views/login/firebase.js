// https://firebase.google.com/docs/auth/web/google-signin
// Initialize Firebase
var config = {
    apiKey: "AIzaSyDOtxBDMbEo6NxbIWQb3wY2pczafp6ZJBU",
    authDomain: "employee-portal-d7582.firebaseapp.com",
    databaseURL: "https://employee-portal-d7582.firebaseio.com",
    projectId: "employee-portal-d7582",
    storageBucket: "",
    messagingSenderId: "33198576852"
};

//debugger;
if (firebase) console.log("Firebase loaded");
else console.warn("Firebase NOT loaded");
if (!firebase.apps.length) {
    firebase.initializeApp(config);
}


var provider = new firebase.auth.GoogleAuthProvider();
provider.addScope("profile");
provider.addScope("email");
firebase.auth().onAuthStateChanged(function (user) {
    var u = user;
    console.log("user", u);
    if (u) {
        localStorage.setItem("g-user", JSON.stringify(user));
        Auth.login();
    } else {
        firebase.auth().signOut();
        localStorage.removeItem("g-user");
    }
});
/*
firebase.auth().signInWithEmailAndPassword(email, password).then(function (result) {


})
.catch(function (error) {
    // Handle Errors here.
    var errorCode = error.code;
    var errorMessage = error.message;
    // ...
});
*/

$(document).on("click", "#btn-login-google", function () {
    //*
    firebase.auth().signInWithPopup(provider).then(function (result) {

        // This gives you a Google Access Token. You can use it to access the Google API.
        var token = result.credential.accessToken;
        // The signed-in user info.
        var user = result.user;
        // ...
        localStorage.setItem("g-user", JSON.stringify(result.user));
        Auth.login();
    }).catch(function (error) {

        // Handle Errors here.
        var errorCode = error.code;
        var errorMessage = error.message;
        // The email of the user's account used.
        var email = error.email;
        // The firebase.auth.AuthCredential type that was used.
        var credential = error.credential;
        // ...
    });
    //*/
    /*
    firebase.auth().getRedirectResult().then(function (result) {

        if (result.credential) {
            // This gives you a Google Access Token. You can use it to access the Google API.
            var token = result.credential.accessToken;
            // ...
        }
        // The signed-in user info.
        var user = result.user;
        localStorage.setItem("g-auth", JSON.stringify(result));
        localStorage.setItem("g-user", JSON.stringify(result.user));

    }).catch(function (error) {

        // Handle Errors here.
        var errorCode = error.code;
        var errorMessage = error.message;
        // The email of the user's account used.
        var email = error.email;
        // The firebase.auth.AuthCredential type that was used.
        var credential = error.credential;
        // ...
    });
    //*/
});

