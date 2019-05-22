var keyValue = $.request("keyValue");
var starttime = $.request("starttime");
var endtime = $.request("endtime");
$(function () {
    Record();
})

function Record() {
    var $gridList = $("#qryDayList");
    $gridList.dataGrid({
        url: "/GameRecord/Record?ID=" + keyValue + "&starttime=" + starttime + "&endtime=" + endtime,
        height: $(window).height() - 178,
        rowNum: 20,
        rowList: [10, 20, 30, 40, 50],
        pager: "#gridPager",
        colModel: [
            { label: '游戏账号', name: 'AccountName', width: 80, align: 'center' },
            { label: '游戏名称', name: 'Name', width: 80, align: 'center' },
            { label: '游戏积分', name: 'Integral', width: 80, align: 'left' },
            { label: '游戏时间', name: 'Time', width: 80, align: 'center' }
        ]
    });
    $("#btn_search_vip").click(function () {
        $gridList.jqGrid('setGridParam', {
            postData: {
                starttime: starttime.getFullYear() + "-" + (starttime.getMonth() + 1) + "-" + starttime.getDate(),
                endtime: endtime.getFullYear() + "-" + (endtime.getMonth() + 1) + "-" + endtime.getDate(),
                vipAccount: $("#txt_search_vip").val()
            }
        }).trigger('reloadGrid');
    });
    $("#btn_search_agent").click(function () {
        $gridList.jqGrid('setGridParam', {
            postData: {
                starttime: starttime.getFullYear() + "-" + (starttime.getMonth() + 1) + "-" + starttime.getDate(),
                endtime: endtime.getFullYear() + "-" + (endtime.getMonth() + 1) + "-" + endtime.getDate(),
                agentAccount: $("#txt_search_agent").val()
            }
        }).trigger('reloadGrid');
    });
    $("#btn_back").click(function () {
        window.location.href = "/GameRecord/Index";
    });
}