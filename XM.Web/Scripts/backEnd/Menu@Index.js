$(function () {
    gridList();
})
function gridList() {
    var $gridList = $("#gridList");
    $gridList.dataGrid({
        url: "/Menu/GetAllMenu",
        height: $(window).height() - 178,
        colModel: [
            { label: '主键', name: 'Id', hidden: true },
            { label: '导航菜单名称', name: 'Name', width: 80, align: 'center' },
            {
                label: '父级节点', name: 'ParentId', hidden: true
            },
            { label: '菜单标识码', name: 'Code', width: 140, align: 'center' },
            { label: '控制器', name: 'Controller', width: 80, align: 'center' },
            { label: '动作', name: 'Action', width: 120, align: 'center' },
            { label: '排序', name: 'SortValue', width: 100, align: 'center' },
            {
                label: "状态", name: "State", width: 60, align: "left",
                formatter: function (cellvalue, options, rowObject) {
                    if (cellvalue == 1) {
                        return '<span class=\"label label-success\">正常</span>';
                    } else if (cellvalue == 2) {
                        return '<span class=\"label label-default\">禁用</span>';
                    }
                }
            }
        ],
        rowNum: 20,
        rowList: [10, 20, 30, 40, 50],
        sortorder: "desc",
        pager: "#gridPager"
    });
    $("#btn_search").click(function () {
        $gridList.jqGrid('setGridParam', {
            postData: {
                keyword: $("#txt_keyword").val()
            }
        }).trigger('reloadGrid');
    });
}
function btn_add() {
    $.modalOpen({
        id: "Form",
        title: "新增菜单",
        url: "/Menu/Form",
        width: "430px",
        height: "420px",
        callBack: function (iframeId) {
            top.frames[iframeId].submitForm();
        }
    });
}
function btn_edit() {
    // 主键
    var keyValue = $("#gridList").jqGridRowValue().Id;
    $.modalOpen({
        id: "Form",
        title: "修改菜单",
        url: "/Menu/Form?keyValue=" + keyValue,
        width: "430px",
        height: "420px",
        callBack: function (iframeId) {
            top.frames[iframeId].submitForm();
        }
    });
}
function btn_delete() {
    $.deleteForm({
        url: "/Menu/DelMenuByIDs",
        param: { id: $("#gridList").jqGridRowValue().Id },
        success: function () {
            //$.currentWindow().$("#gridList").trigger("reloadGrid");
            $("#gridList").jqGrid().setGridParam({ datatype: 'json' }).trigger('reloadGrid');
        }
    })
}
function btn_disabled() {
    var keyValue = $("#gridList").jqGridRowValue();
    keyValue.State = 2;
    $.modalConfirm("注：您确定要【禁用】该项菜单吗？", function (r) {
        if (r) {
            $.submitForm({
                url: "/Menu/Save",
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
    keyValue.State = 1;
    $.modalConfirm("注：您确定要【启用】该项菜单吗？", function (r) {
        if (r) {
            $.submitForm({
                url: "/Menu/Save",
                param: keyValue,
                success: function () {
                    //$.currentWindow().$("#gridList").trigger("reloadGrid");
                    $("#gridList").jqGrid().setGridParam({ datatype: 'json' }).trigger('reloadGrid');
                }
            })
        }
    });
}
