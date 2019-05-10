
var keyValue = $.request("keyValue");
console.log(keyValue);
$(function () {
    initControl();
    if (!!keyValue) { //判断是否有值
        $.ajax({
            url: "/Agent/GetFormJson",
            data: { id: keyValue },
            dataType: "json",
            async: false,
            success: function (data) {
                console.log(data);
                $("#form1").formSerialize(data);
                $("#AgentAccountName").attr('disabled', 'disabled');
            }
        });
    }
});
function initControl() {
}
function submitForm() {
    if (!$('#form1').formValid()) {
        return false;
    }
    $.submitForm({
        url: "/Agent/Save?id=" + keyValue,
        param: $("#form1").formSerialize(),
        success: function () {
            $.currentWindow().$("#gridList").trigger("reloadGrid");
        }
    })
}