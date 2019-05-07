$("#confirmUpdate").click(function () {
    var oldpwd = $.trim($("#oldpwd").val());
    var newpwd = $.trim($("#newpwd").val());
    var vip_AN = $.trim($("#vip_name").text());
    if (newpwd != $.trim($("#confirmPwd").val())) {
        alert("两次密码不一致。")
        return false;
    }
    $(".newpwd_warning").text("");
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
});