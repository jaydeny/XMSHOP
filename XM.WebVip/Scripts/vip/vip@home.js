
/*调用模态框*/
// 登录
window.onload = function () {
    var obj = {
        "modal": "#myModal", "dialog": "#dialog", "content": "#content", "body": "#body"
    };
    $("#vipLogin").click(function () {
        obj.width = "400px";
        obj.height = "400px";
        obj.url = "/vip/Login";
        bouncedLogin(obj);
    });
    // 注册
    $("#vipRegistered").click(function () {
        obj.width = "400px";
        obj.height = "550px";
        obj.url = "/vip/Signin";
        bouncedLogin(obj);
    });

    //退出
    $("#vipExit").click(function () {
        $.get("/vip/RemoveSession", function (data, status, xhr) {
            if (data.success) {
                // 清空数据
                window.setTimeout(function () {
                    window.location.href = "/vip/Index";
                }, 500);
            }
        }, "json")
    });
    //获取登录信息
    var loginInfo = function () {
        $.post("/vip/VipInfo", function (data) {
            if (data.success) {
                $("#vip_info_name").text($.parseJSON(data.data).rows.VipAccountName);
                $("#onlogin_box").addClass("hidden");
                $("#login_box").removeClass("hidden");
                $("#vip_name").text($.parseJSON(data.data).rows.VipAccountName);
                $('#myModal').modal('hide');
            }
        }, "json")
    }
    loginInfo();
}

// 进入商品赛选页面
$(".screening-goods").click(function () {
    window.location.href = "/vip/AgoodsList"
});



