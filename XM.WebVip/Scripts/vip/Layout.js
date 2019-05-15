window.onload = function () {

/*调用模态框*/
// 登录
    var obj = {
        "modal": "#myModal", "dialog": "#dialog", "content": "#content", "body": "#body"
    };
    $("#vipLogin").click(function () {
        obj.width = "400px";
        obj.height = "400px";
        obj.url = "/Home/Login";
        bouncedLogin(obj);
    });
    // 注册
    $("#vipRegistered").click(function () {
        obj.width = "400px";
        obj.height = "550px";
        obj.url = "/Home/Signin";
        bouncedLogin(obj);
    });

    // 安全退出
    var safetyExit = function () {
        $.get("/Home/RemoveSession", function (data, status, xhr) {
            if (data.success) {
                // 清空数据
                window.setTimeout(function () {
                    window.location.href = "/Home/Index";
                }, 500);
            }
        }, "json")
    }
    // 退出
    $("#vipExit").click(function () {
        safetyExit();
    });


    // 商品查寻
    //$("#btnSearch").click(function () {
    //    var search = $.trim($("#txt_search").val());
    //    if (search != "") {
    //        window.location.href = "/product/AgoodsList?search=" + search;
    //    }
    //});
}