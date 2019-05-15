
$("#RechargeToGame").click(function () {
    $.ajax({
        url: "/GameRecharge/Recharge",
        type: "post",
        data: { "money": $(lastRechargeBtn1).text(),"code":"1" },
        success: function (data) {
            var data = JSON.parse(data);
            alert(data.msg)
        }
    })
});

var lastRechargeBtn1 = $(".number-list1 button.action");
$(".number-list1 button").click(function () {
    $(lastRechargeBtn1).removeClass("action");
    $(this).addClass("action");
    lastRechargeBtn1 = $(this)
});


$("#RechargeToShop").click(function () {
    $.ajax({
        url: "/GameRecharge/Recharge",
        type: "post",
        data: { "money": $(lastRechargeBtn2).text(), "code": "2" },
        success: function (data) {
            var data = JSON.parse(data);
            alert(data.msg)
        }
    })
});

var lastRechargeBtn2 = $(".number-list2 button.action");
$(".number-list2 button").click(function () {
    $(lastRechargeBtn2).removeClass("action");
    $(this).addClass("action");
    lastRechargeBtn2 = $(this)
});
