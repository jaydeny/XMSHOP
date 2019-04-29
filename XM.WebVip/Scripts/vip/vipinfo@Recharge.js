// 充值
$(".vipinfo").on("click", "#top-up", function () {
    alert("");
    //$.post("/vip/Recharge", { recharge_price: }, function () {

    //})
})
var lastRechargeBtn = $(".number-list button.action");
$(".number-list button").click(function () {
    $(lastRechargeBtn).removeClass("action");
    $(this).addClass("action");
});
$("#top-up").click(function () {
    
});