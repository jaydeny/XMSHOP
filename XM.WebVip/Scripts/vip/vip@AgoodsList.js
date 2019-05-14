
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
var strGoods = function (i,obj) {
    return "<li><div class='goods-item' ><p class='p-img'><a><img src='/img" + obj.goods_pic + "'  /></a></p><p class='p-title'><a><span>" + obj.goods_name + "</span><span class='red'>" + obj.goods_intro + "</span></a></p><p class='p-price'><b>￥" + obj.price + "</b></p><div class='p-button' data-id=" + i +" ><a class='' >立即下单</a></div></div ></li >";
}

var listGoods

// 渲染商品
var goodsRender = function (parameter) {
    $.post("/Product/QryAgoods", parameter, function (data) {
        console.log(data);
        if (data.total > 0) {
            listGoods = data.rows;
            $.each(data.rows, function (i, n) {
                $(".goods-exhibition>ul").append(strGoods(i,n));
            });
        }
    },"json")
}
// 立即下单
$(".goods-exhibition").on("click", ".p-button", function () {
    var obj = listGoods[$(this).data("id")];
    //console.log(obj);
    $.post("/Shop/buy", { goods_id: obj.goods_id, buy_count: 1, order_total: obj.price, buy_total: obj.price * 1 }, function (data) {
        if (data.success) {
            alert(data.msg);
        } else {
            alert(data.msg);
        }
    },"json")
});
// 获得url的参数
function getQueryVariable(variable) {
    var query = window.location.search.substring(1);
    var vars = query.split("&");
    for (var i = 0; i < vars.length; i++) {
        var pair = vars[i].split("=");
        if (pair[0] == variable) { return pair[1]; }
    }
    return (false);
}
// 初始商品
goodsRender();
// 请求商品
var search = getQueryVariable("search");

//if (search != null && search != "") {
//    goodsRender({ Agoods_Name: search });
//}
//else {
//    goodsRender();
//}