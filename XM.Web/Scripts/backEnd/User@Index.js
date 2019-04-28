$(function () {
    gridList();
})
function gridList() {
    var $gridList = $("#gridList");
    $gridList.dataGrid({
        url: "/User/GetGridJson",
        height: $(window).height() - 128,
        colModel: [
            { label: '主键', name: 'user_id', hidden: true },
            { label: '账户', name: 'user_AN', width: 80, align: 'left' },
            { label: '手机', name: 'user_mp', width: 100, align: 'left' },
            { label: '邮箱', name: 'user_email', width: 140, align: 'left' }, 
            { label: '创建人', name: 'user_CBY', width: 80, align: 'left' },
            {
                label: '角色', name: 'F_RoleId', width: 80, align: 'left',
                formatter: function (cellvalue, options, rowObject) {
                    return top.clients.role[cellvalue] == null ? "" : top.clients.role[cellvalue].fullname;
                }
            },
            { label: '创建时间', name: 'user_CDT', width: 140, align: 'left' },
            {
                label: "允许登录", name: "status_id", width: 60, align: "left",
                formatter: function (cellvalue, options, rowObject) {
                    if (cellvalue == 1) {
                        return '<span class=\"label label-success\">正常</span>';
                    } else if (cellvalue == 0) {
                        return '<span class=\"label label-default\">禁用</span>';
                    }
                }
            }
        ],
        pager: "#gridPager",
        sortname: 'F_DepartmentId asc,F_CreatorTime desc',
        viewrecords: true
    });
    $("#btn_search").click(function () {
        $gridList.jqGrid('setGridParam', {
            postData: {
                keyword: $("#txt_keyword").val()
            },
        }).trigger('reloadGrid');
    });
}
function btn_add() {
    $.modalOpen({
        id: "Form",
        title: "新增用户",
        url: "/User/Form",
        width: "430px",
        height: "350px",
        callBack: function (iframeId) {
            top.frames[iframeId].submitForm();
        }
    });
}
function btn_edit() {
    // 主键
    var keyValue = $("#gridList").jqGridRowValue().user_id;
    $.modalOpen({
        id: "Form",
        title: "修改用户",
        url: "/User/Form?keyValue=" + keyValue,
        width: "430px",
        height: "350px",
        callBack: function (iframeId) {
            top.frames[iframeId].submitForm();
        }
    });
}
function btn_delete() {
    $.deleteForm({
        url: "/User/DeleteForm",
        param: { keyValue: $("#gridList").jqGridRowValue().user_id },
        success: function () {
            $.currentWindow().$("#gridList").trigger("reloadGrid");
        }
    })
}
function btn_details() {
    var keyValue = $("#gridList").jqGridRowValue().F_Id;
    $.modalOpen({
        id: "Details",
        title: "查看用户",
        url: "/User/Details?keyValue=" + keyValue,
        width: "430px",
        height: "410px",
        btn: null,
    });
}
function btn_revisepassword() {

    var keyValue = $("#gridList").jqGridRowValue().F_Id;
    $.modalConfirm("注：您确定要对该账户进行重置密码吗？", function (r) {
        if (r) {
            $.submitForm({
                url: "/SystemManage/User/DisabledAccount",
                param: { keyValue: keyValue },
                success: function () {
                    $.currentWindow().$("#gridList").trigger("reloadGrid");
                }
            })
        }
    });
}
function btn_disabled() {
    var keyValue = $("#gridList").jqGridRowValue().F_Id;
    $.modalConfirm("注：您确定要【禁用】该项账户吗？", function (r) {
        if (r) {
            $.submitForm({
                url: "/SystemManage/User/DisabledAccount",
                param: { keyValue: keyValue },
                success: function () {
                    $.currentWindow().$("#gridList").trigger("reloadGrid");
                }
            })
        }
    });
}
function btn_enabled() {
    var keyValue = $("#gridList").jqGridRowValue().F_Id;
    $.modalConfirm("注：您确定要【启用】该项账户吗？", function (r) {
        if (r) {
            $.submitForm({
                url: "/SystemManage/User/EnabledAccount",
                param: { keyValue: keyValue },
                success: function () {
                    $.currentWindow().$("#gridList").trigger("reloadGrid");
                }
            })
        }
    });
}