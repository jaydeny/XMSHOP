
$("#yuan").text(100);
$("#fen").text(1000);

// 充值
$(".vipinfo").on("click", "#top-up", function () {
    $.post("/vipinfo/Recharge", { recharge_price: $(lastRechargeBtn).text() }, function (data) {
        if (data.success) {
            narn('success', "充值成功,请等待审核!")
        }
        else {
            narn('warn', "充值失败!")
        }
    }, "json")
})

var lastRechargeBtn = $(".number-list button.action");
$(".number-list button").click(function () {
    $(lastRechargeBtn).removeClass("action");
    $(this).addClass("action");
    lastRechargeBtn = $(this);
    $("#yuan").text($(lastRechargeBtn).text());
    $("#fen").text($(lastRechargeBtn).text() * 10)
});

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