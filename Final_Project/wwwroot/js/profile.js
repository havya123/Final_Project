 console.log("Working...") 
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

    
var fileImage;
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
        getProfile("abc@gmail.com"); 
    }
var name;
var id;
//fetch data User
function getProfile(grp) {  
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
                    console.log(dataUser)
                    $("#txtDOB").text(dataUser.dob);
                    name = dataUser.name;
                    id = dataUser.id;
                    $("#nameUser").text(dataUser.name);
                    $("#emailUser").text(dataUser.email);
                    $("#phoneUser").val(dataUser.phone);
                    dataUser.gender == "Man" ? $("#checkedMan").attr('checked', true) : $("#checkedWoman").attr('checked', true);
                    $("#emailUserChange").val(dataUser.email.split('@gmail.com').join(''));
                } 
            }
            else
                alert(res.message); 
        },
        failure: function (res) {  },
        error: function (res) { }
    });
}
//allow user change there name 
$("#showInputName").val(name);
function showChangeName() { 
    if ($("#showInputName").hasClass("form-control") ) {
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

// 
function save() {
    if ($("#emailUser").text() != "" && $("#emailUser").text() != null && $("#emailUser").text() != undefined) {
        var itm = {  
            Id: id,
            Phone: $("#phoneUser").val(),
            Email: $("#emailUser").text(), 
            Name: $("#showInputName").val(),
            Gender: $('#checkedMan').is(":checked") ? "Man" : "Woman",
            Avatar: fileImage
        };
        Avatar: fileImage
        const formData = new FormData();
        formData.append("data", itm); 
        $.ajax({
            type: "POST",
            url: "/profile/update_profile",
            data: { 'data': formData },
            dataType: 'json',
            contentType: false,
            processData: false,
            success: function (res) {
                if (res.success) {
                    alert("Update successfully !!");
                    let c = res.data;
                    console.log(c);
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


//Change Password
function changePass() { 
    if ($("#newPass").val() == $("#confirmPass").val()) {
        const itm = {
            Password: $("#currentPass").val(),
            NewPassword: $("#newPass").val()
        }
    console.log(itm);
    } else {
        alert("Password isn't same");
    }
}