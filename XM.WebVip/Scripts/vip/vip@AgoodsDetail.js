//商品数量减一
$(".minus").click(function () {
    var x = Number($.trim($("#count").val()));
    if (x > 1) {
        $("#count").val(x - 1);
    } else {
        narn("log","最低买一个")
    }
})

//商品数量加一
$(".plus").click(function () {
    var x = Number($.trim($("#count").val()));
    if (x < 50) {
        $("#count").val(x + 1);
    } else {
        narn("log", "请理性消费")
    }
})

//添加购物车
$("#AddCart").click(function () {
    $.ajax({
        url: "/ShoppCart/EditCart",
        data: { "editType": 1, "AgoodsID": $("#agoods_id").val(), "count": $.trim($("#count").val())},
        success: function (data) {
            if (data.success) {
                narn("success","添加购物车成功!")
            } else {
                narn("warn","添加购物车失败!")
            }
        }
    })
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