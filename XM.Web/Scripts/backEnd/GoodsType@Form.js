// 获取编号
var keyValue = $.request("keyValue");
$(function () {
    initControl();
    if (!!keyValue) { //判断是否有值
        $.ajax({
            url: "/Type/GetFormJson",
            data: { id: keyValue },
            dataType: "json",
            async: false,
            success: function (data) {
                $("#form1").formSerialize(data);
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
        url: "/Type/Save?id=" + keyValue,
        param: $("#form1").formSerialize(), ser,
        success: function () {
            $.currentWindow().$("#gridList").trigger("reloadGrid");
        }
    })
}