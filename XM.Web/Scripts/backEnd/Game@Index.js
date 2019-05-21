$(function () {
    gridList();
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
    var pageNum = 1;
    var pageSum;
    $.ajax({
        url: "/GameRecord/Record",
        type: "POST",
        dataType: "json",
        success: function (data) {
            if (data.errorMsg == null) {
                if (data.result.total == 0) {
                    alert("系统繁忙，请稍后重试！");
                }
                else {
                    $("#pageNav").val(data.result.pageNum);
                    $("#pageSum").val(data.result.pageSum);
                    pageSum = data.result.pageSum;
                    $("#pageSize").val(data.result.pageSize);
                    $("#gridList").empty();
                    dynamicTab(data);
                }
            }
            else {
                alert(data.errorMsg);
            }
        }
    });
    //下一页
    $("#next").click(function () {
        if (pageNum < pageSum) {
            pageNum += 1;
            $("#pageNav").val(pageNum);
            searchClick(pageNum);
        }
        else {
            alert("已是最后一页！");
        }
    });
    $("#prev").click(function () {
        if (pageNum > 1) {
            pageNum -= 1;
            $("#pageNav").val(pageNum);
            searchClick(pageNum);
        }
        else {
            alert("已是第一页！");
        }
    });
    $("#npage").click(function () {
        var pageNav = parseInt($("#pageNav").val());
        if (pageNav < 1 || pageNav > pageSum) {
            alert("该页面不存在！");
        }
        else {
            pageNum = pageNav;
            searchClick(pageNum);
        }
    });
    $("#pageSize").change(function () {
        searchClick(pageNum);
    })
    $("#btn_search").click(function () {
        searchClick(pageNum);
    });
    $("#btn_search_agent").click(function () {
        searchClick(pageNum);
    });
    $("#btn_search_vip").click(function () {
        searchClick(pageNum);
    });
}
function searchClick(pageNum) {
    var start = new Date($("#date_start").val());
    var end = new Date($("#date_end").val());
    var time = (end - start) / (1000 * 60 * 60 * 24);
    var pageSize = parseInt($("#pageSize option:selected").val());
    var page = ((pageNum-1) * pageSize) + 1;
    if (time > 90) {
        alert("当前用户可以查阅三个月内的记录")
    }
    else {
        $.ajax({
            url: "/GameRecord/Record",
            type: "POST",
            dataType: "json",
            data: {
                page: page,
                rows: $("#pageSize option:selected").val(),
                vipAccount: $("#btn_search_vip").val(),
                agentAccount: $("#btn_search_agent").val(),
                starttime: start.getFullYear() + "-" + (start.getMonth() + 1) + "-" + start.getDate(),
                endtime: end.getFullYear() + "-" + (end.getMonth() + 1) + "-" + end.getDate()
        },
            success: function (data) {
                if (data.errorMsg == null) {
                    if (data.result.total == 0) {
                        alert("系统繁忙，请稍后重试！");
                    }
                    else {
                        $("#pageSum").val(data.result.pageSum);
                        pageSum = data.result.pageSum;
                        $("#pageSize").val(data.result.pageSize);
                        $("#gridList").empty();
                        dynamicTab(data);
                    }
                }
                else {
                    alert(data.errorMsg);
                }
            }
        });
    }
}
function dynamicTab(data) {
    var $gridList = $("#gridList");
    var $tr = $("<tr style='background-color:#e5e2e2;'></tr>");
    $tr.append("<th style='border-bottom:dashed 1px'></th>");
    $tr.append("<th style='text-align: center;border-bottom:dashed 1px'>游戏账号</th>");
    $tr.append("<th style='text-align: center;border-bottom:dashed 1px'>游戏名称</th>");
    $tr.append("<th style='text-align: center;border-bottom:dashed 1px'>时间</th>");
    $tr.append("<th style='text-align: center;border-bottom:dashed 1px'>积分</th>");
    $tr.append("<th></th>");
    $gridList.append($tr);
    var result = data.result.total;
    for (var i = 0; i < result; i++) {
        var $trTamp = $("<tr></tr>");
        $trTamp.append("<td align='center' style='width:200px;border-bottom:dashed 1px; '>" + (i + 1) + "</td>");
        $trTamp.append("<td align='center' style='width:200px;border-bottom:dashed 1px; '>" + data.result.data[i].AccountName + "</td>");
        $trTamp.append("<td align='center' style='width:200px;border-bottom:dashed 1px'>" + data.result.data[i].Name + "</td>");
        $trTamp.append("<td align='center' style='width:200px;border-bottom:dashed 1px'>" + data.result.data[i].Time + "</td>");
        $trTamp.append("<td align='center' style='width:200px;border-bottom:dashed 1px'>" + data.result.data[i].Integral + "</td>");
        $trTamp.appendTo($gridList);
    }
}



