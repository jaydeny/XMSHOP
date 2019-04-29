
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
alert("ok");
// 渲染订单
var orderTemplate = function () {
    return "<li><div class='flex-2'><div style='float: left;margin: 0 10px 0 0;'><img src=' /></div>< p >" + obj.GoodsName + "</p ></div > <div class='flex-1'>￥<span>" + obj.GoodsPrice+"</span></div> <div class='flex-1'><small>X</small><span>"+obj+"</span></div> <div class='flex-1'>￥<span>1888</span></div><div class='flex-1'><a>以付款</a></div></li>";
}

$.post("/Agent/QryAgoods", function (data) {
    console.log(data);
},"json")

$.post("/Agent/GetAllGoodsInfo", function (data) {
    console.log(data);
},"json")
