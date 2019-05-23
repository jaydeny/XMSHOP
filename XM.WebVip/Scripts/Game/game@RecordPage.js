var StartDate = "";
var EndDate = "";
var GameID;
// 模板
var RecordTemplate = function (obj) {
    return "<li><div class='flex-1'><a class='gameRecord' href='/GameRecord/DetailPage'>" + obj.ID + "</a></div><div class='flex-1'><span>" + obj.Name + "</span></div><div class='flex-1'><span>" + obj.Integral + "</span></div>";
}
var DetailTemplate = function (obj) {
    return "<li><div class='flex-1'><span>" + obj.AccountName + "</span></div><div class='flex-1'><span>" + obj.Integral + "</span></div><div class='flex-1'><span>" + obj.Time + "</span></div><div class='flex-1'><span>" + obj.Name + "</span></div>";
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
QueryRecord();

$(".vipinfo-form").on("click", "#Query", function () {
    QueryRecord();
});

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
        GameID = $(this).text();
        $.ajax({
            url: $(this).prop("href"),
            success: function (data) {
                if (!$(".vipinfo-main .info-head").hasClass("hidden")) {
                    $(".vipinfo-main .info-head").addClass("hidden");
                }
                $(".vipinfo-main .info-body").html(data);
            }
        }, "html").done(function () {
            // 页面条数
            paging.callbackMethod = function () {
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
            }
            // 回调
            paging.callbackMethod();
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
        alert("已经是第一页了")
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
        alert("最后一页了")
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