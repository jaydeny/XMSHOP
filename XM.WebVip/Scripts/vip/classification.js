$(".goods-exhibition").on("click", ".p-button", function () {
    alert("立即下单");
});

var bounced = function (obj) {
    console.log("ok");
    $(obj.dialog).css({ "width": obj.width });
    $(obj.content).css({ "height": obj.height });
    $(obj.modal).modal('show');
    $.post(obj.url, function (data, status, xhr) {
        $(obj.body).html(data);
    })
}
//弹出商品详情
$(".goods-exhibition").on("click", "img,.p-title a", function () {
    var obj = {
        "modal": "#myModal", "dialog": "#dialog", "content": "#content", "body": "#body"
    };
    obj.width = "640px";
    obj.height = "440px";
    obj.url = "/home/GoodsDetails";
    bounced(obj);
});
// 渲染商品
var goodsRender = function(list) {
    $.each(list, function (i,n) {
        $(".goods-exhibition>ul").append(strGoods(n));
    });
}
// 弹出商品详情
$("#vipRegistered").click(function () {
    
});