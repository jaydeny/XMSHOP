
var code = "1";
var left = "glyphicon-arrow-left";
var right = "glyphicon-arrow-right";

$(".vipinfo-form").on("click", "#point", function () {
    if ($("#point").hasClass(left)) {
        $("#point").removeClass(left);
        $("#point").addClass(right);
        code = "1";
    } else {
        $("#point").removeClass(right);
        $("#point").addClass(left);
        code = "2";
    }
});

$(".vipinfo-form").on("click", "#RechargeToGame", function () {
    $.ajax({
        url: "/GameRecharge/Recharge",
        type: "post",
        data: { "money": $(Money).val(), "code": code },
        success: function (data) {
            var data = JSON.parse(data);
            //window.location.href = "/vipinfo/vipinfopage";
            if (data.success) {
                if (data.msg == "vip004") {
                    narn('success', "充值成功!")
                }
                narn('success', "提现成功!")
            }
            narn('warn', "充值失败!")
        }
    })
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