
var keyValue = $.request("keyValue");
$(function () {
    initControl();
    if (!!keyValue) { //判断是否有值
        $.ajax({
            url: "/Menu/GetFormJson",
            data: { id: keyValue },
            dataType: "json",
            async: false,
            success: function (data) {
                $("#form1").formSerialize(data);
                //$("#UserAccountName").attr('disabled', 'disabled');
            }
        });
    }
});

function initControl() {
    // 获取角色
    $.ajax({
        url: "/Menu/GetAllMenu",
        data: { rows: 100 },
        dataType: "json",
        async: false,
        success: function (data) {
            $("#ParentId").html("");
            $.each(data.rows, function (i, n) {
                $("#ParentId").append("<option value='" + n.Id + "'>" + n.Name + "</option>");
            });
        }
    });
}
function submitForm() {
    if (!$('#form1').formValid()) {
        return false;
    }
    $.submitForm({
        url: "/Menu/Save?id=" + keyValue,
        param: $("#form1").formSerialize(),
        success: function () {
            $.currentWindow().$("#gridList").trigger("reloadGrid");
        }
    })
}