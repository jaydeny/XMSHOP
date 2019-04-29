
$(".vipinfo-nav a").click(function () {

    if ($(this).prop("href") != "") {
        $.get($(this).prop("href"), function (data, status, xhr) {
            if (!$(".vipinfo-main .info-head").hasClass("hidden")) {
                $(".vipinfo-main .info-head").addClass("hidden");
            }
            $(".vipinfo-main .info-body").html(data);
        }, "html")
    }
    return false;
});
$(".vipinfo-nav .title").click(function () {
    location.href = "http://localhost:56404/vip/vipinfopage";
});