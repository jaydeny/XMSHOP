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



    // 进入个人信息页
    $("#vip_name").click(function () {
        $.get("/vipinfo/vipinfo", function (data) {
            if (data.success) {
                window.location.href = "/vipinfo/vipinfopage";
            } else {
                alert(data.msg);
                window.location.href = "/home/index";
            }
        }, "json")
    });


    //进入游戏
    $("#LoginGame").click(function () {
        $.post("/GameHome/Login", function (data) {
            if (data.success) {
                var e = JSON.parse(data.msg)
                //window.location.href = e.result;
                window.open(e.result,"_black");
            } else {
                alert(data.msg);
                window.location.href = "/Home/Index";
            }

        }, "json")
    });

    //查询积分
    $("#GetCredit").click(function () {
        $.post("/GameHome/GetCredit", function (data) {
            if (data.success) {
                var e = JSON.parse(data.msg)
                alert(e.result[0].Integral)
            }
            else {
                alert(data.msg);
                window.location.href = "/Home/Index";
            }
        }, "json")
    });
}