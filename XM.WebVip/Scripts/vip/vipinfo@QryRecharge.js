
var Template = function (obj) {
    var status;
    obj.status_id == 6 ? status = "审核中" : obj.status_id == 7 ? status="充值成功" : status="充值失败";
    return "<li><div class='flex-1'><span>" + obj.id + "</span></div><div class='flex-1'><span>" + obj.recharge_price + "</span></div><div class='flex-1'><span>" + obj.recharge_integral + "</span></div><div class='flex-1'><span>" + obj.recharge_time + "</span></div><div class='flex-1'><span>" + status + "</span></div>";
}

var QryRecharge = function () {
    $.ajax({
        url: "/VipInfo/QryRecharge",
        data: { "page": count, "rows": rows, "recharge_time": $("#date").val()},
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

QryRecharge();



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
        QryRecharge();
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
        QryRecharge();
    }
});


//点击分页
$("#btn_num_Page").click = function () {
    QryRecharge();
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