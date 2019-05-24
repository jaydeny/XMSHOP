$(function () {
    gridList();
})
function gridList() {
    var $gridList = $("#gridList");
    $gridList.dataGrid({
        url: "/Vip/GetAllUserInfo",
        height: $(window).height() - 178,
        rowNum: 20,
        rowList: [10, 20, 30, 40, 50],
        sortorder: "desc",
        pager: "#gridPager",
        colModel: [
            { label: '主键', name: 'VipID', hidden: true },
            { label: '账户', name: 'VipAccountName', width: 80, align: 'left' },
            { label: '手机', name: 'VipMobliePhone', width: 100, align: 'left' },
            { label: '邮箱', name: 'VipEmail', width: 140, align: 'left' },
            { label: '代理编号', name: 'AgentID', width: 80, align: 'left' },
            {
                label: '创建时间', name: 'CreateTime', width: 140, align: 'left',
                formatter: "date", formatoptions: { srcformat: 'Y-m-d', newformat: 'Y-m-d' }
            },
            {
                label: "允许登录", name: "StatusID", width: 60, align: "left",
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
                VipAccountName: $("#txt_keyword").val()
            }
        }).trigger('reloadGrid');
    });
}
function btn_add() {
    $.modalOpen({
        id: "Form",
        title: "新增",
        url: "/Vip/Form",
        width: "430px",
        height: "350px",
        callBack: function (iframeId) {
            top.frames[iframeId].submitForm();
        }
    });
}
function btn_edit() {
    // 主键
    var keyValue = $("#gridList").jqGridRowValue().VipID;
    $.modalOpen({
        id: "Form",
        title: "修改",
        url: "/Vip/Form?keyValue=" + keyValue,
        width: "430px",
        height: "350px",
        callBack: function (iframeId) {
            top.frames[iframeId].submitForm();
        }
    });
}
function btn_delete() {
    $.deleteForm({
        url: "/Vip/DelUserByIDs",
        param: { id: $("#gridList").jqGridRowValue().VipID },
        success: function () {
            //$.currentWindow().$("#gridList").trigger("reloadGrid");
            $("#gridList").jqGrid().setGridParam({ datatype: 'json' }).trigger('reloadGrid');
        }
    })
}
function btn_details() {
    var keyValue = $("#gridList").jqGridRowValue().F_Id;
    $.modalOpen({
        id: "Details",
        title: "查看",
        url: "/Vip/Details?keyValue=" + keyValue,
        width: "430px",
        height: "410px",
        btn: null,
    });
}
function btn_revisepassword() {
    var keyValue = $("#gridList").jqGridRowValue().VipID;
    var Account = $("#gridList").jqGridRowValue().F_Account;
    var RealName = $("#gridList").jqGridRowValue().F_RealName;
    $.modalOpen({
        id: "RevisePassword",
        title: '重置密码',
        url: '/SystemManage/Vip/RevisePassword?keyValue=' + keyValue + "&account=" + escape(Account) + '&realName=' + escape(RealName),
        width: "450px",
        height: "260px",
        callBack: function (iframeId) {
            top.frames[iframeId].submitForm();
        }
    });
}
function btn_disabled() {
    var keyValue = $("#gridList").jqGridRowValue();
    keyValue.StatusID = 2;
    keyValue.id = keyValue.VipID;
    $.modalConfirm("注：您确定要【禁用】该项吗？", function (r) {
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
    keyValue.id = keyValue.VipID;
    $.modalConfirm("注：您确定要【启用】该项吗？", function (r) {
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