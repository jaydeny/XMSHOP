var total = 0;
$(function () {
    gridList();
    $("")
})

function gridList() {
    var now = new Date();
    var startday = "01";
    var endday = ("0" + now.getDate()).slice(-2);
    var month = ("0" + (now.getMonth() + 1)).slice(-2);
    var starttime = now.getFullYear() + "-" + (month) + "-" + (startday);
    var endtime = now.getFullYear() + "-" + (month) + "-" + (endday);
    $("#date_start").val(starttime);
    $("#date_end").val(endtime);
    var $gridList = $("#gridList");
    $gridList.dataGrid({
        url: "/GameRecord/GetRecordCollect",
        height: $(window).height() - 178,
        colModel: [
            { label: '游戏账号', name: 'ID', width: 80, align: 'center' },
            { label: '游戏名称', name: 'Name', width: 80, align: 'left' },
            { label: '游戏积分', name: 'Integral', width: 80, align: 'left'}
        ],
        gridComplete: function (cellValue, options, rowObject) {
            var total = $("#gridList").getCol('Integral', false, 'sum');
            $("#gridList").footerData('set', { "ID": "合计：", "Name": total }, false);
        },
        footerrow: true,
        rowNum: 20,
        rowList: [10, 20, 30, 40, 50],
        sortorder: "desc",
        viewrecords: true
    });
    $("#btn_search").click(function () {
        start = new Date($("#date_start").val());
        end = new Date($("#date_end").val());
        var time = (end - start) / (1000 * 60 * 60 * 24);
        if (time > 90) {
            alert("当前用户可以查阅三个月内的记录")
        }
        else {
            $gridList.jqGrid('setGridParam', {
                postData: {
                    starttime: start.getFullYear() + "-" + (start.getMonth() + 1) + "-" + start.getDate(),
                    endtime: end.getFullYear() + "-" + (end.getMonth() + 1) + "-" + end.getDate()
                }
            }).trigger('reloadGrid');
        }
    });
    $gridList.click(function () {
        var keyValue = $("#gridList").jqGridRowValue().ID;
        var starttime = $("#date_start").val();
        var endtime = $("#date_end").val();
        window.location.href = "/GameRecord/GetRecord?keyValue=" + keyValue + "&starttime=" + starttime + "&endtime=" + endtime;
    });

}



