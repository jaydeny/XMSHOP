// 用户修改密码js
$("#submitPwdUpdate").click(function () {
    var oldpwd = $.trim($("#oldPwd").val());
    var newpwd = $.trim($("#newPwd").val());
    if ($.trim($("#oldPwd").val())=="") {
        $("#oldPwd_warning").text("请输入原密码");
        return false;
    }
    $("#oldPwd_warning").text("");
    if ($.trim($("#newPwd").val()) == "") {
        $("#newPwd_warning").text("请输入新密码");
        return false;
    }
    $("#newPwd_warning").text("");
    if (newpwd != $.trim($("#confirmPwd").val())) {
        $("#confirmPwd_warning").text("两次密码不一致");
        return false;
    }
    $("#confirmPwd_warning").text("");
    $.post("/User/UpdatePwd", { UserPwd: oldpwd, NewPwd: newpwd }, function (data) {
        if (data.success) {
            $("#oldPwd").val("");
            $("#newPwd").val("");
            $("#confirmPwd").val("");
            alert(data.msg);
            // 安全退出
            $.get("/login/UserLoginOut", function (data) {
                if (data.success) {
                    // 清空数据
                    window.setTimeout(function () {
                        top.location.href = "/Login/Index";
                    }, 500);
                }
            }, "json")
        } else {
            alert(data.msg);
        }
    }, "json")
});
