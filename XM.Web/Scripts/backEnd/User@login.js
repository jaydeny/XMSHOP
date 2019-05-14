(function ($) {
    $.login = {
        formMessage: function (msg) {
            $('.login_tips').find('.tips_msg').remove();
            $('.login_tips').append('<div class="tips_msg"><i class="fa fa-question-circle"></i>' + msg + '</div>');
        },
        loginClick: function () {
            var $username = $("#txt_account");
            var $password = $("#txt_password");
            if ($username.val() == "") {
                $username.focus();
                $.login.formMessage('请输入登录名。');
                return false;
            } else if ($password.val() == "") {
                $password.focus();
                $.login.formMessage('请输入登录密码。');
                return false;
            } else {
                $("#login_button").attr('disabled', 'disabled').find('span').html("登录...");
                $.ajax({
                    url: "/Login/CheckUserLogin",
                    data: { user_AN: $.trim($username.val()), user_pwd : $.trim($password.val()) },
                    type: "post",
                    dataType: "json",
                    success: function (data) {
                        if (data.success) {
                            $('.login_tips').find('.tips_msg').remove();
                            $("#login_button").find('span').html("登录成功，正在跳转...");
                            window.setTimeout(function () {
                                window.location.href = "/Home/Index";
                            }, 500);
                        } else {
                            $("#login_button").removeAttr('disabled').find('span').html("登录");
                            $("#switchCode").trigger("click");
                            $.login.formMessage(data.msg);
                        }
                    }
                });
            }
        },
        init: function () {
            $('.wrapper').height($(window).height());
            $(".container").css("margin-top", ($(window).height() - $(".container").height()) / 2 - 50);
            $(window).resize(function (e) {
                $('.wrapper').height($(window).height());
                $(".container").css("margin-top", ($(window).height() - $(".container").height()) / 2 - 50);
            });
            $("#login_button").click(function () {
                $.login.loginClick();
            });
            document.onkeydown = function (e) {
                if (!e) e = window.event;
                if ((e.keyCode || e.which) == 13) {
                    document.getElementById("login_button").focus();
                    document.getElementById("login_button").click();
                }
            }
        }
    };
    $(function () {
        $.login.init();
    });
})(jQuery);