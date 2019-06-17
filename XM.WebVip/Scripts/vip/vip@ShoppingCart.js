var sp1 = parseInt($(".price").data("val"));
var sp2 = parseInt($(".count").data("val"));
(function getMoney(e, r) {
    var res = parseInt(e * r);
    $(".priceTotal").text(res)
}(sp1, sp2));


//删除购物车中的一项
$(".delete").click(function () {
    var itemID = $(this).data("val");
    $.ajax({
        url: "/ShoppCart/EditCart",
        data: { "editType": 2, "itemID": itemID },
        success: function (data) {
            var e = JSON.parse(data)
            if (e.success) {
                window.location.href = "/ShoppCart/ShoppingCartPage";
            } else {
                narn("warn", "删除失败")
            }
        }
    })
}) 

//商品数量减一
$(".minus").click(function () {
    var that = $(this).closest("div").children(".count");
    var x = Number($.trim($(that).val()));
    
    var AgoodsID = $(this).closest("div.shopping-row").data("val");
    if (x > 1) {
        $(that).val(x - 1);
        $.ajax({
            url: "/ShoppCart/EditCart",
            data: { "editType": 1, "AgoodsID": AgoodsID , "count":x-1 },
            success: function (data) {
                var e = JSON.parse(data)
                if (e.success) {
                    window.location.href = "/ShoppCart/ShoppingCartPage";
                } else {
                    narn("warn", "修改失败")
                }
            }
        })
    } else {
        narn("log", "最低买一个")
    }
})

//商品数量加一
$(".plus").click(function () {
    var that = $(this).closest("div").children(".count");
    var x = Number($.trim($(that).val()));

    var AgoodsID = $(this).closest("div.shopping-row").data("val");
    if (x < 50) {
        $(that).val(x + 1);
        $.ajax({
            url: "/ShoppCart/EditCart",
            data: { "editType": 1, "AgoodsID": AgoodsID, "count": x + 1 },
            success: function (data) {
                var e = JSON.parse(data)
                if (e.success) {
                    window.location.href = "/ShoppCart/ShoppingCartPage";
                } else {
                    narn("warn", "修改失败")
                }
            }
        })
    } else {
        narn("log", "请理性消费")
    }
})

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

// 点击下单
$("#Orders").click(function () {
    var buys = [];
    var list = $("#agoods .check:checked")
    //购物车项id的集合
    var itemID = [];

    $.each(list, function (index, obj) {
        id = $(obj).closest("div.shopping-row").data("val");
        count = $(obj).closest("div.shopping-row").data("count");
        proTotal = $(obj).closest("div.shopping-row").find(".price").data("val");
        buys.push({ "proID": id, "count": count, "proTotal": proTotal, "addressID": $("#Add").val(), "acID": $("#Ac").val() });

        itemid = $(obj).closest("div.shopping-row").data("itemid");
        itemID.push(itemid)
    })
    $.ajax({
        url: "/Shop/BuyToPro",
        method: 'post',
        contentType: 'application/json',
        data: JSON.stringify(buys),
        dataType: "json",
        success: function (data) {
            if (!data.success) {
                narn("warn", "余额不足!")
            } else {
                narn("success", "购物成功")
            }
        }
    }).done(function () {
            BuyDelete(itemID);
        })
})

//购物后删除购物车项
function BuyDelete(items) {
    console.log(items)
    $.ajax({
        url: "/ShoppCart/deleCarts",
        method: 'post',
        contentType: 'application/json',
        data: JSON.stringify(items),
        success: function (data) {
            console.log(data)
            window.location.href = "/ShoppCart/ShoppingCartPage";
        }
    })
}

//选择购物项
$("#agoods").on("click", ".check", function () {
    if ($(this).prop("checked")) {
        var count = $(this).closest("div.shopping-row").data("count")
        var price = $(this).closest("div.shopping-row").find(".price").data("val")
        var result = Number(count * price)
        var origin = Number($("#total").text())
        $("#total").text(parseInt(result + origin))
        $("#choose").text(parseInt(Number($("#choose").text()) + 1))
    }
    else {
        var count = $(this).closest("div.shopping-row").data("count")
        var price = $(this).closest("div.shopping-row").find(".price").data("val")
        var result = Number(count * price)
        var origin = Number($("#total").text())
        $("#total").text(parseInt(origin - result))
        $("#choose").text(parseInt($("#choose").text() - 1))
    }
})

//全选
$(".checkAll").click(function () {
    var boo = $(this).prop("checked");
    var list = $("#agoods .check").prop("checked", boo);

    if (boo) {
        var sum = 0;
        $("#choose").text(list.length)

        $.each(list, function (index, obj) {
            var count = Number($(obj).closest("div.shopping-row").data("count"))
            var price = Number($(obj).closest("div.shopping-row").find(".price").data("val"))
            sum += parseInt(count * price)
        })
        $("#total").text(sum)
    } else {
        $("#choose").text(0)
        $("#total").text(0)
    }
})

//批量删除
$(".deleteAll").click(function () {
    var list = $("#agoods .check:checked")
    //购物车项id的集合
    var itemID = [];

    $.each(list, function (index, obj) {
        itemid = $(obj).closest("div.shopping-row").data("itemid");
        itemID.push(itemid)
    })

    $.ajax({
        url: "/ShoppCart/deleCarts",
        method: 'post',
        contentType: 'application/json',
        data: JSON.stringify(itemID),
        success: function (data) {
            var e = JSON.parse(data)
            if (e.success) {
                window.location.href = "/ShoppCart/ShoppingCartPage";
            } else {
                narn("warn","删除失败")
            }
        }
    })
})