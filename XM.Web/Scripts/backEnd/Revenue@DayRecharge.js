$(function () {
    Form();
})

function Form() {
    var loc = location.href;
    var len = loc.length;
    var index = loc.indexOf("=");
    var keyValue = decodeURI(loc.substring(index + 1, len));
    $("#txt_date").val(keyValue);
    var $gridList = $("#qryDayList");
    $gridList.dataGrid({
        url: "/Revenue/QryDayRechargeForm?day=" + keyValue,
        height: $(window).height() - 178,
        rowNum: 20,
        rowList: [10, 20, 30, 40, 50],
        sortorder: "desc",
        pager: "#gridPager",
        colModel: [
            {
                label: '时间', name: 'recharge_time', width: 180, align: 'left',
                formatter: function (cellvalue, options, rowObject) {
                    var time = cellvalue;
                    return time.substring(11, 16);
                }
            },
            { label: '充值流水号', name: 'recharge_name', width: 200, align: 'left' },
            { label: '充值金额', name: 'recharge_price', width: 200, align: 'left' },
            { label: '审核人', name: 'agent_AN', width: 200, align: 'left' },
            { label: '充值人', name: 'vip_AN', width: 200, align: 'left' }
        ]
    });
    $("#btn_back").click(function () {
        window.location.href = "/Revenue/RechargeForm";
    });
}