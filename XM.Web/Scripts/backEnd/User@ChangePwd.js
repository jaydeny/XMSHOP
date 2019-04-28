(function ($) {
    $.login = {
        formMessage: function (msg) {
            $('.login_tips').find('.tips_msg').remove();
            $('.login_tips').append('<div class="tips_msg"><i class="fa fa-question-circle"></i>' + msg + '</div>');
        },
        loginClick: function () {
            var $newPwd = $("#newPwd");
            var $oldPwd = $("#oldPwd");
            if ($newPwd.val() == "") {
                $newPwd.focus();
                $.login.formMessage('请输入新密码。');
                return false;
            } else if ($oldPwd.val() == "") {
                $oldPwd.focus();
                $.login.formMessage('请输入确定密码。');
                return false;
            } else if ($newPwd.val() != $oldPwd.val()) {
                $.login.formMessage('两次密码不一致。');
                return false;
            } else {
                $("#btnConfirm").attr('disabled', 'disabled').find('span').html("正在修改...");
                $.ajax({
                    url: "/APLogin/CheckLogin",
                    data: { username: $.trim($newPwd.val()) },
                    type: "post",
                    dataType: "json",
                    success: function (data) {
                        if (data.state == "success") {
                            $("#btnConfirm").find('span').html("修改成功，正在跳转...");
                            window.setTimeout(function () {
                                // 页面跳转
                                window.location.href = "/Home/Index";
                            }, 500);
                        } else {
                            $("#btnConfirm").removeAttr('disabled').find('span').html("确定");
                            $("#switchCode").trigger("click");
                            $code.val('');
                            $.login.formMessage(data.message);
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
            $("#btnConfirm").click(function () {
                $.login.loginClick();
            });
            document.onkeydown = function (e) {
                if (!e) e = window.event;
                if ((e.keyCode || e.which) == 13) {
                    document.getElementById("btnConfirm").focus();
                    document.getElementById("btnConfirm").click();
                }
            }
        }
    };
    $(function () {
        $.login.init();
    });
})(jQuery);