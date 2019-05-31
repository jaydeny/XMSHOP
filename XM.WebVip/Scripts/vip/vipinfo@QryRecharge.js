//默认显示本周的数据
var Default = function () {
    $.ajax({
        url: "/VipInfo/QryRecharge",
        data: { "page": count, "rows": rows, "StartDate": $("#StartDate").val(), "EndDate": $("#EndDate").val() },
        success: function (data) {
            var e = JSON.parse(data);
            $(".Recharge-list>ul>li:not(:first-child)").remove();
            $.each(e.rows, function (index, obj) {
                $(".Recharge-list>ul").append(Template(obj));
            });
            showList(e.total);
        }
    })
};
Default();

//--------------------------------------------分页
//当前页数
var count = 1;
var rows = 10;
var allSource = 0;
var counts = 1;
var data = { "page": count, "rows": rows, "StartDate": $("#StartDate").val(), "EndDate": $("#EndDate").val() };

//每页显示条数
var btn_num_Rows_count = $("#btn_num_Rows_count");

//跳转页数
var btn_num_Page_count = $("#btn_num_Page_count");

//显示所有页数
var num_Page_Count = $("#num_Page_Count");
//总条数
var Page_Count = document.getElementById("Page_Count");

//上一页
$(".vipinfo-form").on("click", "#before", function () {
    if (count == 1) {
        alert("已经是第一页了")
    } else {
        count -= 1;
        btn_num_Page_count.val(count);
        data.page = count;
        QryRecharge(data);
    }
});

//下一页
$(".vipinfo-form").on("click", "#end", function () {
    //拿到总页数
    if (count >= counts) {
        alert("最后一页了")
    } else {
        count += 1;
        btn_num_Page_count.val(count);
        data.page = count;
        console.log(data)
        QryRecharge(data);
    }
});

//点击确定
$(".vipinfo-form").on("click", "#btn_num_Page", function () {
    data.page = $("#btn_num_Page_count").val();
    btn_num_Page_count.val($("#btn_num_Page_count").val());
    QryRecharge(data);
});

//封装列表显示函数，传入列表对象进行渲染页面
function showList(page) {
    const page_count = Math.ceil(page / rows);
    //alert(page_count);
    $("#num_Page_Count").text = "共 " + page_count + " 页";
    $("#Page_Count").text = "共 " + page + "条数据";
    counts = page_count;
    addOption(page_count);
    btn_num_Page_count.val(count);
    //将条数提取出去
    allSource = page;
}

function addOption(page_count) {
    var num_page = $("#btn_num_Page_count");
    num_page.empty();
    for (var i = 0; i < page_count; i++) {
        let op = $("<option></option>");
        op.val(i + 1);
        op.text(i + 1);
        num_page.append(op)
    }
}

//table的模板
var Template = function (obj) {
    var status;
    obj.status_id == 6 ? status = "审核中" : obj.status_id == 7 ? status = "充值成功" : status = "充值失败";
    return "<li><div class='flex-1'><span>" + obj.id + "</span></div><div class='flex-1'><span>" + obj.recharge_price + "</span></div><div class='flex-1'><span>" + obj.recharge_integral + "</span></div><div class='flex-1'><span>" + obj.recharge_time.replace('T', ' ') + "</span></div><div class='flex-1'><span>" + status + "</span></div>";
}

//时段查询
var QryRecharge = function (data) {
    $.ajax({
        url: "/VipInfo/QryRecharge",
        data: data,
        success: function (data) {
            var e = JSON.parse(data);
            $(".Recharge-list>ul>li:not(:first-child)").remove();
            $.each(e.rows, function (index, obj) {
                $(".Recharge-list>ul").append(Template(obj));
            });
            showList(e.total);
        }
    })
}

function normal() {
    var normal = { "page": count, "rows": rows, "StartDate": $("#StartDate").val(), "EndDate": $("#EndDate").val() };
    data = normal;
    QryRecharge(normal)
}

function ThisWeek() {
    var ThisWeek = { "page": count, "rows": rows, "StartDate": getWeekStartDate(), "EndDate": getWeekEndDate() };
    $("#StartDate").val(getWeekStartDate());
    $("#EndDate").val(getWeekEndDate());
    data = ThisWeek;
    QryRecharge(ThisWeek)
}

function LastWeek() {
    var LastWeek = { "page": count, "rows": rows, "StartDate": getLastWeekStartDate(), "EndDate": getLastWeekEndDate() };
    $("#StartDate").val(getLastWeekStartDate());
    $("#EndDate").val(getLastWeekEndDate());
    data = LastWeek;
    QryRecharge(LastWeek)
}

function ThisMonth() {
    var ThisMonth = { "page": count, "rows": rows, "StartDate": getMonthStartDate(), "EndDate": getMonthEndDate() };
    $("#StartDate").val(getMonthStartDate());
    $("#EndDate").val(getMonthEndDate());
    data = ThisMonth;
    QryRecharge(ThisMonth)
}

function LastMonth() {
    var LastMonth = { "page": count, "rows": rows, "StartDate": getLastMonthStartDate(), "EndDate": getLastMonthEndDate() };
    $("#StartDate").val(getLastMonthStartDate());
    $("#EndDate").val(getLastMonthEndDate());
    data = LastMonth;
    QryRecharge(LastMonth)
}

//--------------------------------------------------------------日期查询
var now = new Date(); //当前日期
var nowDayOfWeek = now.getDay(); //今天本周的第几天
var nowDay = now.getDate(); //当前日
var nowMonth = now.getMonth(); //当前月
var nowYear = now.getYear(); //当前年
nowYear += (nowYear < 2000) ? 1900 : 0; //
var lastMonthDate = new Date(); //上月日期
lastMonthDate.setDate(1);
lastMonthDate.setMonth(lastMonthDate.getMonth() - 1);
var lastYear = lastMonthDate.getYear();
var lastMonth = lastMonthDate.getMonth();
//格式化日期：yyyy-MM-dd
function formatDate(date) {
    var myyear = date.getFullYear();
    var mymonth = date.getMonth() + 1;
    var myweekday = date.getDate();
    if (mymonth < 10) {
        mymonth = "0" + mymonth;
    }
    if (myweekday < 10) {
        myweekday = "0" + myweekday;
    }
    return (myyear + "-" + mymonth + "-" + myweekday);
}
//获得某月的天数
function getMonthDays(myMonth) {
    var monthStartDate = new Date(nowYear, myMonth, 1);
    var monthEndDate = new Date(nowYear, myMonth + 1, 1);
    var days = (monthEndDate - monthStartDate) / (1000 * 60 * 60 * 24);
    return days;
}
//获得本周的开始日期
function getWeekStartDate() {
    var weekStartDate = new Date(nowYear, nowMonth, nowDay - nowDayOfWeek);
    return formatDate(weekStartDate);
}
//获得本周的结束日期
function getWeekEndDate() {
    var weekEndDate = new Date(nowYear, nowMonth, nowDay + (6 - nowDayOfWeek));
    return formatDate(weekEndDate);
}
//获得上周的开始日期
function getLastWeekStartDate() {
    var weekStartDate = new Date(nowYear, nowMonth, nowDay - nowDayOfWeek - 7);
    return formatDate(weekStartDate);
}
//获得上周的结束日期
function getLastWeekEndDate() {
    var weekEndDate = new Date(nowYear, nowMonth, nowDay - nowDayOfWeek - 1);
    return formatDate(weekEndDate);
}
//获得本月的开始日期
function getMonthStartDate() {
    var monthStartDate = new Date(nowYear, nowMonth, 1);
    return formatDate(monthStartDate);
}
//获得本月的结束日期
function getMonthEndDate() {
    var monthEndDate = new Date(nowYear, nowMonth, getMonthDays(nowMonth));
    return formatDate(monthEndDate);
}
//获得上月开始时间
function getLastMonthStartDate() {
    var lastMonthStartDate = new Date(nowYear, lastMonth, 1);
    return formatDate(lastMonthStartDate);
}
//获得上月结束时间
function getLastMonthEndDate() {
    var lastMonthEndDate = new Date(nowYear, lastMonth, getMonthDays(lastMonth));
    return formatDate(lastMonthEndDate);
}