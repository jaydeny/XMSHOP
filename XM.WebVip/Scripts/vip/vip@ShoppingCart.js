//弹出商品框
var bounced = function (obj) {
    $(obj.dialog).css({ "width": obj.width });
    $(obj.content).css({ "height": obj.height });
    $(obj.modal).modal('show');
    $.post(obj.url, function (data, status, xhr) {
        $(obj.body).html(data);
    })
}

$("#Orders").click(function () {
    var obj = {
        "modal": "#myModal", "dialog": "#dialog", "content": "#content", "body": "#body"
    };
    obj.width = "640px";
    obj.height = "440px";
    obj.url = "/ShoppingCart/ChooseAddress";
    bounced(obj);
})

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
    if (x > 1) {
        $(that).val(x - 1);
    } else {
        narn("log", "最低买一个")
    }
})

//商品数量加一
$(".plus").click(function () {
    var that = $(this).closest("div").children("#count");
    var x = Number($.trim($(that).val()));
    if (x < 50) {
        $(that).val(x + 1);
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