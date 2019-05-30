var StartDate = "";
var EndDate = "";
var GameID;
// 模板
var RecordTemplate = function (obj) {
    return "<li  class='gameRecord' data-id='" + obj.ID+"'><div class='flex-1'><a>" + obj.ID + "</a></div><div class='flex-1'><span>" + obj.Name + "</span></div><div class='flex-1'><span>" + obj.Integral + "</span></div>";
}
var DetailTemplate = function (obj) {
    return "<li><div class='flex-1'><span>" + obj.AccountName + "</span></div><div class='flex-1'><span>" + obj.Integral + "</span></div><div class='flex-1'><span>" + obj.Time.replace('T', ' ') + "</span></div><div class='flex-1'><span>" + obj.Name + "</span></div>";
}


//以下是汇总
var QueryRecord = function () {
    var sum = 0;
    StartDate = $("#StartDate").val();
    EndDate = $("#EndDate").val();
    $.ajax({
        url: "/GameRecord/Record",
        data: { 'StartDate': StartDate, 'EndDate': EndDate },
        success: function (data) {
            var e = JSON.parse(data)
            $(".Record-list>ul>li:not(:first-child)").remove();
            $.each(e.result, function (index, obj) {
                $(".Record-list>ul").append(RecordTemplate(obj));
                sum = sum + obj.Integral
            });
            $("#total").text(sum);
        }
    }, "json")
}


$(".vipinfo-form").on("click", "#back", function () {
    $.get("/GameRecord/RecordPage", function (data, status, xhr) {
            if (!$(".vipinfo-main .info-head").hasClass("hidden")) {
                $(".vipinfo-main .info-head").addClass("hidden");
            }
            $(".vipinfo-main .info-body").html(data);
        }, "html")
    QueryRecord();
});

//以下是详情
var QryDetail = function () {
    $(".vipinfo-form").on("click", ".gameRecord", function () {
        GameID = $(this).data("id");
        $.ajax({
            url: "/GameRecord/DetailPage",
            success: function (data) {
                if (!$(".vipinfo-main .info-head").hasClass("hidden")) {
                    $(".vipinfo-main .info-head").addClass("hidden");
                }
                $(".vipinfo-main .info-body").html(data);
            }
        }, "html").done(function () {
                $.ajax({
                    url: "/GameRecord/Detail",
                    data: { 'PIndex': count, "PSize": rows, 'GameID': GameID, 'StartDate': StartDate, 'EndDate': EndDate },
                    success: function (data) {
                        var e = JSON.parse(data);
                        $(".Detail-list>ul>li:not(:first-child)").remove();
                        $.each(e.result.data, function (index, obj) {
                            $(".Detail-list>ul").append(DetailTemplate(obj));
                        });
                        //总条数
                        showList(e.result.total);
                    }
                }, "json")
        })
        return false;
    });
}

QryDetail();


//当前页数
var count = 1;
var rows = 10;
var allSource = 0;
var counts = 1;

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
        alert("第一页")
    } else {
        count -= 1;
        btn_num_Page_count.val(count);
        QueryDetail();
    }
});

//下一页
$(".vipinfo-form").on("click", "#end", function () {
    //拿到总页数
    if (count >= counts) {
        alert("最后一页")
    } else {
        count += 1;
        btn_num_Page_count.val(count);
        QueryDetail();
    }
});


//点击分页
$("#btn_num_Page").click = function () {
    QueryDetail();
}

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

//读取数据
var QueryDetail = function () {
    $.ajax({
        url: "/GameRecord/Detail",
        data: { 'PIndex': count, "PSize": rows, 'GameID': GameID, 'StartDate': StartDate, 'EndDate': EndDate },
        success: function (data) {
            var e = JSON.parse(data);
            console.log(e)
            $(".Detail-list>ul>li:not(:first-child)").remove();
            $.each(e.result.data, function (index, obj) {
                $(".Detail-list>ul").append(DetailTemplate(obj));
            });
            //总条数
            showList(e.result.total);
        }
    }, "json")
}

//以下是汇总
var QueryRecordBtn = function (date) {
    var sum = 0;
    $.ajax({
        url: "/GameRecord/Record",
        data: date,
        success: function (data) {
            var e = JSON.parse(data)
            $(".Record-list>ul>li:not(:first-child)").remove();
            $.each(e.result, function (index, obj) {
                $(".Record-list>ul").append(RecordTemplate(obj));
                sum = sum + obj.Integral
            });
            $("#total").text(sum);
        }
    }, "json")
}


function normal() {
    var normal = { "StartDate": StartDate, "EndDate": EndDate },
    date = normal;
    QueryRecordBtn(normal)
}

function ThisWeek() {
    var ThisWeek = { "StartDate": getWeekStartDate(), "EndDate": getWeekEndDate() };
    date = ThisWeek;
    QueryRecordBtn(ThisWeek)
}

function LastWeek() {
    var LastWeek = { "StartDate": getLastWeekStartDate(), "EndDate": getLastWeekEndDate() };
    date = LastWeek;
    QueryRecordBtn(LastWeek)
}

function ThisMonth() {

    var ThisMonth = { "StartDate": getMonthStartDate(), "EndDate": getMonthEndDate() };
    date = ThisMonth;
    QueryRecordBtn(ThisMonth)
}

function LastMonth() {
    var LastMonth = { "StartDate": getLastMonthStartDate(), "EndDate": getLastMonthEndDate() };
    date = LastMonth;
    QueryRecordBtn(LastMonth)
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