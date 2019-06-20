// 分页
var paging = new Paging("#paging-box");
//弹出商品框
var bounced = function (obj) {
    $(obj.dialog).css({ "width": obj.width });
    $(obj.content).css({ "height": obj.height });
    $(obj.modal).modal('show');
    $.post(obj.url, function (data, status, xhr) {
        $(obj.body).html(data);
    })
}

$(".filter_box").on("click", ".type", function () {
    $(".filter_box .type-action").removeClass("type-action");
    $(this).addClass("type-action");
    $("#typeName").text($(this).text());
    paging.currentPage = 1;
    getQryAgoods();
});
// 商品排序
$(".filter_box").on("click", ".sort", function () {
    if ($(this).hasClass("sort-action")) {
        if ($(this).hasClass("sort-asc")) {
            $(this).removeClass("sort-asc");
            $(this).addClass("sort-desc").data("order", "desc");
        }
        else {
            $(this).removeClass("sort-desc");
            $(this).addClass("sort-asc").data("order", "asc");
        }
    }
    else {
        $(".filter_box .sort-action").removeClass("sort-action").removeClass("sort-asc").removeClass("sort-desc");
        $(this).addClass("sort-action").addClass("sort-asc").data("order", "asc");
    }
    paging.currentPage = 1;
    getQryAgoods();
});

// 商品集合
var listGoods
// 商品模板
var strGoods = function (i, obj) {
    return "<li><div class='goods-item' ><p class='p-img d-button' data-id=" + i + "><a><img src='/image/" + obj.goods_pic + "'  /></a></p><p class='p-title'><a><span>" + obj.goods_name + "</span><span class='red'>" + obj.goods_intro + "</span></a></p><p class='p-price'><b>￥" + obj.price + "</b></p><div class='p-button' data-id=" + i + " ><a class='' >立即下单</a></div></div ></li >";
}
var getQryAgoods = function () {
    var typeId = $(".filter_box .type-action").data("id");
    var name = $("#txt_search").val();
    var sort = $(".filter_box .sort-action").data("val");
    var order = $(".filter_box .sort-action").data("order");
    $.post("/Product/QryAgoods", { rows: paging.pageTotal, page: paging.currentPage, type_id: typeId, goods_Name: name, sort: sort, order: order }, function (data) {
        if (data.total > 0) {
            $(".goods-exhibition>.empty").hide();
            listGoods = data.rows;
            $(".goods-exhibition>ul").html("");
            $.each(data.rows, function (i, n) {
                $(".goods-exhibition>ul").append(strGoods(i, n));
            });
            // 回到顶端
            document.body.scrollTop = document.documentElement.scrollTop = 0;
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
        getQryAgoods();
    }
    // 回调
    paging.callbackMethod();
}

var obj;
// 立即下单,让客户选择活动
$(".goods-exhibition").on("click", ".p-button", function () {
    obj = listGoods[$(this).data("id")];
    $.post("/Shop/ChooseAcPage",  function (data) {
        if (data.success) {
            var Form = {
                "modal": "#myModal", "dialog": "#dialog", "content": "#content", "body": "#body"
            };
            Form.width = "500px";
            Form.height = "400px";
            Form.url = "/Shop/ChooseAc";
            bouncedLogin(Form);
        } else {
            narn('warn',"请登录后重试")
        }
    }, "json")

});

//选择活动后,购物的方法
$(".choose-main").on("click", "#ChooseAc", function () {
    var Ac = $(this).data('val')
    $.post("/Shop/buy", { agoods_id: obj.id, buy_count: 1, order_total: obj.price, buy_total: obj.price * 1, Ac_id: Ac }, function (data) {
        if (data.success) {
            narn('success', "购物成功")
            $("#myModal").modal('hide');
        } else {
            if (data.msg == "vip013") {
                narn('warn', "用户余额不足,请充值后从试!")
            }
            narn('warn', "购物出错,请重试!")
        }
    }, "json")
});

//弹出商品详情
$(".goods-exhibition").on("click", ".d-button", function () {
    var agoods = listGoods[$(this).data("id")];
    var obj = {
        "modal": "#myModal", "dialog": "#dialog", "content": "#content", "body": "#body"
    };
    obj.width = "640px";
    obj.height = "440px";
    obj.url = "/ShoppingCart/AgoodsDetail?id="+agoods.id;
    bounced(obj);
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

// 请求商品
var search = decodeURI(getQueryVariable("search"));
if (search != "false") {
    $("#txt_search").val(search);
}
// 初始商品
goodsRender();

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