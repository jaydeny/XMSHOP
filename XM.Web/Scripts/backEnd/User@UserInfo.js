
$(function () {
    $('.wrapper').height($(window).height() - 11);
    $("#submitInfo").click(function () {
        if (!$('#formInfo').formValid()) {
            return false;
        }
        $.submitForm({
            url: "/User/UpdataInfo",
            param: $("#formInfo").formSerialize(),
            success: function () {
            }
        })
    });
    $("#submitPwd").click(function () {
        if (!$('#formPwd').formValid()) {
            return false;
        }
        $.submitForm({
            url: "/User/UpdatePwd",
            param: $("#formPwd").formSerialize(),
            success: function (data) {
                console.log(data);
                if (data.success) {
                    window.setTimeout(function () {
                        // 页面跳转
                        top.window.location.href = "/Login/Index";
                    }, 500);
                }
            }
        })
    });
})











