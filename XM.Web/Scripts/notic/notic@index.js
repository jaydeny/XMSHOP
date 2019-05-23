//弹出模态框
function btn_add() {
    $.modalOpen({
        id: "Form",
        title: "新增",
        url: "/NoticManager/Form",
        width: "700px",
        height: "600px",
        callBack: function (iframeId) {
            top.frames[iframeId].submitForm();
        }
    });
}

$(function () {
    gridList();
    $("#gridList").on("click", ".delete", function () {
        var id = $(this).data("val");

        if (confirm("确定删除该公告?")) {
            $.ajax({
                url: "/NoticManager/Delete",
                data: { id },
                success: function (data) {
                    var e = JSON.parse(data);
                    console.log(e);
                    if (e.success) {
                        alert(e.msg)
                    }
                    gridList();
                }
            })
        }
    });
})

function gridList() {
    var $gridList = $("#gridList");
    $gridList.dataGrid({
        url: "/NoticManager/Manager",
        height: $(window).height() - 178,
        colModel: [
            { label: '标题', name: 'title', width: 100, align: 'center'},
            { label: '内容', name: 'content', width: 240, align: 'center' },
            { label: '开始时间', name: 'starttime', width: 160, align: 'center' },
            { label: '结束时间', name: 'endtime', width: 160, align: 'center' },
            { label: '发布人', name: 'publisher', width: 80, align: 'center' },
            {
                label: '操作', name: '_id', width: 160, align: 'center',
                formatter: function (cellvalue, options, rowObject) {
                    return '<button class="delete" data-val="' + cellvalue + '">删除</button>';
                }
            },
        ],
        rowNum: 20,
        rowList: [10, 20, 30, 40, 50],
        sortorder: "desc",
        pager: "#gridPager"
    });

    //$.ajax({
    //    url: "/NoticManager/Manager",
    //    data: {
    //        page: "1",
    //        rows: $("#pageSize option:first").val(),
    //        title: $("#txt_search_title").val(),
    //        receiver: $("#txt_search_receiver").val()
    //    },
    //    dataType: "json",
    //    success: function (data) {
    //        $("#pageNav").val(data.page);
    //        $("#pageSum").val(data.total);
    //        $("#gridList").empty();
    //        dynamicTab(data.rows);
    //    }
    //});
}

function dynamicTab(data) {
    var $gridList = $("#gridList");
    var $tr = $("<tr style='background-color:#e5e2e2;'></tr>");
    $tr.append("<th style='text-align: center;border-bottom:dashed 1px'>标题</th>");
    $tr.append("<th style='text-align: center;border-bottom:dashed 1px'>内容</th>");
    $tr.append("<th style='text-align: center;border-bottom:dashed 1px'>开始时间</th>");
    $tr.append("<th style='text-align: center;border-bottom:dashed 1px'>结束时间</th>");
    $tr.append("<th style='text-align: center;border-bottom:dashed 1px'>发布人</th>");
    $tr.append("<th style='text-align: center;border-bottom:dashed 1px'>操作</th>");
    $tr.append("<th></th>");
    $gridList.append($tr);
    $.each(data, function (index, obj) {
        var $trTamp = $("<tr></tr>");
        $trTamp.append("<td align='center' style='width:200px;border-bottom:dashed 1px; '>" + obj.title + "</td>");
        $trTamp.append("<td align='center' style='width:200px;border-bottom:dashed 1px'>" + obj.content + "</td>");
        $trTamp.append("<td align='center' style='width:200px;border-bottom:dashed 1px'>" + obj.starttime + "</td>");
        $trTamp.append("<td align='center' style='width:200px;border-bottom:dashed 1px'>" + obj.endtime + "</td>");
        $trTamp.append("<td align='center' style='width:200px;border-bottom:dashed 1px'>" + obj.publisher + "</td>");
        $trTamp.append("<td align='center' style='width:200px;border-bottom:dashed 1px'> <button class='delete' data-val='"+obj._id+"'>删除</button> </td>");
        $trTamp.appendTo($gridList);
    })
};


