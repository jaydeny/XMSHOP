
var code = "1";
var left = "glyphicon-arrow-left";
var right = "glyphicon-arrow-right";
$("#point").click(function () {
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


$("#RechargeToGame").click(function () {
    $.ajax({
        url: "/GameRecharge/Recharge",
        type: "post",
        data: { "money": $(Money).val(), "code": code },
        success: function (data) {
            var data = JSON.parse(data);
            alert(data.msg);
            window.location.href = "/vipinfo/vipinfopage";
        }
    })
});
