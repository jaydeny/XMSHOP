var userInfoData;
$.get("/User/InfoUser", function (data) {
    userInfoData = data;
    $("#UserAccountName").val(data.UserAccountName);
    $("#UserEmail").val(data.UserEmail);
    $("#UserMobliePhone").val(data.UserMobliePhone);
}, "json");
$("#submitInfo").click(function () {
    var txtEmail = $("#UserEmail").val();
    var txtphone = $("#UserMobliePhone").val();
    userInfoData.UserEmail = txtEmail;
    userInfoData.UserMobliePhone = txtphone;
    $.post("/User/Save", userInfoData, function (data) {
        console.log(data);
        alert(data.msg);
    },"json");
});