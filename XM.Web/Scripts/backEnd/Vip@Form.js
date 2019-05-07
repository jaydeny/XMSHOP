
var keyValue = $.request("keyValue");
$(function () {
    initControl();
    if (!!keyValue) { //判断是否有值
        $.ajax({
            url: "/Vip/GetFormJson",
            data: { id: keyValue },
            dataType: "json",
            async: false,
            success: function (data) {
                $("#form1").formSerialize(data);
                $("#vip_AN").attr('disabled', 'disabled');
            }
        });
    }
});
function initControl() {

    // 获取代理商
    $.post("/Agent/GetAllUserInfo", function (data) {
        $.each(data.rows, function (i, obj) {
            $("#agent_id").append("<option value='" + obj.AgentID + "'>" + obj.AgentAccountName + "</option>")
        })
    }, "json");

}
function submitForm() {
    if (!$('#form1').formValid()) {
        return false;
    }
    $.submitForm({
        url: "/Vip/AddUser?id=" + keyValue,
        param: $("#form1").formSerialize(),
        success: function (data) {
            console.log(data);
            $.currentWindow().$("#gridList").trigger("reloadGrid");
        }
    })
}