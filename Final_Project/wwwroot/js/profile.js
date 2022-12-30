 console.log("Working...")
$(document).ready(function () {
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
        }
    }
    $(".imgInp").change(function () {
        readURL(this);
    });  
    //set default page is MainProfile
    LoadDialog("/Profile/MainProfile");
    //
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
    }
});
   