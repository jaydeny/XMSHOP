
// 安全退出
$("#home_exit").click(function () {
    $.get("/Login/UserLoginOut", function (data) {
        if (data.success) {
            window.location.href = "/Login";
        }
    }, "json")
});



