$("#btnEmail").click(function () {
    var an = $.trim($("#name").val());
    $.post("/Home/FoundPwd", { "vip_AN": an },
        function (data) {
            if (data.success) {
                $("#btnEmail").text("已发送到邮箱,将关闭页面。");
                window.setTimeout(function () {
                    $('#myModal').modal('hide');
                }, 1000);
            }
            else {
                $("#btnEmail").text(data.msg);
                window.setTimeout(function () {
                    $("#btnEmail").text("确定");
                }, 1000);
            }
        }, "json");
});