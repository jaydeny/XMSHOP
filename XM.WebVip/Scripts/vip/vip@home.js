
// 进入商品赛选页面
$(".home-menu").on("click", ".screening-goods", function () {
    window.location.href = "/product/AgoodsList"
});

// 热销模板
var HotGoods = function (obj) {
    return "<li><a class='screening-goods' ><div class='info'><p class='img'><img alt='图片' src='/img/" + obj.goods_pic + "'></p><div class='title'>" + obj.goods_name + "</div><p class='desc'>" + obj.goods_intro + "</p><p class='price'>¥" + obj.price + "</p><p class='tips' style='display: none'><em class=''><span class=''>标签</span></em></p></a></li >";
}

// 获取热销
$.post("/product/HotGoods", function (data) {
    $.each(data.rows, function (i, n) {
        $(".hot_melt ul").append(HotGoods(n));
    });
}, "json");

//获取推存 
$.post("/product/BoutiqueGoods", function (data) {
    $.each(data.rows, function (i, n) {
        $(".quality_goods ul").append(HotGoods(n));
    });
},"json")

