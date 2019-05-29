//弹出模态框
function btn_add() {
    $.modalOpen({
        id: "Form",
        title: "新增",
        url: "/Activity/ActivityAdd",
        width: "700px",
        height: "600px",
        callBack: function (iframeId) {
            //console.log(iframeId)
            top.frames[iframeId].submitForm();
        }
    });
}

$(function () {
    gridList();
    $("#gridList").on("click", ".delete", function () {
        var id = $(this).data("val");

        if (confirm("确定删除该活动?")) {
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
        url: "/Activity/getAllActtvity",
        height: $(window).height() - 178,
        colModel: [
            { label: '标题', name: 'Title', width: 100, align: 'center' },
            { label: '内容', name: 'Content', width: 240, align: 'center' },
            {
                label: '创建时间', name: 'CreateDate', width: 160, align: 'center',
                formatter: "date", formatoptions: { srcformat: 'Y-m-d', newformat: 'Y-m-d' }
            },
            {
                label: '开始时间', name: 'StartDate', width: 160, align: 'center',
                formatter: "date", formatoptions: { srcformat: 'Y-m-d', newformat: 'Y-m-d' }
            },
            {
                label: '结束时间', name: 'EndDate', width: 160, align: 'center',
                formatter: "date", formatoptions: { srcformat: 'Y-m-d', newformat: 'Y-m-d' } },
            { label: '发布人', name: 'Publisher', width: 80, align: 'center' },
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
}

function dynamicTab(data) {
    var $gridList = $("#gridList");
    var $tr = $("<tr style='background-color:#e5e2e2;'></tr>");
    $tr.append("<th style='text-align: center;border-bottom:dashed 1px'>标题</th>");
    $tr.append("<th style='text-align: center;border-bottom:dashed 1px'>内容</th>");
    $tr.append("<th style='text-align: center;border-bottom:dashed 1px'>创建时间</th>");
    $tr.append("<th style='text-align: center;border-bottom:dashed 1px'>开始时间</th>");
    $tr.append("<th style='text-align: center;border-bottom:dashed 1px'>结束时间</th>");
    $tr.append("<th style='text-align: center;border-bottom:dashed 1px'>发布人</th>");
    $tr.append("<th style='text-align: center;border-bottom:dashed 1px'>操作</th>");
    $tr.append("<th></th>");
    $gridList.append($tr);
    $.each(data, function (index, obj) {
        var $trTamp = $("<tr></tr>");
        
        $trTamp.append("<td align='center' style='width:200px;border-bottom:dashed 1px; '>" + obj.Title + "</td>");
        $trTamp.append("<td align='center' style='width:200px;border-bottom:dashed 1px'>" + obj.Content + "</td>");
        $trTamp.append("<td align='center' style='width:200px;border-bottom:dashed 1px'>" + obj.CreateDate + "</td>");
        $trTamp.append("<td align='center' style='width:200px;border-bottom:dashed 1px'>" + obj.StartDate + "</td>");
        $trTamp.append("<td align='center' style='width:200px;border-bottom:dashed 1px'>" + obj.EndDate + "</td>");
        $trTamp.append("<td align='center' style='width:200px;border-bottom:dashed 1px'>" + obj.Publisher + "</td>");
        $trTamp.append("<td align='center' style='width:200px;border-bottom:dashed 1px'> <button class='delete' data-val='" + obj.id + "'>删除</button> </td>");
        $trTamp.appendTo($gridList);
    })
};