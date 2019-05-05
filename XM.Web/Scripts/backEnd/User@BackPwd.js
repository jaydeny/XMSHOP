(function ($) {
    $.login = {
        loginClick: function () {
            var $username = $("#txt_account");
            if ($username.val() == "") {
                $username.focus();
                $.login.formMessage('请输入登录名。');
                return false;
            } else {
                $("#btnConfirm").attr('disabled', 'disabled').find('span').html("申请中...");
                $.ajax({
                    url: "/APLogin/BackPwd",
                    data: { username: $.trim($username.val()) },
                    type: "post",
                    dataType: "json",
                    success: function (data) {
                        if (data.state == "success") {
                            $("#btnConfirm").find('span').html("请查收邮箱，正在跳转...");
                            window.setTimeout(function () {
                                // 回到登录页面
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
            // 设置页面宽高
            $('.wrapper').height($(window).height());
            $(".container").css("margin-top", ($(window).height() - $(".container").height()) / 2 - 50);
            $(window).resize(function (e) {
                $('.wrapper').height($(window).height());
                $(".container").css("margin-top", ($(window).height() - $(".container").height()) / 2 - 50);
            });

            // 登录事件
            $("#btnConfirm").click(function () {
                $.login.loginClick();
            });

            // 页面动画
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