$(function () {
    gridList();
})
function gridList() {
    var $gridList = $("#gridList");
    $gridList.dataGrid({
        url: "GetGridJson",
        height: $(window).height() - 128,
        colModel: [
            { label: 'id', name: 'id', hidden: true },
            { label: "名称", name: 'name', width: 80, align: 'left' },
            { label: "标识", name: 'code', width: 80, align: 'left' },
            { label: "排序", name: 'sort', width: 80, align: 'left' },
        ]
    });
    $("input[name='tag']").click(function () {
        $gridList.jqGrid('setGridParam', {
            postData: { tag: $("input[name='tag']:checked").val() },
        }).trigger('reloadGrid');
    });
}
function btn_add() {
    $.modalOpen({
        id: "Edit",
        title: "添加",
        url: "/Dic/Form?tag=" + $("input[name='tag']:checked").val(),
        width: "400px",
        height: "300px",
        callBack: function (iframeId) {
            top.frames[iframeId].submitForm();
        }
    });
}

function btn_edit() {
    var keyValue = $("#gridList").jqGridRowValue().id;
    $.modalOpen({
        id: "Edit",
        title: "修改",
        url: "/Dic/Form?id=" + keyValue + "&tag=" + $("input[name='tag']:checked").val(),
        width: "400px",
        height: "300px",
        callBack: function (iframeId) {
            top.frames[iframeId].submitForm();
        }
    });
}

function btn_delete() {
    $.deleteForm({
        url: "Delete",
        param: { ID: $("#gridList").jqGridRowValue().id },
        success: function () {
            $.currentWindow().$("#gridList").trigger("reloadGrid");
        }
    })
}
