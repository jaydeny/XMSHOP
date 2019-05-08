$(function () {
    gridList();
})
function gridList() {
    var $gridList = $("#gridList");
    $gridList.dataGrid({
        url: "/Goods/GetAllGoodsInfo",
        height: $(window).height() - 178,
        rowNum: 20,
        rowList: [10, 20, 30, 40, 50],
        sortorder: "desc",
        pager: "#gridPager",
        colModel: [
            { label: '主键', name: 'GoodsID', hidden: true },
            { label: '类型', name: 'GoodsType', width: 80, align: 'left' },
            { label: '商品', name: 'GoodsName', width: 80, align: 'left' },
            { label: '单价', name: 'GoodsPrice', width: 100, align: 'left' },
            { label: '图片', name: 'GoodsPicture', width: 80, align: 'left' },
            { label: '创建人', name: 'GoodsCreateBy', width: 80, align: 'left' },
            { label: '创建时间', name: 'GoodsCreateTime', width: 140, align: 'left' }
        ],
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
        url: "/Goods/Form",
        width: "430px",
        height: "350px",
        callBack: function (iframeId) {
            top.frames[iframeId].submitForm();
        }
    });
}
function btn_edit() {
    // 主键
    var keyValue = $("#gridList").jqGridRowValue().GoodsID;
    $.modalOpen({
        id: "Form",
        title: "修改用户",
        url: "/Goods/Form?keyValue=" + keyValue,
        width: "430px",
        height: "350px",
        callBack: function (iframeId) {
            top.frames[iframeId].submitForm();
        }
    });
}
function btn_delete() {
    $.deleteForm({
        url: "/Goods/DelGoodsByIDs",
        param: { id: $("#gridList").jqGridRowValue().GoodsID },
        success: function () {
            //$.currentWindow().$("#gridList").trigger("reloadGrid");
            $("#gridList").jqGrid().setGridParam({ datatype: 'json' }).trigger('reloadGrid');
        }
    })
}
function btn_details() {
    var keyValue = $("#gridList").jqGridRowValue().GoodsID;
    $.modalOpen({
        id: "Details",
        title: "查看用户",
        url: "/Goods/Details?keyValue=" + keyValue,
        width: "430px",
        height: "410px",
        btn: null,
    });
}
function btn_disabled() {
    var keyValue = $("#gridList").jqGridRowValue().F_Id;
    $.modalConfirm("注：您确定要【禁用】该项账户吗？", function (r) {
        if (r) {
            $.submitForm({
                url: "/Goods/Save",
                param: { keyValue: keyValue },
                success: function () {
                    //$.currentWindow().$("#gridList").trigger("reloadGrid");
                    $("#gridList").jqGrid().setGridParam({ datatype: 'json' }).trigger('reloadGrid');
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
                url: "/Goods/Save",
                param: { keyValue: keyValue },
                success: function () {
                    //$.currentWindow().$("#gridList").trigger("reloadGrid");
                    $("#gridList").jqGrid().setGridParam({ datatype: 'json' }).trigger('reloadGrid');
                }
            })
        }
    });
}