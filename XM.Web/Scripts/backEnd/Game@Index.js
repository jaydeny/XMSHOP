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
    $.ajax({
        url: "/GameRecord/Record",
        type: "POST",
        data: {
            page: "1",
            rows: "20"
        },
        dataType: "json",
        success: function (data) {
            if (data.errorMsg == null) {
                $("#gridList").html("");
                dynamicTab(data);
            }
            else {
                alert(data.errorMsg);
            }
        }
    });

    $("#btn_search").click(function () {
        var start = new Date($("#date_start").val());
        var end = new Date($("#date_end").val());
        var time = (end - start) / (1000 * 60 * 60 * 24);
        if (time > 90) {
            alert("当前用户可以查阅三个月内的记录")
        }
        else {
            $.ajax({
                url: "/GameRecord/Record",
                type: "POST",
                data: {
                    page: "1",
                    rows: "20",
                    starttime: start.getFullYear() + "-"+(start.getMonth() + 1) + "-" + start.getDate(),
                    endtime: end.getFullYear() + "-"+(end.getMonth() + 1) + "-" + end.getDate()
                },
                dataType: "json",
                success: function (data) {
                    if (data.result != null) {
                        $("#gridList").html("");
                        dynamicTab(data);
                    }
                    else {
                        alert("没有查询到数据！");
                    }
                }
            });
        }
    });
    $("#btn_search_agent").click(function () {
        var start = new Date($("#date_start").val());
        var end = new Date($("#date_end").val());
        var time = (end - start) / (1000 * 60 * 60 * 24);
        if (time > 90) {
            alert("当前用户可以查阅三个月内的记录")
        }
        else {
            $.ajax({
                url: "/GameRecord/Record",
                type: "POST",
                data: {
                    page: "1",
                    rows: "20",
                    starttime: start.getFullYear() + "-" + (start.getMonth() + 1) + "-" + start.getDate(),
                    endtime: end.getFullYear() + "-" + (end.getMonth() + 1) + "-" + end.getDate(),
                    agentAccount: $("#txt_search_agent").val()
                },
                dataType: "json",
                success: function (data) {
                    if (data.result != null) {
                        $("#gridList").html("");
                        dynamicTab(data);
                    }
                    else {
                        alert("没有查询到数据");
                    }
                    $("#txt_search_agent").val("");
                }
            });
        }

    });
    $("#btn_search_vip").click(function () {
        var start = new Date($("#date_start").val());
        var end = new Date($("#date_end").val());
        var time = (end - start) / (1000 * 60 * 60 * 24);
        if (time > 90) {
            alert("当前用户可以查阅三个月内的记录")
        }
        else {
            $.ajax({
                url: "/GameRecord/Record",
                type: "POST",
                data: {
                    page: "1",
                    rows: "20",
                    starttime: start.getFullYear() + "-" + (start.getMonth() + 1) + "-" + start.getDate(),
                    endtime: end.getFullYear() + "-" + (end.getMonth() + 1) + "-" + end.getDate(),
                    vipAccount: $("#txt_search_vip").val()
                },
                dataType: "json",
                success: function (data) {
                    if (data.result != null) {
                        $("#gridList").html("");
                        dynamicTab(data);
                    }
                    else {
                        alert("没有查询到数据");
                    }
                    $("#txt_search_vip").val("");
                }
            });
        }

    });
}

function dynamicTab(data) {
    var $gridList = $("#gridList");
    var $tr = $("<tr style='background-color:#e5e2e2;'></tr>");
    $tr.append("<th style='text-align: center;border-bottom:dashed 1px'>游戏账号</th>");
    $tr.append("<th style='text-align: center;border-bottom:dashed 1px'>游戏名称</th>");
    $tr.append("<th style='text-align: center;border-bottom:dashed 1px'>时间</th>");
    $tr.append("<th style='text-align: center;border-bottom:dashed 1px'>积分</th>");
    $tr.append("<th></th>");
    $gridList.append($tr);
    var result = data.result.total;
    console.log(result);
    for (var i = 0; i < data.result.total; i++) {
        var $trTamp = $("<tr></tr>");
        $trTamp.append("<td align='center' style='width:200px;border-bottom:dashed 1px; '>" + data.result.data[i].AccountName + "</td>");
        $trTamp.append("<td align='center' style='width:200px;border-bottom:dashed 1px'>" + data.result.data[i].Name + "</td>");
        $trTamp.append("<td align='center' style='width:200px;border-bottom:dashed 1px'>" + data.result.data[i].Time + "</td>");
        $trTamp.append("<td align='center' style='width:200px;border-bottom:dashed 1px'>" + data.result.data[i].Integral + "</td>");
        $trTamp.appendTo($gridList);
    }

}

