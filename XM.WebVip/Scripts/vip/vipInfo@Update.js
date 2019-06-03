// 修改个人信息
//$.post("/vipinfo/VipInfo", function (data) {
//        var obj = $.parseJSON(data.data).rows;
//        $("#name").val(obj.VipAccountName);
//        $("#email").val(obj.VipEmail);
//        $("#tel").val(obj.VipMobliePhone);
//}, "json");

// 修改个人信息
$("#btnUpdate").click(function () {
    var email = $.trim($("#email").val());
    var tel = $.trim($("#tel").val());
    var an = $.trim($("#name").val());
    if (vailEmail("#warningEmail", email) && vailPhone("#warningTel", tel)) {
        $.post("/Home/Update", { "AN": an, "vip_mp": tel, "vip_Email": email }, function (data) {
            if (data.success) {
                narn('success', data.msg)
            }
            else {
                narn('warn', data.msg)
            }
        }, "json");
    }
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