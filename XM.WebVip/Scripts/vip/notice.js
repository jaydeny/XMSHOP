// 公告渲染
function applyNoticebox(id,data) {
    $(".notice-box nav").html("");
    var act = ""
    $.each(data, function (i, n) {
        if (n._id == id) {
            act = "active";
            $("#title").text(n.title);
            $("#txt_time").text(n.starttime.substr(0, 10));
            $("#txtcontent").html(n.content);
        }
        $("#notice-box").append('<li role="presentation" data-id="' + n._id + '" class="' + act + '"><a href="#">' + n.title+'</a></li>');
        act = "";
    });
}

// 获取公告
var noticeData;
function getNotice() {
    $.get("/Notice/GetNotice", function (data) {
        console.log(data);
        noticeData = data.rows;
        applyNoticebox($("#notice").data("id"), noticeData);
    }, "json")
}
getNotice();
$("#notice-box").on("click", "li", function () {
    $("#notice-box li.active").removeClass("active");
    var id = $(this).data("id");
    $(this).addClass("active");
    $.each(noticeData, function (i, n) {
        if (n._id == id) {
            act = "active";
            $("#title").text(n.title);
            $("#txt_time").text(n.starttime.substr(0, 10));
            $("#txtcontent").html(n.content);
        }
    });
    $("#title").text(n.title);
    $("#txt_time").text(n.starttime.substr(0, 10));
    $("#txtcontent").html(n.content);
})
