
/*调用模态框*/
// 登录
window.onload = function () {
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
    // 获取登录信息
    var loginInfo = function () {
        $.post("/vipinfo/VipInfo", function (data) {
            if (data.success) {
                $("#vip_info_name").text($.parseJSON(data.data).rows.VipAccountName);
                $("#onlogin_box").addClass("hidden");
                $("#login_box").removeClass("hidden");
                $("#vip_name").text($.parseJSON(data.data).rows.VipAccountName);
                $("#Remainder").text($.parseJSON(data.data).rows.Remainder);
                $('#myModal').modal('hide');
            }
        }, "json")
    }

    // 进入商品赛选页面
    $(".home-menu").on("click",".screening-goods", function () {
        window.location.href = "/product/AgoodsList"
    });

    // 热销模板
    var HotGoods = function (obj) {
        return "<li><a class='screening-goods' ><div class='info'><p class='img'><img alt='图片' src='/img/" + obj.goods_pic + "'></p><div class='title'>" + obj.goods_name + "</div><p class='desc'>" + obj.goods_intro + "</p><p class='price'>¥" + obj.price+"</p><p class='tips' style='display: none'><em class=''><span class=''>标签</span></em></p></a></li >";
    }

    // 获取热销
    $.post("/product/HotGoods", function (data) {
        $.each(data.rows, function (i, n) {
            $(".hot_melt ul").append(HotGoods(n));
        });
    },"json");

    //获取推存 
    $.post("/product/BoutiqueGoods", function (data) {
        $.each(data.rows, function (i, n) {
            $(".quality_goods ul").append(HotGoods(n));
        });
    },"json")

    // 初始化
    //loginInfo();

    // 商品查寻
    $("#btnSearch").click(function () {
        var search = $.trim($("#txt_search").val());
        if (search != "")
        {
            window.location.href = "/product/AgoodsList?search=" + search;
        }
    });
}

$("#LoginGame").click(function () {
    $.post("/GameHome/Login", function (data) {
        console.log(data);
        var e = JSON.parse(data.msg)
        window.location.href = e.result;

    }, "json")
});

$("#GetCredit").click(function () {
    $.post("/GameHome/GetCredit", function (data) {
        console.log(data);
        var e = JSON.parse(data.msg)
        alert(e.result[0].Integral)
    }, "json")
});