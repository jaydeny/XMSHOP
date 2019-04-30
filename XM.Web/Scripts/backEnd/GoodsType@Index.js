$(function () {
    gridList();
})
function gridList() {
    var $gridList = $("#gridList");
    $gridList.dataGrid({
        url: "/GoodsType/GetGridJson",
        height: $(window).height() - 128,
        colModel: [
            { label: '主键', name: 'TypeID', hidden: true },
            { label: '类型名称', name: 'TypeName', width: 120, align: 'left' },
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
        title: "新增商品类型",
        url: "/GoodsType/Form",
        width: "430px",
        height: "200px",
        callBack: function (iframeId) {
            top.frames[iframeId].submitForm();
        }
    });
}
function btn_edit() {
    // 主键
    var keyValue = $("#gridList").jqGridRowValue().TypeID;
    $.modalOpen({
        id: "Form",
        title: "修改商品类型",
        url: "/GoodsType/Form?keyValue=" + keyValue,
        width: "430px",
        height: "200px",
        callBack: function (iframeId) {
            top.frames[iframeId].submitForm();
        }
    });
}
function btn_delete() {
    $.deleteForm({
        url: "/GoodsType/DeleteForm",
        param: { keyValue: $("#gridList").jqGridRowValue().TypeID },
        success: function () {
            $.currentWindow().$("#gridList").trigger("reloadGrid");
        }
    })
}


