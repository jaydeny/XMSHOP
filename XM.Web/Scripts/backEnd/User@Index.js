$(function () {
    gridList();
})
function gridList() {
    var $gridList = $("#gridList");
    $gridList.dataGrid({
        url: "/User/GetAllUserInfo",
        height: $(window).height() - 178,
        colModel: [
            { label: '主键', name: 'id', hidden: true },
            { label: '账户', name: 'UserAccountName', width: 80, align: 'center' },
            { label: '手机', name: 'UserMobliePhone', width: 100, align: 'center' },
            { label: '邮箱', name: 'UserEmail', width: 140, align: 'center' }, 
            { label: '创建人', name: 'UserCreateBy', width: 80, align: 'center' },
            { label: '创建时间', name: 'UserCreateDate', width: 160, align: 'center' },
            {
                label: "允许登录", name: "StatusID", width: 60, align: "left",
                formatter: function (cellvalue, options, rowObject) {
                    if (cellvalue == 1) {
                        return '<span class=\"label label-success\">正常</span>';
                    } else if (cellvalue == 0) {
                        return '<span class=\"label label-default\">禁用</span>';
                    }
                }
            }
        ],
        rowNum: 10,
        rowList: [10, 20, 30],
        sortorder: "desc",
        pager: "#gridPager"
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
    var keyValue = $("#gridList").jqGridRowValue().id;
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
        url: "/User/DelUserByIDs",
        param: { id: $("#gridList").jqGridRowValue().id },
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
                    $.currentWindow().$("#gridList").trigger("gridList");
                    
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