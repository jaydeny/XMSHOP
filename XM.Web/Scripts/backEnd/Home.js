
$("#home_exit").click(function () {
    $.get("/login/UserLoginOut", function (data) {
        if (data.success) {
            // window.location.href = window.location.host+"/Login"; 
            window.location.href = "/Login";
        }
    }, "json")
});