$(document).ready(function () {
    $("#reg").hide();
});


function login() {
    let email = $("#form2Example11").val();
    let pass = $("#form2Example22").val();
    console.log("Username: ", email);
    console.log("Password: ", pass);

    let s = check(email, pass);
    if (s == "") {
        let dataLogin = { Username: email, Password: pass };
        console.log(dataLogin);
        $.ajax({
            type: "POST",
            url: "/login/doLogin",
            data: { "data": dataLogin },
            async: false,
            success: function (response) {
                console.log(response);
                if (response.success) {
                    let usr = response.user;
                    alert("Hello !" + usr.fullname);
                    document.location.href = "/Home";
                }
                else {
                    alert("Error: " + response.message);
                }
            },
            failure: function (response) {
                console.log(response.responseText);
            },
            error: function (response) {
                console.log(response.responseText);
            }
        });
    }
    else {
        alert(s);
    }
}

function check(email, pass) {
    var s = "";
    if (email == "" || email == undefined || email == null)
        s += "Chưa nhập email !";
    if (pass == "" || pass == undefined || pass == null)
        s += "\nChưa nhập mật khẩu !"

    return s;

}

function regi() {
    $("#reg").show();
    $("#log").hide();
}

function btsignin() {
    $("#log").show();
    $("#reg").hide();
}

(function () {
    'use strict'
    var forms = document.querySelectorAll('.needs-validation')
    Array.prototype.slice.call(forms)
        .forEach(function (form) {
            form.addEventListener('submit', function (event) {
                if (!form.checkValidity()) {
                    event.preventDefault()
                    event.stopPropagation()
                }
                form.classList.add('was-validated')
            }, false)
        })
})()

function register(e) {
    e.preventDefault();
    e.stopPropagation();
    let obj = {
        username: $("#floatingInputUsername").val(),
        fullname: $("#floatingInputFullname").val(),
        password: $("#floatingPassword").val(),
        confirm: $("#floatingPasswordConfirm").val(),
        email: $("#floatingInputEmail").val(),
    }
    console.log(obj)
}