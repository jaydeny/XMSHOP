
// 公告
var noticeData;
// 会员已读公告
var msgStatus = null;
var act = "active";
// 公告渲染
function applyNoticebox(data) {
    $(".notice-box nav").html("");
    var act = "active";
    if (msgStatus != null) {
        var boo;
        $.each(data, function (i, n) {
            var boo = msgStatus.some(function (val) {
                return n._id == val.msgid;
            });
            if (boo) {
                $("#notice-box").append('<li role="presentation" data-id="' + i + '"><a href="#">' + n.title + '<span>已读</span></a></li>');
            }
            else {
                $("#notice-box").prepend('<li role="presentation" data-id="' + i + '"><a href="#">' + n.title + '<span>未读</span></a></li>');
            }
            
        });
        return false;
    }
    $.each(data, function (i, n) {
        $("#notice-box").append('<li role="presentation" data-id="' + i + '"><a href="#">' + n.title +'<span></span></a></li>');
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
        if (data.total == -1) {
            noticeData = data.rows.result;
            msgStatus = data.rows.msgStatus;
        }
        else {
            noticeData = data.rows;
        }
        applyNoticebox(noticeData);
        $("#notice-box>li:first-child").addClass(act);
        txtInput(noticeData[$("#notice-box>li:first-child").data("id")]);
        if ($("#notice-box>li:first-child").find("span").text() == "未读") {
            markRead($("#notice-box>li:first-child"));
        }
    }, "json")
}
getNotice();
// 列表事件
$("#notice-box").on("click", "li", function () {
    if ($(this).hasClass("action")) {
        return false;
    }
    var that = $("#notice-box li.active");
    $(that).removeClass(act);
    $(this).addClass(act);
    var id = $(this).data("id");
    txtInput(noticeData[id]);
    if ($(that).data("read") == "no") {
        $("#notice-box").append(that);
        $(that).data("read", "yes");
    }
    if ($(this).find("span").text() == "未读") {
        markRead(this);
    }
})

function markRead(that) {
    var msgid = noticeData[$(that).data("id")]._id;
    $(that).find("span").text("已读")
    $.get("/Notice/AddNotice", { msgid }, function (data) {
        if (data.success) {
            $(that).data("read", "no");
        }
    }, "json");
    $("#span_notice").text(parseInt($("#span_notice").text())-1);
}
$("#btnClose").click(function () {
    $('#myModal').modal('hide');
});
