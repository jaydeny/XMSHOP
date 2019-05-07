
var keyValue = $.request("keyValue");
$(function () {
    initControl();
    if (!!keyValue) { //判断是否有值
        $.ajax({
            url: "/Goods/",
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
        url: "/Goods/GoodsAdd?id=" + keyValue,
        param: $("#form1").formSerialize(), ser,
        success: function () {
            $.currentWindow().$("#gridList").trigger("reloadGrid");
        }
    })
}