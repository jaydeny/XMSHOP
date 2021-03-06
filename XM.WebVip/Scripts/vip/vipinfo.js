﻿var last_a;
function refreshJS(script) {
    var newScript = document.createElement('script');
    newScript.src = script + '?' + new Date().getTime();
    document.body.appendChild(newScript);
}
$(".vipinfo-nav a").click(function () {
    if ($(this).hasClass("action")) {
        return false;
    }
    $(last_a).removeClass("action");
    $(this).addClass("action");
    last_a = $(this);
    $(this).addClass("action");
    if ($(this).prop("href") != "") {
        var jsUrl = $(this).data("js");
        $.get($(this).prop("href"), function (data, status, xhr) {
            if (!$(".vipinfo-main .info-head").hasClass("hidden")) {
                $(".vipinfo-main .info-head").addClass("hidden");
            }
            if (jsUrl != undefined) {
                refreshJS(jsUrl);
            }
            $(".vipinfo-main .info-body").html(data);
        }, "html")
    }
    return false;
});
// 进入个人信息页
$(".vipinfo-nav .title").click(function () {
    window.location.href = "/vipinfo/vipinfopage";
});
// 订单模板
var orderTemplate = function (obj) {
    return "<li><div class='flex-1' ><div style='float: left;margin: 0 10px 0 0;'></div><p>" + obj.OrderID + "</p></div ><div class='flex-1'>￥<span>" + obj.OrderPrice + "</span></div><div class='flex-1'><small>X</small><span>1</span></div><div class='flex-1'>￥<span>" + obj.OrderPrice + "</span></div><div class='flex-2'>" + obj.OrderDate.replace('T', ' ')+"</div></li >";
}

// 查询订单
var qryOrder = function () {
    /*// 页面条数
    paging.pageTotal = 3;
    paging.callbackMethod = function () {
        $.post("/Shop/QryOrder", { rows: paging.pageTotal, page: paging.currentPage }, function (data) {
            if (data.total > 0) {
                $("#empty_order").addClass("hidden");
                $("#order_box").removeClass("hidden");
                $(".order-list>ul").html("");
                $.each(data.rows, function (i, n) {
                    $(".order-list>ul").append(orderTemplate(n));
                });
                //总条数
                paging.total = data.total;
                paging.renderPaging();
            }
            else {
                $("#empty_order").removeClass("hidden");
                $("#order_box").addClass("hidden");
            }
        }, "json")
    }
    // 回调
    paging.callbackMethod();*/
    var paging = new Paging("#paging-box");
    paging.pageTotal = 3;
    paging.callbackMethod = function () {
        $.post("/Shop/QryOrder", { rows: paging.pageTotal, page: paging.currentPage }, function (data) {
            if (data.total > 0) {
                $("#empty_order").addClass("hidden");
                $("#order_box").removeClass("hidden");
                $(".order-list>ul").html("");
                $.each(data.rows, function (i, n) {
                    $(".order-list>ul").append(orderTemplate(n));
                });
                //总条数
                paging.total = data.total;
                paging.renderPaging();
            }
            else {
                $("#empty_order").removeClass("hidden");
                $("#order_box").addClass("hidden");
            }
        }, "json")
    }
    // 回调
    paging.callbackMethod();
}
qryOrder();

// 去充值
$("#go-top-up").click(function () {
    last_a = $("#a_top-up");
    $(last_a).addClass("action");
    $.get($(last_a).prop("href"), function (data, status, xhr) {
        if (!$(".vipinfo-main .info-head").hasClass("hidden")) {
            $(".vipinfo-main .info-head").addClass("hidden");
        }
        $(".vipinfo-main .info-body").html(data);
    }, "html")
});


//重新请求游戏积分
$("#load").click(function () {
    setIntegral();
});

var setIntegral = function () {
    $.get("/vipinfo/vipinfo", function (data) {
        if (data.success) {
            window.location.href = "/vipinfo/vipinfopage";
        } else {
            narn('warn', 请登录后重试)
            window.location.href = "/home/index";
        }
    }, "json")
}


//提示框弹出方法
function narn(type, text) {
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





