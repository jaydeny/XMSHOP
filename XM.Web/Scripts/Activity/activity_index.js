//弹出模态框
function btn_add() {
    $.modalOpen({
        id: "Form",
        title: "新增",
        url: "/Activity/ActivityAdd",
        width: "700px",
        height: "600px",
        callBack: function (iframeId) {
            //console.log(iframeId)
            top.frames[iframeId].submitForm();
        }
    });
};
//查看活动详情
function btn_info() {
    let typeNum = $("#gridList").jqGridRowValue().Ac_type;
    let id = $("#gridList").jqGridRowValue().id;

    $.modalOpen({
        id: "Form",
        title: "活动详情",
        url: "/Activity/GetOrderForm?typeNum=" + typeNum + "&id=" + id,
        width: "430px",
        height: "400px"
    });
}

$(function () {
    gridList();
})

function gridList() {
    var $gridList = $("#gridList");
    $gridList.dataGrid({
        url: "/Activity/getAllActtvity",
        height: $(window).height() - 178,
        colModel: [
            { label: '活动ID', name: 'id', hidden: true },
            { label: '活动类型', name: 'Ac_type', hidden: true },
            { label: '活动状态', name: 'status_id', hidden: true },
            { label: '标题', name: 'Title', width: 100, align: 'center' },
            { label: '内容', name: 'Content', width: 240, align: 'center' },
            {
                label: '创建时间', name: 'CreateDate', width: 160, align: 'center',
                formatter: "date", formatoptions: { srcformat: 'Y-m-d', newformat: 'Y-m-d' }
            },
            {
                label: '开始时间', name: 'StartDate', width: 160, align: 'center',
                formatter: "date", formatoptions: { srcformat: 'Y-m-d', newformat: 'Y-m-d' }
            },
            {
                label: '结束时间', name: 'EndDate', width: 160, align: 'center',
                formatter: "date", formatoptions: { srcformat: 'Y-m-d', newformat: 'Y-m-d' } },
            { label: '发布人', name: 'Publisher', width: 80, align: 'center' }
            //,
            //{
            //    label: '操作', name: '_id', width: 160, align: 'center',
            //    formatter: function (cellvalue, options, rowObject) {
            //        //console.log(cellvalue)
            //        //console.log(options)
            //        //console.log(rowObject)
            //        return '<button class="btn-group" data-val="' + rowObject + '" onclick = "btn_edit()">编辑</button >';
            //    }
            //}
        ],
        rowNum: 20,
        rowList: [10, 20, 30, 40, 50],
        sortorder: "desc",
        pager: "#gridPager"
    });
    //$gridList.click(function () {
    //    let typeNum = $("#gridList").jqGridRowValue().Ac_type;
    //    let id = $("#gridList").jqGridRowValue().id;
       
    //    $.modalOpen({
    //        id: "Form",
    //        title: "订单详情",
    //        url: "/Activity/GetOrderForm?typeNum=" + typeNum + "&id=" + id,
    //        width: "430px",
    //        height: "400px"
    //    });
    //});
    $("#btn_search_title").click(function () {
       
        $gridList.jqGrid('setGridParam', {
            postData: {
                Title: $("#txt_search_receiver").val(),
                Person: null
            },
        }).trigger('reloadGrid');
    });
};
function btn_edit() {
    //console.log(event)
    let typeNum = $("#gridList").jqGridRowValue().Ac_type;
    let id = $("#gridList").jqGridRowValue().id;

    $.modalOpen({
        id: "Form",
        title: "修改",
        url: "/Activity/EditOrderForm?typeNum=" + typeNum + "&id=" + id,
        width: "430px",
        height: "400px",
        callBack: function (iframeId) {
            top.frames[iframeId].submitForm();
        }
    });
};


