
$(".vipinfo-nav a").click(function () {

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
$(".vipinfo-nav .title").click(function () {
    window.location.href = "/vip/vipinfopage";
});
// 订单模板
var orderTemplate = function () {
    return "<li><div class='flex-2'><div style='float: left;margin: 0 10px 0 0;'><img src=' /></div>< p >" + obj.GoodsName + "</p ></div > <div class='flex-1'>￥<span>" + obj.GoodsPrice+"</span></div> <div class='flex-1'><small>X</small><span>"+obj+"</span></div> <div class='flex-1'>￥<span>1888</span></div><div class='flex-1'><a>以付款</a></div></li>";
}

// 查询订单
$.post("/vip/QryAgoods", function (data) {
    if (data.total > 0) {
        $("#empty_order").addClass("hidden");
        $("#order_box").removeClass("hidden");
    }
    else {
        $("#empty_order").removeClass("hidden");
        $("#order_box").addClass("hidden");
    }
}, "json")







