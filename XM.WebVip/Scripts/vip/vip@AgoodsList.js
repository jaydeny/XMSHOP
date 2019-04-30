$(".goods-exhibition").on("click", ".p-button", function () {
    alert("立即下单");
});

// 弹出商品框
//var bounced = function (obj) {
//    console.log("ok");
//    $(obj.dialog).css({ "width": obj.width });
//    $(obj.content).css({ "height": obj.height });
//    $(obj.modal).modal('show');
//    $.post(obj.url, function (data, status, xhr) {
//        $(obj.body).html(data);
//    })
//}
//弹出商品详情
//$(".goods-exhibition").on("click", "img,.p-title a", function () {
//    var obj = {
//        "modal": "#myModal", "dialog": "#dialog", "content": "#content", "body": "#body"
//    };
//    obj.width = "640px";
//    obj.height = "440px";
//    obj.url = "/home/GoodsDetails";
//    bounced(obj);
//});

// 商品模板
var strGoods = function (obj) {
    return "<li><div class='goods-item' ><p class='p-img'><a><img /></a></p><p class='p-title'><a><span>" + obj.goods_name + "</span><span class='red'>亮点</span></a></p><p class='p-price'><b>￥" + obj.price + "</b></p><div class='p-button'><a class='' data-id=" + obj.id+" >立即下单</a></div></div ></li >";
}

// 渲染商品
var goodsRender = function (list) {
    $.post("/vip/qryagoods", function (data) {
        if (data.total > 0) {
            $.each(data.rows, function (i, n) {
                $(".goods-exhibition>ul").append(strGoods(n));
            });
        }
    },"json")
}
$(".goods-exhibition").on("error", "img", function () {
    $(this).unbind('error'); //防止替换图片加载失败后陷入无限循环
    $(this).attr('src', '~/img/failure.png');
})
goodsRender(new Array(10));