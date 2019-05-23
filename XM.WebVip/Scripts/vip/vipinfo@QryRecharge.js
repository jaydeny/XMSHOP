
var Template = function (obj) {
    var status;
    obj.status_id == 6 ? status = "审核中" : obj.status_id == 7 ? status="充值成功" : status="充值失败";
    return "<li><div class='flex-1'><span>" + obj.id + "</span></div><div class='flex-1'><span>" + obj.recharge_price + "</span></div><div class='flex-1'><span>" + obj.recharge_integral + "</span></div><div class='flex-1'><span>" + obj.recharge_time + "</span></div><div class='flex-1'><span>" + status + "</span></div>";
}

var QryRecharge = function () {
    $.ajax({
        url: "/VipInfo/QryRecharge",
        success: function (data) {
            console.log(data);
            var e = JSON.parse(data);
            $(".Recharge-list>ul>li:not(:first-child)").remove();
            $.each(e.rows, function (index, obj) {
                $(".Recharge-list>ul").append(Template(obj));
            });
        }
    })
}

QryRecharge();