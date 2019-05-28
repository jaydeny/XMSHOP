//当前页数
var count = 1;
var rows = 10;
var allSource = 0;

//每页显示条数
var btn_num_Rows_count = $("#btn_num_Rows_count");

//跳转页数
var btn_num_Page_count = $("#btn_num_Page_count");

//显示所有页数
var num_Page_Count = document.getElementById("num_Page_Count");
//总条数
var Page_Count = document.getElementById("Page_Count");

//上一页功能
document.querySelector("#before").onclick = function () {
    if (count == 1) {
        alert("已经是第一页了")
    } else {
        count -= 1;
        btn_num_Page_count.val(count);
        onloadData(count, rows);
    }
}

//下一页功能
document.querySelector("#end").onclick = function () {
    //拿到总页数
    const counts = $("#num_Page_Count")[0].name;
    if (count >= counts) {
        alert("最后一页了")
    } else {
        count += 1;
        btn_num_Page_count.val(count);
        onloadData(count, rows);
    }

}

//点击分页
document.querySelector("#btn_num_Page").onclick = function () {

    onloadData(count, rows);
}

//封装列表显示函数，传入列表对象进行渲染页面
function showList(page) {
    const page_count = Math.ceil(page / rows);
    num_Page_Count.innerText = "共 " + page_count + " 页";
    Page_Count.innerText = "共 " + page + "条数据";
    num_Page_Count.name = page_count;
    addOption(page_count);
    //将条数提取出去
    allSource = page;
}

//模板
var DetailTemplate = function (obj) {
    return "<li></div><div class='flex-1'><span>" + obj.AccountName + "</span></div></div ><div class='flex-1'><span>" + obj.Integral + "</span></div><div class='flex-1'><span>" + obj.Time.replace('T', ' ') + "</span></div><div class='flex-1'><span>" + obj.Name + "</span></div>";
}

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
                    $.ajax({
                        url: "/GameRecord/Detail",
                        data: { 'PIndex': paging.currentPage, "PSize": paging.pageTotal, 'GameID': GameID, 'StartDate': StartDate, 'EndDate': EndDate },
                        success: function (data) {
                            var e = JSON.parse(data);
                            console.log(e)
                            $(".Detail-list>ul>li:not(:first-child)").remove();
                            $.each(e.result.data, function (index, obj) {
                                $(".Detail-list>ul").append(DetailTemplate(obj));
                            });
                            //总条数
                            showList(e.result.page)
                        }
                    }, "json")
                })
});