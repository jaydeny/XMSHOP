$(function () {
    gridList();
})
function gridList() {
    var $gridList = $("#gridList");
    $gridList.dataGrid({
        url: "/Role/GetALLRoleInfo",
        height: $(window).height() - 178,
        rowNum: 20,
        rowList: [10, 20, 30, 40, 50],
        sortorder: "desc",
        pager: "#gridPager",
        colModel: [
            { label: '主键', name: 'Id', hidden: true },
            { label: '权限名', name: 'Name', width: 100, align: 'left' },
            { label: '标识码', name: 'Code', width: 100, align: 'left' },
            {
                label: "状态", name: "State", width: 130, align: "left",
                formatter: function (cellvalue, options, rowObject) {
                    if (cellvalue == 1) {
                        return '<span class=\"label label-success\">正常</span>';
                    } else if (cellvalue == 2) {
                        return '<span class=\"label label-default\">冻结</span>';
                    }
                }
            }
        ]
    });
    $("#btn_search").click(function () {
        $gridList.jqGrid('setGridParam', {
            postData: {
                name: $("#txt_keyword").val()
            }
        }).trigger('reloadGrid');
    });
}
function btn_add() {
    $.modalOpen({
        id: "Form",
        title: "新增用户",
        url: "/Role/Form",
        width: "550px",
        height: "370px",
        btn: null
    });
}
function btn_edit() {
    // 主键
    var keyValue = $("#gridList").jqGridRowValue().Id;
    $.modalOpen({
        id: "Form",
        title: "修改用户",
        url: "/Role/Form?keyValue=" + keyValue,
        width: "550px",
        height: "370px",
        btn: null
    });
}
function btn_delete() {
    $.deleteForm({
        url: "/Role/DelRoleByIds",
        param: { id: $("#gridList").jqGridRowValue().Id },
        success: function () {
            //$.currentWindow().$("#gridList").trigger("reloadGrid");
            $("#gridList").jqGrid().setGridParam({ datatype: 'json' }).trigger('reloadGrid');
        }
    })
}

function btn_disabled() {
    var keyValue = $("#gridList").jqGridRowValue();
    keyValue.StatusID = 0;
    keyValue.id = keyValue.Id;
    $.modalConfirm("注：您确定要【禁用】该项权限吗？", function (r) {
        if (r) {
            $.submitForm({
                url: "/Vip/Save",
                param: keyValue,
                success: function () {
                    //$.currentWindow().$("#gridList").trigger("reloadGrid");
                    $("#gridList").jqGrid().setGridParam({ datatype: 'json' }).trigger('reloadGrid');
                }
            })
        }
    });
}
function btn_enabled() {
    var keyValue = $("#gridList").jqGridRowValue();
    keyValue.StatusID = 1;
    keyValue.id = keyValue.Id;
    $.modalConfirm("注：您确定要【启用】该项权限吗？", function (r) {
        if (r) {
            $.submitForm({
                url: "/Vip/Save",
                param: keyValue,
                success: function () {
                    //$.currentWindow().$("#gridList").trigger("reloadGrid");
                    $("#gridList").jqGrid().setGridParam({ datatype: 'json' }).trigger('reloadGrid');
                }
            })
        }
    });
}