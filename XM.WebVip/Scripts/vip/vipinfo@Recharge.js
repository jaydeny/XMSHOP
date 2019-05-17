
$("#yuan").text(100);
$("#fen").text(1000);

// 充值
$(".vipinfo").on("click", "#top-up", function () {
    $.post("/vipinfo/Recharge", { recharge_price: $(lastRechargeBtn).text() }, function (data) {
        alert(data.msg);
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

