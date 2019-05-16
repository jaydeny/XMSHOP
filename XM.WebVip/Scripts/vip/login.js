
$("#login_account").click(function () {
    if (!$(this).hasClass("selected")) {
        $(this).addClass("selected");
        $("#login_note").removeClass("selected");
        $("#account_div").removeClass("hidden");
        $("#note_div").addClass("hidden");
        $(".form-control").val("");
    }
});
$("#login_note").click(function () {
    if (!$(this).hasClass("selected")) {
        $(this).addClass("selected");
        $("#login_account").removeClass("selected");
        $("#note_div").removeClass("hidden");
        $("#account_div").addClass("hidden");
        $(".form-control").val("");
    }
});
// 注册弹框
$("#register").click(function () {
    var obj = {
        "modal": "#myModal", "dialog": "#dialog", "content": "#content", "body": "#body"
    };
    obj.width = "400px";
    obj.height = "550px";
    obj.url = "/Home/Signin";
    bouncedLogin(obj);
})
// 忘记密码弹框
$("#back").click(function () {
    var obj = {
        "modal": "#myModal", "dialog": "#dialog", "content": "#content", "body": "#body"
    };
    obj.width = "400px";
    obj.height = "300px";
    obj.url = "/Home/FoundPwdPage";
    bouncedLogin(obj);
})

// 登录
$("#bntLogin").click(function () {
    var an = $.trim($("#txt_an").val());
    var pwd = $.trim($("#txt_pwd").val());
    if (an == "") {
        $(".hint>p").html("账号不能为空。");
    }
    else if (pwd == "") {
        $(".hint>p").html("密码不能为空。");
    }
    else {
        $.post("/Home/Login", { "AN": an, "pwd": pwd }, function (data, status, xhr) {
            if (data.success) {
                //$("#onlogin_box").addClass("hidden");
                //$("#login_box").removeClass("hidden");
                //$("#vip_name").text(data.data.vip_AN);
                //$('#myModal').modal('hide');
                window.location.href = '/Home';
                $("#Game").href = "/GameHome/Login"
            }
            else {
                $(".hint>p").html(data.msg);
            }
        }, "json");
    }
});

