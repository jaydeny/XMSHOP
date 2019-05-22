
var noticeData;
var act = "active";
// 公告渲染
function applyNoticebox(id,data) {
    $(".notice-box nav").html("");
    var act = ""
    $.each(data, function (i, n) {
        if (n._id == id) {
            act = "active";
            $("#title").text(n.title);
            $("#txt_startTime").text(n.starttime.substr(0, 10));
            $("#txt_endTime").text(n.endtime.substr(0, 10));
            $("#txtcontent").html(n.content);
        }
        $("#notice-box").append('<li role="presentation" data-id="' + n._id + '" class="' + act + '"><a href="#">' + n.title+'</a></li>');
        act = "";
    });
}

// 写入input
function txtInput(n) {
    $("#title").text(n.title);
    $("#txt_startTime").text(n.starttime.substr(0, 10));
    $("#txt_endTime").text(n.endtime.substr(0, 10));
    $("#txtcontent").html(n.content);
}
// 获取公告
function getNotice() {
    $.get("/Notice/GetNotice", function (data) {
        noticeData = data.rows;
        applyNoticebox($("#notice").data("id"), noticeData);
    }, "json")
}
getNotice();
// 列表事件
$("#notice-box").on("click", "li", function () {
    $("#notice-box li.active").removeClass(act);
    var id = $(this).data("id");
    $(this).addClass(act);
})
$("#btnTag").click(function () {
    var that = $("#notice-box li.active");
    var msgid = $(that).data("id");
    var id;
    $.get("/Notice/AddNotice", { msgid }, function (data) {
        if (data.success) {
            if ($(that).next().length != 0) {
                $(that).next().addClass(act);
                id = $(that).next().data("id");
                $("#span_notice").text($(that).next().siblings().length);
                $(that).remove();
            }
            else if ($(that).prev().length != 0) {
                $(that).prev().addClass(act);
                id = $(that).prev().data("id");
                $("#span_notice").text($(that).prev().siblings().length);
                $(that).remove();
            }
            else {
                $("#span_notice").removeClass("red").text(0);
                $('#myModal').modal('hide');
                return false;
            }
            $.each(noticeData, function (i, n) {
                if (n._id == id) {
                    txtInput(n);
                    return false;
                }
            });
        }
    },"json")
});
$("#btnClose").click(function () {
    $('#myModal').modal('hide');
    $.getNotice();
});
