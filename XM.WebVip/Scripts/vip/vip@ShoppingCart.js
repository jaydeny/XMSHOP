//弹出商品框
var bounced = function (obj) {
    $(obj.dialog).css({ "width": obj.width });
    $(obj.content).css({ "height": obj.height });
    $(obj.modal).modal('show');
    $.post(obj.url, function (data, status, xhr) {
        $(obj.body).html(data);
    })
}



//删除购物车中的一项
$(".delete").click(function () {
    var itemID = $(this).data("val");
    alert(itemID)
    $.ajax({
        url: "/ShoppCart/EditCart",
        data: { "editType": 2, "itemID": itemID },
        success: function (data) {
            var e = JSON.parse(data)
            if (e.success) {
                narn("success", "删除成功")
            } else {
                narn("warn", "删除失败")
            }
        }
    })
}) 

//商品数量减一
$(".minus").click(function () {
    var that = $(this).closest("div").children("#count");
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
                    narn("success", "修改成功")
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
    var that = $(this).closest("div").children("#count");
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
                    narn("success", "修改成功")
                } else {
                    narn("warn", "修改失败")
                }
            }
        })
    } else {
        narn("log", "请理性消费")
    }
})

//点击下单,让用户选择地址
$("#Orders").click(function () {
    var obj = {
        "modal": "#myModal", "dialog": "#dialog", "content": "#content", "body": "#body"
    };
    obj.width = "640px";
    obj.height = "440px";
    obj.url = "/ShoppingCart/ChooseAddress";
    bounced(obj);
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


var result = [];
$("#Orders").click(function () {
    var list = $("#agoods .check:checked")
    $.each(list, function (index, obj) {
        id = $(obj).closest("div.shopping-row").find("#count").data("val")
        count = $(obj).closest("div.shopping-row").data("val")
        result.push({ "addressID": addressID, "count": count, "proID": id, "proTotal": 单价, "tcID":活动id});
    })
})