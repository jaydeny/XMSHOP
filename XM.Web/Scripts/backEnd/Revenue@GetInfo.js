$(function () {
    Form();
})

function Form() {
    var loc = location.href;
    console.log(loc.length);
    var len = loc.length;
    var index = loc.indexOf("=");
    if (len > 63) {
        var objindex = loc.indexOf("&");
        var aindex = loc.lastIndexOf("=");
        var keyValue = decodeURI(loc.substring(index + 1, objindex));
        var agent = loc.substring(aindex+1, len);
    } else {
        var keyValue = decodeURI(loc.substring(index + 1, len));
        var agent = "";
    }
    $("#txt_date").val(keyValue);
    var $gridList = $("#qryDayList");
    $gridList.dataGrid({
        url: "/Revenue/QryDayForm?day=" + keyValue + "&agent_AN=" + agent,
        height: $(window).height() - 178,
        rowNum: 20,
        rowList: [10, 20, 30, 40, 50],
        sortorder: "desc",
        pager: "#gridPager",
        colModel: [
            { label:'订单号',name:'OrderID',width:180,align:'left'},
            {
                label: '时间', name: 'OrderDate', width: 180, align: 'left',
                formatter: function (cellvalue, options, rowObject) {
                    var time = cellvalue;
                    return time.substring(11, 16);
                }
            },
            { label: '会员账号', name: 'VipAccountName', width: 200, align: 'left' },
            { label: '顾客电话', name: 'OrderMP', width: 200, align: 'left' },
            { label: '代理账号', name: 'AgentAccountName', width: 200, align: 'left' },
            { label: '总金额', name: 'OrderPrice', width: 200, align: 'left' }
        ]
    });
    $("#btn_back").click(function () {
        window.location.href = "/Revenue/ReportForm";
    });
    $gridList.click(function () {
        var keyValue = $("#qryDayList").jqGridRowValue().OrderID;
        $.modalOpen({
            id: "Form",
            title: "订单详情",
            url: "/Revenue/GetOrderForm?keyValue=" + keyValue,
            width: "430px",
            height: "550px"
        });
    })
}