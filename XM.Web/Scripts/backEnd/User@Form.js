
var keyValue = $.request("keyValue");
$(function () {
    initControl();
    if (!!keyValue) { //判断是否有值
        $.ajax({
            url: "/User/GetFormJson",
            data: { id: keyValue },
            dataType: "json",
            async: false,
            success: function (data) {
                $("#form1").formSerialize(data);
                $("#UserAccountName").attr('disabled', 'disabled');
            }
        });
    }
});

function initControl() {
    // 获取角色
    $.get("/Role/GetALLRoleInfo", function (data) {
        $("#RoleID").html("");
        $.each(data.rows, function (i, n) {
            $("#RoleID").append("<option value='"+n.Id+"'>" + n.Name+"</option>");
        });
    },"json")
}
function submitForm() {
    if (!$('#form1').formValid()) {
        return false;
    }
    $.submitForm({
        url: "/User/Save?id=" + keyValue,
        param: $("#form1").formSerialize(),
        success: function () {
            $.currentWindow().$("#gridList").trigger("reloadGrid");
        }
    })
}