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
                alert(data.msg);
            }
        }, "json")
    }
});