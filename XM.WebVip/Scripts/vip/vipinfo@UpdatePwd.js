$("#confirmUpdate").click(function () {
    var oldpwd = $.trim($("#oldpwd").val());
    var newpwd = $.trim($("#newpwd").val());
    var confirmPwd = $.trim($("#confirmPwd").text());
    if (vailPwd("#oldpwdWarning", oldpwd) && vailPwd("#newpwdWarning", newpwd) && vailConfPwd("#newpwdWarning", newpwd, confirmPwd)) {
        $.post("/Home/UpdatePwd", { vip_AN: vip_AN, oldPwd: oldpwd, vip_pwd: newpwd }, function (data) {
            if (data.success) {
                $("#oldpwd").val("");
                $("#newpwd").val("");
                $("#confirmPwd").val("");
                alert("修改成功,将安全退出跳转到首页!");
                // 安全退出
                $.get("/Home/RemoveSession", function (data, status, xhr) {
                    if (data.success) {
                        // 清空数据
                        window.setTimeout(function () {
                            window.location.href = "/Home/Index";
                        }, 500);
                    }
                }, "json")
            } else {
                narn('warn', data.msg)
            }
        }, "json")
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