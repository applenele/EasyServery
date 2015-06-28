$(document).ready(function () {



    $.post("/UserApi/Login", { username: "test",password:"test" }, function (data) {
        console.log(data);
    })


    //$.post("/UserApi/Register", { username: "test", password: "test" }, function (data) {
    //      console.log(data);
    //})



});