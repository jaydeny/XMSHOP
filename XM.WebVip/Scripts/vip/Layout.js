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
            }
            else
            {
                narn('warn',"请登录后重试")
            }
        }, "json")
    });


    // 进入个人信息页
    //$("#vip_name").click(function () {
    //    setIntegral();
    //});


    //进入游戏
    $("#LoginGame").click(function () {
        $.post("/GameHome/Login", function (data) {
            if (data.success) {
                var e = JSON.parse(data.msg)
                window.open(e.result,"_black");
            } else {
                narn('warn',"请登录后进入游戏!")
            }

        }, "json")
    });

    //查询积分
    $("#GetCredit").click(function () {
        var type = 'log';
        var text;
        $.post("/GameHome/GetCredit", function (data) {
            if (data.success) {
                var e = JSON.parse(data.msg)
                type = 'success';
                text = '您当前的积分:' + e.result[0].Integral
            }
            else {
                type = 'warn';
                text = "请登录后查询积分";
            }
            narn(type,text)
        }, "json")
    });

    //提示框弹出方法
    function narn(type,text) {
        naranja()[type]({
            title: '温馨提示',
            text: text,
            timeout: '5000',
            buttons: [{
                text: '接受',
                click: function (e) {
                    naranja().success({
                        title: '通知',
                        text: '通知被接受'
                    })
                }
            }, {
                text: '取消',
                click: function (e) {
                    e.closeNotification()
                }
            }]
        })
    }
}