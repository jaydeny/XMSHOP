$(function () {
    gridList();
})
function gridList() {
    var $gridList = $("#gridList");
    $gridList.dataGrid({
        url: "/Agent/GetAllUserInfo",
        height: $(window).height() - 178,
        rowNum: 20,
        rowList: [10, 20, 30, 40, 50],
        sortorder: "desc",
        pager: "#gridPager",
        colModel: [
            { label: '主键', name: 'AgentID', hidden: true },
            { label: '账户', name: 'AgentAccountName', width: 80, align: 'left' },
            { label: '手机', name: 'MobliePhone', width: 100, align: 'left' },
            { label: '邮箱', name: 'Email', width: 140, align: 'left' },
            { label: '创建人', name: 'CreateBy', width: 80, align: 'center' },
            { label: '创建时间', name: 'CreateTime', width: 140, align: 'left' },
            {
                label: "状态", name: "StatusID", width: 60, align: "left",
                formatter: function (cellvalue, options, rowObject) {
                    if (cellvalue == 1) {
                        return '<span class=\"label label-success\">正常</span>';
                    } else if (cellvalue == 2) {
                        return '<span class=\"label label-default\">禁用</span>';
                    }
                }
            }
        ]
    });
    $("#btn_search").click(function () {
        $gridList.jqGrid('setGridParam', {
            postData: {
                agent_AN: $("#txt_keyword").val()
            },
        }).trigger('reloadGrid');
    });
}
function btn_add() {
    $.modalOpen({
        id: "Form",
        title: "新增",
        url: "/Agent/Form",
        width: "430px",
        height: "310px",
        callBack: function (iframeId) {
            top.frames[iframeId].submitForm();
        }
    });
}
function btn_edit() {
    var keyValue = $("#gridList").jqGridRowValue().AgentID;
    $.modalOpen({
        id: "Form",
        title: "修改",
        url: "/Agent/Form?keyValue=" + keyValue,
        width: "430px",
        height: "310px",
        callBack: function (iframeId) {
            top.frames[iframeId].submitForm();
        }
    });
}
function btn_delete() {
    $.deleteForm({
        url: "/Agent/DelUserByIDs",
        param: { id: $("#gridList").jqGridRowValue().AgentID },
        success: function () {
            //$.currentWindow().$("#gridList").trigger("reloadGrid");
            $("#gridList").jqGrid().setGridParam({ datatype: 'json' }).trigger('reloadGrid');
        }
    })
}
function btn_details() {
    var keyValue = $("#gridList").jqGridRowValue().AgentID;
    $.modalOpen({
        id: "Details",
        title: "查看",
        url: "/Agent/Details?keyValue=" + keyValue,
        width: "430px",
        height: "410px",
        btn: null,
    });
}
function btn_revisepassword() {
    var keyValue = $("#gridList").jqGridRowValue().F_Id;
    var Account = $("#gridList").jqGridRowValue().F_Account;
    var RealName = $("#gridList").jqGridRowValue().F_RealName;
    $.modalOpen({
        id: "RevisePassword",
        title: '重置密码',
        url: '/SystemManage/User/RevisePassword?keyValue=' + keyValue + "&account=" + escape(Account) + '&realName=' + escape(RealName),
        width: "450px",
        height: "260px",
        callBack: function (iframeId) {
            top.frames[iframeId].submitForm();
        }
    });
}
function btn_disabled() {
    var keyValue = $("#gridList").jqGridRowValue();
    keyValue.id = keyValue.AgentID;
    keyValue.StatusID = 2;
    $.modalConfirm("注：您确定要【禁用】该项账户吗？", function (r) {
        if (r) {
            $.submitForm({
                url: "/Agent/Save",
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
    keyValue.id = keyValue.AgentID;
    keyValue.StatusID = 1;
    $.modalConfirm("注：您确定要【启用】该项账户吗？", function (r) {
        if (r) {
            $.submitForm({
                url: "/Agent/Save",
                param: keyValue,
                success: function () {
                    //$.currentWindow().$("#gridList").trigger("reloadGrid");
                    $("#gridList").jqGrid().setGridParam({ datatype: 'json' }).trigger('reloadGrid');
                }
            })
        }
    });
}