// 注册
$("#btnSignin").click(function () {
    var an = $.trim($("#name").val());
    var pwd = $.trim($("#pwd").val());
    var confirm_pwd = $.trim($("#confirm_pwd").val());
    var email = $.trim($("#email").val());
    var tel = $.trim($("#tel").val());
    // 验证邮箱
    if (vailAN("#name_warning", an) && vailPwd("#pwd_warning", pwd) && vailConfPwd("#confirm_pwd_warning", pwd,confirm_pwd) && vailEmail("#email_warning", email) && vailPhone("#tel_warning", tel)) {

        $.post("/Home/Signin", { "vip_AN": an, "vip_mp": tel, "vip_Email": email, "vip_pwd": pwd, "status_id": 1, "agent_id": 2 },
            function (data) {
                if (data.success) {
                    $("#btnSignin").text("注册成功,将跳转到登录页面。");
                        window.setTimeout(function () {
                            var obj = {
                                "modal": "#myModal", "dialog": "#dialog", "content": "#content", "body": "#body"
                            };
                            obj.width = "400px";
                            obj.height = "400px";
                            obj.url = "/Home/Login";
                            bouncedLogin(obj);
                        }, 3000);
                }
                else {
                    $("#name_warning").text(data.msg);
                }
            },"json");
    }

});
