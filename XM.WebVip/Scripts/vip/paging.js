//总条数
var total = 178;
// 页面条数
var pageTotal = 10;
// 显示页数
var showPage = 6;
// 当前页
var currentPage = 10;
// 总页数
var pageNumber = Math.ceil(total / pageTotal);
// 容器
var pagingBox = ".pagination"
// 刷新
var renderPaging = function () {
    var strHtml = "";
    var booPageTotal = false;
    // 上一页
    var up_page = '<li id="up_page"><a><span class="glyphicon glyphicon-step-backward" aria-hidden="true"></span></a></li>';
    // 下一页
    var below_page = '<li id="below_page"><a><span class="glyphicon glyphicon-step-forward" aria-hidden="true"></span></a></li>';
    // 页的模板
    var beforePage = '<li class="page"><a>@</a></li>';
    var fuzzyPage = '<li><a>...</a></li>';
    // 总页数
    pageNumber = Math.ceil(total / pageTotal);
    // 形式一
    if (pageNumber < 11) {
        for (var i = 1; i < pageNumber + 1; i++) {
            if (i == currentPage) {
                strHtml += beforePage.replace('@', i).replace('page', "active page");
            }
            else {
                strHtml += beforePage.replace('@', i);
            }
        }
        
    }

    // 形式二
    if (pageNumber > 11 && currentPage < showPage) {
        for (var i = 1; i < showPage; i++) {
            if (i == currentPage) {
                strHtml += beforePage.replace('@', i).replace('page', "active page");
            }
            else {
                strHtml += beforePage.replace('@', i);
            }
        }
        strHtml += fuzzyPage;
        strHtml += beforePage.replace('@', pageNumber);
        strHtml = up_page + strHtml + below_page
        booPageTotal = true;
    }

    // 形式三
    if (pageNumber > 11 && currentPage > showPage && currentPage > (pageNumber - showPage)) {
        strHtml += beforePage.replace('@', 1);
        strHtml += fuzzyPage;
        for (var i = pageNumber - showPage + 1; i < pageNumber + 1; i++) {
            if (i == currentPage) {
                strHtml += beforePage.replace('@', i).replace('page', "active page");
            }
            else {
                strHtml += beforePage.replace('@', i);
            }
        }
        strHtml = up_page + strHtml + below_page
        booPageTotal = true;
    }
    // 形式四
    if (pageNumber > 11 && currentPage >= showPage && currentPage <= (pageNumber - showPage)) {
        strHtml += beforePage.replace('@', 1);
        strHtml += fuzzyPage;
        strHtml += beforePage.replace('@', currentPage - 2);
        strHtml += beforePage.replace('@', currentPage - 1);
        strHtml += beforePage.replace('@', currentPage).replace('page', "active page");
        strHtml += beforePage.replace('@', currentPage + 1);
        strHtml += beforePage.replace('@', currentPage + 2);
        strHtml += fuzzyPage;
        strHtml += beforePage.replace('@', pageNumber);
        strHtml = up_page + strHtml + below_page
        booPageTotal = true;
    }
    // 刷新
    var showRefresh = '<li id="refresh"><a>刷新</a></li>';
    strHtml += showRefresh;
    if (booPageTotal) {
        var showSetSkipPage = '<li><a>到第<input id="skipPage" value=' + currentPage + ' />页</a></li><li><a><button id="butConfirm">确定</button></a></li>';
        strHtml += showSetSkipPage;
    }
    // 显示条数
    var showTotal = '<li><a>共<span>' + total + '</span>条</a></li>';
    strHtml += showTotal;
    $(pagingBox).html(strHtml);
}
// 单击页
$(pagingBox).on("click", ".page a", function () {
    currentPage = parseInt($(this).text());
    renderPaging();
});
// 上一页
$(".pagination").on("click", "#up_page", function () {
    if ((currentPage - 1) > 0) {
        --currentPage;
        renderPaging();
    }
});
// 下一页
$(".pagination").on("click", "#below_page", function () {
    if ((currentPage + 1) <= pageNumber) {
        ++currentPage;
        renderPaging();
    }
});
// 刷新
$(".pagination").on("click", "#refresh", function () {
    currentPage = 1;
    renderPaging();
});
// 页数跳转
$(".pagination").on("click", "#butConfirm", function () {
    var skipVal = $("#skipPage").val();
    if (skipVal > 0 && skipVal <= pageNumber) {
        currentPage = skipVal;
        renderPaging();
    }
});