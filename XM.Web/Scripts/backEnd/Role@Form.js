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
    // 渲染单选
    $("#permissionTree").treeview({
        height: 240,
        showcheck: true,
        url: "/Role/GetAllMenu",
        param: { roleId: keyValue }
    });
    
}

// 创建对象
var RoleMemu = function (mId, open, obj) {
    var entity = null;
    if (obj != null) {
        entity = obj;
    }
    else {
        var add = false, update = false, del = false, other = false;
        entity = {
            m_id: mId,
            add: add,
            update: update,
            delete: del,
            other: other
        }
    }
        if (open == 0) {
            entity.add = true;
        }
        else if (open == 1) {
            entity.update = true
        }
        else if (open == 2) {
            entity.delete = true
        }
        else if (open == 3) {
            entity.other = true;
        }
    return entity;
}

function submitForm() {
    // 调试
    //debugger
    var postData = $("#form1").formSerialize();
    // 获取选单
    var list = $("#permissionTree").getCheckedNodes();
    // 选单集
    var roleObj = [];
    for (var i = 0; i<list.length; i++) {
        // 获取字符
        var pattSeparator = new RegExp(/-/);
        var pattId = new RegExp("\\d+");
        if (pattSeparator.test(list[i])) {
            // 重置
            boo = true;
            // 获取选单编号
            var roleId = pattId.exec(list[i])[0];
            // 选单操作
            var operation = list[i].charAt(list[i].length - 1);
            var boo = true;
            for (var j = 0; j < roleObj.length; j++) {
                if (roleObj[j].m_id == roleId) {
                    RoleMemu(roleId, operation, roleObj[j]);
                    boo = false;
                    break;
                }
            }
            if (boo) {
                roleObj.push(RoleMemu(roleId, operation, null));
            }
        }
    }
    if (!roleObj.length > 0) {
        $.modalMsg("请选中权限", false);
        return false;
    }
    postData.roleMemus = roleObj;
    postData.Id = keyValue;
    $.submitForm({
        url: "/Role/Save",
        param: postData,
        success: function () {

            $.currentWindow().$("#gridList").trigger("reloadGrid");
        }
    });
}