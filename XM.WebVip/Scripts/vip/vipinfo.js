var last_a;
$(".vipinfo-nav a").click(function () {
    $(last_a).removeClass("action");
    $(this).addClass("action");
    last_a = $(this);
    $(this).addClass("action");
    if ($(this).prop("href") != "") {
        $.get($(this).prop("href"), function (data, status, xhr) {
            if (!$(".vipinfo-main .info-head").hasClass("hidden")) {
                $(".vipinfo-main .info-head").addClass("hidden");
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
    return "<li><div class='flex-1' ><div style='float: left;margin: 0 10px 0 0;'><img src='' /></div><p>" + obj.id + "</p></div ><div class='flex-1'>￥<span>" + obj.order_total + "</span></div><div class='flex-1'><small>X</small><span>1</span></div><div class='flex-1'>￥<span>" + obj.order_total + "</span></div><div class='flex-2'>" + obj.order_date.replace('T', ' ')+"</div></li >";
}

// 查询订单
$.post("/Shop/QryOrder", function (data) {
    console.log(data);
    if (data.total > 0) {
        $("#empty_order").addClass("hidden");
        $("#order_box").removeClass("hidden");
        $.post("/shop/QryOrder", function (data) {
            $.each(data.rows, function (i, n) {
                $(".order-list>ul").append(orderTemplate(n));
            });
        },"json")

    }
    else {
        $("#empty_order").removeClass("hidden");
        $("#order_box").addClass("hidden");
    }
}, "json")

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








