
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
                console.log(data);
                $("#form1").formSerialize(data);
                $("#VipAccountName").attr('disabled', 'disabled');
                $("#AgentID").val(data.AgentID);
            }
        });
    }
});
function initControl() {
    console.log(top.clients.agents);
    $.each(top.clients.agents, function (i, n) {
        $("#AgentID").append("<option value='" + n.AgentID + "'>" + n.AgentAccountName + "</option>");
    });

}
function submitForm() {
    if (!$('#form1').formValid()) {
        return false;
    }
    $.submitForm({
        url: "/Vip/Save?id=" + keyValue,
        param: $("#form1").formSerialize(),
        success: function (data) {
            $.currentWindow().$("#gridList").trigger("reloadGrid");
        }
    })
}