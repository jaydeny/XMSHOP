$(function () {
    gridList();
})

function gridList() {
    var $gridList = $("#gridList");
    var now = new Date();
    var startday = "01";
    var endday = ("0" + now.getDate()).slice(-2);
    var month = ("0" + (now.getMonth() + 1)).slice(-2);
    var start = now.getFullYear() + "-" + (month) + "-" + (startday);
    var end = now.getFullYear() + "-" + (month) + "-" + (endday);
    $("#date_start").val(start);
    $("#date_end").val(end);
    $gridList.dataGrid({
        url: "/Revenue/QryDayTotal",
        height: $(window).height() - 178,
        rowNum: 20,
        rowList: [10, 20, 30, 40, 50],
        colModel: [
            { label: '日期', name: 'date', width: 180, align: 'left',},
            { label: '营收(/元)	', name: 'total', width: 200, align: 'left' }
        ]
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
                    year: start.getFullYear(),
                    startMonth: (start.getMonth() + 1),
                    endMonth: (end.getMonth() + 1),
                    startDay: start.getDate(),
                    endDay: end.getDate()
                }
            }).trigger('reloadGrid');
        }
    });
    $("#btn_search_agent").click(function () {
            $gridList.jqGrid('setGridParam', {
                postData: {
                    agent_AN: $("#txt_search").val()
                }
            }).trigger('reloadGrid');
    });
    $gridList.click(function () {
        if ($("#txt_search").val()!= null && $("#txt_search").val() != "") {
            var keyValue = $("#gridList").jqGridRowValue().date;
            var agent = $("#txt_search").val();
            // window.location.href = "/Revenue/GetInfoForm?" + "keyValue=" + keyValue + "&agent=" + encodeURI(agent);
            window.location.href = "/Revenue/GetInfoForm?keyValue=" + keyValue + "&agent=" + agent;
        } else {
            var keyValue = $("#gridList").jqGridRowValue().date;
            window.location.href = "/Revenue/GetInfoForm?" + "keyValue=" + encodeURI(keyValue);
        }
        
    });

}

