// 修改个人信息

    $.post("/vip/VipInfo", function (data) {
        var obj = $.parseJSON(data.data).rows;
        $("#name").val(obj.VipAccountName);
        $("#email").val(obj.VipEmail);
        $("#tel").val(obj.VipMobliePhone);
}, "json");

$("#btnUpdate").click(function () {
    var email = $.trim($("#email").val());
    var tel = $.trim($("#tel").val());
    $.post("/vip/Update", { "vip_mp": tel, "vip_Email": email }, function (data) {
        console.log(data);
        if (data.success) {
            alert(data.msg);
        }
    });
});