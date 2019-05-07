// 修改个人信息

$.post("/vipinfo/VipInfo", function (data) {
        var obj = $.parseJSON(data.data).rows;
        $("#name").val(obj.VipAccountName);
        $("#email").val(obj.VipEmail);
        $("#tel").val(obj.VipMobliePhone);
}, "json");

// 修改个人信息
$("#btnUpdate").click(function () {
    var email = $.trim($("#email").val());
    var tel = $.trim($("#tel").val());
    var an = $.trim($("#name").val());
    $.post("/Home/Update", { "AN":an,"vip_mp": tel, "vip_Email": email }, function (data) {
        alert(data.msg);
    },"json");
});