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
    var an = $.trim($("#name").val());
    $.post("/vip/Update", { "AN":an,"vip_mp": tel, "vip_Email": email }, function (data) {
        alert(data.msg);
    },"json");
});