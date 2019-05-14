$(function () {
    gridList();
})
function gridList() {
    var $gridList = $("#gridList");
    $gridList.dataGrid({
        url: "/Revenue/QryDayRechargeTotal",
        height: $(window).height() - 178,
        rowNum: 20,
        rowList: [10, 20, 30, 40, 50],
        sortorder: "desc",
        pager: "#gridPager",
        colModel: [
            { label: '日期', name: 'date', width: 80, align: 'left' },
            { label: '营收(/元)	', name: 'total', width: 100, align: 'left' }
        ]
    });
    $("#btn_search").click(function () {
        $gridList.jqGrid('setGridParam', {
            postData: {
                keyword: $("#txt_keyword").val()
            }
        }).trigger('reloadGrid');
    });
}
