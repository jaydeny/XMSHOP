var keyValue = $.request("keyValue");
$(function () {
    initControl();
    if (!!keyValue) {
        $.ajax({
            url: "/Role/GetFormJson",
            data: { id: keyValue },
            dataType: "json",
            async: false,
            success: function (data) {
                $("#form1").formSerialize(data);
            }
        });
    }
})
function initControl() {


    // 按钮事件
    $('#wizard').wizard().on('change', function (e, data) {
        var $finish = $("#btn_finish");
        var $next = $("#btn_next");
        if (data.direction == "next") {
            switch (data.step) {
                case 1:
                    if (!$('#form1').formValid()) {
                        return false;
                    }
                    $finish.show();
                    $next.hide();
                    break;
                default:
                    break;
            }
        } else {
            $finish.hide();
            $next.show();
        }
    });
    $("#permissionTree").on("")

    //$.get("/Role/GetAllMenu", function (data) {
    //    console.log(data);
    //});
    // 渲染单选
    $("#permissionTree").treeview({
        height: 240,
        showcheck: true,
        url: "/Role/GetAllMenu"
    });
    
}
function submitForm() {
    // 创建对象
    var RoleMemu = function (mId, add,del, other) {
        return {
            m_id: mId,
            add: add != true ? false : true,
            delete: del != true ? false : true,
            other: other != true ? false : true
        }
    }
    var postData = $("#form1").formSerialize();
    postData["permissionIds"] = String($("#permissionTree").getCheckedNodes());
    console.log($("#permissionTree").getCheckedNodes());
    var list = $("#permissionTree").getCheckedNodes();
    for (var i = 0; list.length; i++) {
        console.log(list[i]);
    }
    //$.submitForm({
    //    url: "/Role/SubmitForm?Id=" + keyValue,
    //    param: postData,
    //    success: function () {
    //        $.currentWindow().$("#gridList").trigger("reloadGrid");
    //    }
    //});
}