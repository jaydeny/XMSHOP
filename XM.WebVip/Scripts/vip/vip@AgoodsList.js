
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

$(".filter_box").on("click", ".type", function () {
    $(".filter_box .type-action").removeClass("type-action");
    $(this).addClass("type-action");
    paging.currentPage = 1;
    var id = $(this).data("id");
    getQryAgoods(id);
});

// 商品集合
var listGoods
// 商品模板
var strGoods = function (i,obj) {
    return "<li><div class='goods-item' ><p class='p-img'><a><img src='/image/" + obj.goods_pic + "'  /></a></p><p class='p-title'><a><span>" + obj.goods_name + "</span><span class='red'>" + obj.goods_intro + "</span></a></p><p class='p-price'><b>￥" + obj.price + "</b></p><div class='p-button' data-id=" + i +" ><a class='' >立即下单</a></div></div ></li >";
}
var getQryAgoods = function (typeId) {
    $.post("/Product/QryAgoods", { rows: paging.pageTotal, page: paging.currentPage, type_id: typeId }, function (data) {
        if (data.total > 0) {
            $(".goods-exhibition>.empty").hide();
            listGoods = data.rows;
            $(".goods-exhibition>ul").html("");
            $.each(data.rows, function (i, n) {
                $(".goods-exhibition>ul").append(strGoods(i, n));
            });
            // 回到顶端
            document.body.scrollTop = document.documentElement.scrollTop = 100;
            //总条数
            paging.total = data.total;
            paging.renderPaging();
        }
        else {
            $(".goods-exhibition>ul").html("");
            paging.total = data.total;
            paging.renderPaging();
            $(".goods-exhibition>.empty").show();
        }
    }, "json");
}

// 渲染商品
var goodsRender = function () {
    // 页面条数
    paging.pageTotal = 15;
    paging.callbackMethod = function () {
        var typeId = $(".filter_box .type-action").data("id");
        getQryAgoods(typeId);
    }
    // 回调
    paging.callbackMethod();
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