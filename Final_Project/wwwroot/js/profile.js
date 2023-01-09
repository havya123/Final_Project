 console.log("Working...") 
 var Id;
var name; 
var fileImage;
var listOrder = null;
//change style off button when click change pages

$.each($(".select-button"), (function (key, item) {
    $(item).click(() => {
        $(".select-button").removeClass("button-list-change");
        if ($(this).hasClass("button-list-change")) {
            $(item).removeClass("button-list-change");
        } else {
            $(this).addClass("button-list-change");
        }
    });
}))

    
//Change Avatar when update from client and show image out UI
function readURL(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $('#avatarShow').attr('src', e.target.result);
        }
        reader.readAsDataURL(input.files[0]);
        fileImage = input.files[0];
    }
}
$(".imgInp").change(function () {
    readURL(this);
});
//set default page is MainProfile
LoadDialog("/Profile/MainProfile");
function GetAjaxData(url, method, datatype, data) {
    var response;
    $.ajax({
        async: false,
        type: method,
        url: url,
        data: data,
        dataType: datatype,
        contentType: "application/json",
        success: function (res) {
            response = res;
        },
        error: function (err) {
            response = err;
        }
    });
    return response;
} 


//insert data of pages in table tags when button with class = "orderRouter" is click 
$(".orderRouter").click(function () {
    var url = $(this).attr("data-url");
    LoadDialog(url);
})
function LoadDialog(loadurl) {
    this.counter = undefined;
    var htmlcontent = GetAjaxData(loadurl, "GET", "html");
    $("#changeRouter").html(htmlcontent);
    getProfile(localStorage.getItem("Email"));
}

//fetch data User
function getProfile(grp) {
    loadingOn();
    $.ajax({
        type: "POST",
        url: "/profile/get_profile",
        data: { 'Email': grp},
        async: false,
        success: function (res) {
            if (res.success) {
                let data = res.data;
                const dataUser = data.data[0];
                if (dataUser != null && dataUser != undefined) { 
                    console.log(dataUser); 
                    $("#txtDOB").val(dataUser.dob.split('T00:00:00').join(''));  
                    name = dataUser.username; 
                    Id = dataUser.id;
                    $("#nameUser").text(dataUser.username);
                    $("#emailUser").text(dataUser.email); 
                    $("#phoneUser").val(dataUser.phone);
                    dataUser.gender == "Man" ? $("#checkedMan").attr('checked', true) : $("#checkedWoman").attr('checked', true);
                    $("#emailUserChange").val(dataUser.email.split('@gmail.com').join(''));
                }
            } 
            else {
            loadingOff();
                alert(res.message);
}
        }, 
        failure: function (res) { loadingOff() },
        error: function (res) { loadingOff() }
    });
}

//allow user change there name 
$("#showInputName").val(name);
function showChangeName() {
    if ($("#showInputName").hasClass("form-control")) {
        $("#showInputName").hide();
        $("#nameUser").show();
        $("#showInputName").removeClass("form-control");
    } else {
        $("#showInputName").addClass("form-control");
        $("#showInputName").show();
        $("#showInputName").focus();
        $("#nameUser").hide();
    }
}

// save information about user was change
function save() {
    if ($("#emailUser").text() != "" && $("#emailUser").text() != null && $("#emailUser").text() != undefined) {
        var itm = {  
            Id: Id,
            Phone: $("#phoneUser").val(),
            Email: $("#emailUser").text(),
            Username: $("#showInputName").val(),
            Gender: $('#checkedMan').is(":checked") ? "Man" : "Woman", 
            DOB: $("#txtDOB").val()
        };
        console.log(itm)
        $.ajax({
            type: "POST",
            url: "/profile/update_profile",
            data: { 'data': itm },
            success: function (res) {
                if (res.success) {
                    alert("Update successfully !!"); 
                }
                else
                    alert(res.message);
            },
            failure: function (res) { },
            error: function (res) { }
        });
    }
} 
$("#change-profile").click(function (e) {
    e.preventDefault();
    save();
})

$("#dataOrder").click(function (e) {
    e.preventDefault();
    get_order();
})

//Change Password or email
function changePass() { 
    const email = $("#emailUserChange").val();
    const currentPass = $("#currentPass").val(); 
    const newPass = $("#newPass").val();
    const confirmPass = $("#confirmPass").val()

    if (newPass === "" && confirmPass === "" && email !== "" && currentPass !== "") {
        const itm = {
            email: $("#emailUserChange").val().trimEnd() + '@gmail.com',
            password: currentPass,
            Id: Id
        }
        $.ajax({
            type: "POST",
            url: "/profile/change_email",
            data: itm,
            async: false,
            success: function (res) {
                if (res.success) {
                    alert("Update Email Sucess!!");
                    localStorage.setItem("Email", $("#emailUserChange").val().trimEnd() + '@gmail.com') 
                }
                else
                    alert(res.message);
            },
            failure: function (res) { },
            error: function (res) { }
        });
    }
    else if (newPass === "" && confirmPass === "" && email !== "") {
        alert("Need input password");
        $("#currentPass").addClass("is-invalid");
    }
    else {
        if (newPass == confirmPass) {
            const itm = {
                Id: Id,
                oldPass: currentPass,
                newPass: newPass
            } 
            console.log(itm);
            $.ajax({
                type: "POST",
                url: "/profile/change_pass",
                data: itm,
                async: false,
                success: function (res) {
                    if (res.success)
                        alert("Update PassWord Sucess!!");
                    else
                        alert(res.message);
                },
                failure: function (res) { },
                error: function (res) { }
            });
        } else {
            alert("Password isn't same");
            $("#confirmPass").addClass(" is-invalid");
            $("#confirmPass").focus();
        }
    }

}

function get_order() { 
    loadingOn();
    $.ajax({
        type: "POST",
        url: "/profile/get_order",
        data: { "IdUser": Id },
        success: function (res) {
            if (res.success) { 
                let data = res.data;  
                if (data.data != null && data.data != undefined) {  
                    let dataRes = [];
                    for (var i = 0; i < data.data.length; i++) {
                        let itm = data.data[i];   
                        dataRes.push(itm); 
                    }
                    listOrder = dataRes; 
                    $("#tbodyResult").html("");
                    $("#courseTemplate").tmpl(data.data).appendTo("#tbodyResult");
                }
            }
            else {
                loadingOff();
                alert(res.message);
            }
        },
        failure: function (res) { loadingOff(); },
        error: function (res) { loadingOff(); }
    });
}
 
function showDetail(id) {
    if (listOrder != null && id != null && id >= 0) {
        let itm = $.grep(listOrder, function (obj) {
            return obj.id == id;
        })[0]; 
        $("#txtID").val(itm.id);
        $("#product").text(itm.product);
        $("#quantity").text(itm.quantity);
        $("#total").text(itm.total); 
    }
}

function cancelOrder(id) {
    let con = confirm("Do you want cancel this order?")
    if (con == false)
        return;

    if (id != null && id != undefined && id >= 0) { 
        $.ajax({
            type: "POST",
            url: "/profile/cancel_order",
            data: { "Id": id },
            success: function (res) {
                if (res.success) {
                    alert("Cancel successfull");
                    get_order();
                }
                else { 
                    alert(res.message);
                }
            },
            failure: function (res) { alert("Cancel fail") },
            error: function (res) { alert("Cancel fail") }
        });
    }
}

function loadingOff() {
    document.getElementById("loader").style.display = "none";
}

function loadingOn() {
    document.getElementById("loader").style.display = "block";
}