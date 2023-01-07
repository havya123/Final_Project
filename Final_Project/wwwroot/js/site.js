// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function signOut() {
    if (confirm("Do you want to sign out ?") == true) {
        $.ajax({
            type: "POST",
            url: "/Login/signOut",
            async: false,
            success: function (res) {
                if (res.success) {
                    document.location.href = "/";
                }
                else
                    alert(res.message);
            },
            failure: function (res) { },
            error: function (res) { },
        });
    }
}