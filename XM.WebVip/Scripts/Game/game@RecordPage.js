var StartDate = "";
var EndDate = "";

// 模板
var RecordTemplate = function (obj) {
    return "<li></div ><div class='flex-1'><a class='gameRecord' href='/GameRecord/DetailPage'>" + obj.ID + "</a></div></div ><div class='flex-1'><span>" + obj.Name + "</span></div><div class='flex-1'><span>" + obj.Integral + "</span></div>";
}
var DetailTemplate = function (obj) {
    return "<li></div><div class='flex-1'><span>" + obj.AccountName + "</span></div></div ><div class='flex-1'><span>" + obj.Integral + "</span></div><div class='flex-1'><span>" + obj.Time + "</span></div><div class='flex-1'><span>" + obj.Name + "</span></div>";
}

////以下是汇总
//var QueryRecord = function () {
//    var sum = 0;
//    $.ajax({
//        url: "/GameRecord/RecordPage",
//        success: function () {
//            $.ajax({
//                url: "/GameRecord/Record",
//                data: { 'StartDate': StartDate, 'EndDate': EndDate },
//                success: function (data) {
//                    var e = JSON.parse(data)
//                    $(".Record-list>ul>li:not(:first-child)").remove();
//                    $.each(e.result, function (index, obj) {
//                        $(".Record-list>ul").append(RecordTemplate(obj));
//                        sum = sum + obj.Integral
//                    });
//                    $("#total").text(sum);
//                }
//            }, "json")
//        }
//    })
//}

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



//以下是详情
$(".vipinfo-form").on("click", ".gameRecord", function () {
    var GameID = $(this).text();
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
                    paging.pagingBox = ".vipinfo-form .pagination";
                    paging.pageTotal = 1;
                    paging.callbackMethod = function () {
                        $.ajax({
                            url: "/GameRecord/Detail",
                            data: { 'PIndex': paging.currentPage, "PSize": paging.pageTotal, 'GameID': GameID, 'StartDate': StartDate, 'EndDate': EndDate },
                            success: function (data) {
                                console.log(data)
                                var e = JSON.parse(data);
                                console.log(e)
                                $(".Detail-list>ul>li:not(:first-child)").remove();
                                $.each(e.result.data, function (index, obj) {
                                    $(".Detail-list>ul").append(DetailTemplate(obj));
                                });
                                //总条数
                                paging.total = e.result.total;
                                paging.renderPaging();
                            }
                        }, "json")
                    }
                    // 回调
                    paging.callbackMethod(); { }
                })
    return false;
});


$(".info-body").on("click", "#back", function () {
    window.location.href = "/GameRecord/RecordPage";
});


