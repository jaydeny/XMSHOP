
// 安全退出
$("#home_exit").click(function () {
    $.get("/login/UserLoginOut", function (data) {
        if (data.success) {
            window.location.href = "/Login";
        }
    }, "json")
});

//用户权限




