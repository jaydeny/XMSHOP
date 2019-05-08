
var keyValue = $.request("keyValue");
$(function () {
    initControl();
    $.get("/Type/GetAllTypeInfo", function (data) {
        $("#GoodsType").html("");
        $.each(data.rows, function (i, n) {
            $("#GoodsType").append("<option value='" + n.TypeID + "'>" + n.TypeName + "</option>");
        });
    }, "json").done(function () {
            if (!!keyValue) { //判断是否有值
                $.ajax({
                    url: "/Goods/GetFormJson",
                    data: { id: keyValue },
                    dataType: "json",
                    async: false,
                    success: function (data) {
                        $("#form1").formSerialize(data);
                    }
                });
            }
        })
});
function initControl() {
}
function submitForm() {
    if (!$('#form1').formValid()) {
        return false;
    }
    $.submitForm({
        url: "/Goods/Save?id=" + keyValue,
        param: $("#form1").formSerialize(),
        success: function () {
            $.currentWindow().$("#gridList").trigger("reloadGrid");
        }
    })
}