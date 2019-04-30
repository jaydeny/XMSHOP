$("#confirmUpdate").click(function () {
    var oldpwd = $.trim($("#oldpwd").val());
    var newpwd = $.trim($("#newpwd").val());
    if (newpwd != $.trim($("#confirmPwd").val())) {
        alert("两次密码不一致。")
        return false;
    }
    $(".newpwd_warning").text("");
    $.post("/vip/UpdatePwd", { oldPwd: oldpwd, vip_pwd: newpwd }, function (data) {
        alert(data.msg);
    }, "json")
});