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
    $("#btnSearch").click(function () {
        var search = $.trim($("#txt_search").val());
        if (search != "") {
            window.location.href = "/product/AgoodsList?search=" + search;
        }
    });
    
    // 公告
    $("#notice").click(function () {
        $("#notice").data("id", $(this).data("id"));
        obj.width = "600px";
        obj.height = "400px";
        obj.url = "/Notice/Notice";
        bouncedLogin(obj);
    })
    // 获取公告
    function getNotice() {
        $.get("/Notice/GetNotice", function (data) {
            if (data.total == "-1") {
                var rows = data.rows;
                var count = rows.result.length;
                for (var i = 0; i < rows.result.length; i++) {
                    for (var j = 0; j < rows.msgStatus.length; j++) {
                        if (rows.result[i]._id == rows.msgStatus[j].msgid) {
                            count--;
                            break;
                        }
                    }
                }
                if (count > 0) {
                    $("#span_notice").addClass("red").text(count);
                }
            }
            else if (data.total > 0) {
                $("#span_notice").addClass("red").text(data.total);
            }
        }, "json")
    }
    getNotice();

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


    // 进入个人信息页
    $("#vip_name").click(function () {
        setIntegral();
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
                alert("你的积分为:"+e.result[0].Integral)
            }
            else {
                alert(data.msg);
                window.location.href = "/Home/Index";
            }
        }, "json")
    });

    var setIntegral = function () {

        $.post("/GameHome/GetCredit", function (data) {

        }, "json").done(
                        function (data) {
                            var e = JSON.parse(data.msg)
                            $.ajax({
                                url: "/vipInfo/getCredit",
                                data: { "Integral": e.result[0].Integral },
                                success: function () {
                                    $.get("/vipinfo/vipinfo", function (data) {
                                        if (data.success) {
                                            window.location.href = "/vipinfo/vipinfopage";
                                        } else {
                                            alert(data.msg);
                                            window.location.href = "/home/index";
                                        }
                                    }, "json")
                                }
                            })
                        }
                    )
    }
}