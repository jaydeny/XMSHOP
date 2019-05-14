// 基于Bootstrap分页插件
var paging = {};
//总条数
paging.total = 110;
// 页面条数
paging.pageTotal = 10;
// 显示页数
paging.showPage = 6;
// 当前页
paging.currentPage = 1;
// 总页数
paging.pageNumber = Math.ceil(paging.total / paging.pageTotal);
// 容器
paging.pagingBox = ".pagination";
// 回调方法
paging.callbackMethod;
// 最大页数
paging.maxpage = 10;

// 刷新
paging.renderPaging = function () {
    var strHtml = "";
    var booPageTotal = false;
    // 上一页
    var up_page =
        '<li id="up_page"><a><span class="glyphicon glyphicon-step-backward" aria-hidden="true"></span></a></li>';
    // 下一页
    var below_page =
        '<li id="below_page"><a><span class="glyphicon glyphicon-step-forward" aria-hidden="true"></span></a></li>';
    // 页的模板
    var beforePage = '<li class="page"><a>@</a></li>';
    var fuzzyPage = '<li><a>...</a></li>';
    // 总页数
    paging.pageNumber = Math.ceil(paging.total / paging.pageTotal);
    if (paging.pageNumber < 2) {
        return false;
    }
    // 形式一
    if (paging.pageNumber <= paging.maxpage) {
        for (var i = 1; i < paging.pageNumber + 1; i++) {
            if (i == paging.currentPage) {
                strHtml += beforePage.replace('@', i).replace('page', "active page");
            } else {
                strHtml += beforePage.replace('@', i);
            }
        }
    }

    // 形式二
    if (paging.pageNumber > paging.maxpage && paging.currentPage < paging.showPage) {
        for (var i = 1; i < paging.showPage; i++) {
            if (i == paging.currentPage) {
                strHtml += beforePage.replace('@', i).replace('page', "active page");
            } else {
                strHtml += beforePage.replace('@', i);
            }
        }
        strHtml += fuzzyPage;
        strHtml += beforePage.replace('@', paging.pageNumber);
        strHtml = up_page + strHtml + below_page
        booPageTotal = true;
    }

    // 形式三
    if (paging.pageNumber > paging.maxpage && paging.currentPage > paging.showPage && paging.currentPage > (paging.pageNumber -
        paging.showPage)) {
        strHtml += beforePage.replace('@', 1);
        strHtml += fuzzyPage;
        for (var i = paging.pageNumber - paging.showPage + 1; i < paging.pageNumber + 1; i++) {
            if (i == paging.currentPage) {
                strHtml += beforePage.replace('@', i).replace('page', "active page");
            } else {
                strHtml += beforePage.replace('@', i);
            }
        }
        strHtml = up_page + strHtml + below_page
        booPageTotal = true;
    }
    // 形式四
    if (paging.pageNumber > paging.maxpage && paging.currentPage >= paging.showPage && paging.currentPage <= (paging.pageNumber -
        paging.showPage)) {
        strHtml += beforePage.replace('@', 1);
        strHtml += fuzzyPage;
        strHtml += beforePage.replace('@', paging.currentPage - 2);
        strHtml += beforePage.replace('@', paging.currentPage - 1);
        strHtml += beforePage.replace('@', paging.currentPage).replace('page', "active page");
        strHtml += beforePage.replace('@', paging.currentPage + 1);
        strHtml += beforePage.replace('@', paging.currentPage + 2);
        strHtml += fuzzyPage;
        strHtml += beforePage.replace('@', paging.pageNumber);
        strHtml = up_page + strHtml + below_page
        booPageTotal = true;
    }
    // 刷新
    var showRefresh = '<li id="refresh"><a>刷新</a></li>';
    strHtml += showRefresh;
    if (booPageTotal) {
        var showSetSkipPage = '<li><a>到第<input id="skipPage" value=' + paging.currentPage +
            ' />页</a></li><li><a><button type="button" class="btn btn-primary" id="butConfirm">确定</button></a></li>';
        strHtml += showSetSkipPage;
    }
    // 显示条数
    var showTotal = '<li><a>共<span>' + paging.total + '</span>条</a></li>';
    strHtml += showTotal;
    $(paging.pagingBox).html(strHtml);
}
// 单击页
$(paging.pagingBox).on("click", ".page a", function () {
    paging.currentPage = parseInt($(this).text());
    paging.callbackMethod();
});
// 上一页
$(".pagination").on("click", "#up_page", function () {
    if ((paging.currentPage - 1) > 0) {
        --paging.currentPage;
        paging.callbackMethod();
    }
});
// 下一页
$(".pagination").on("click", "#below_page", function () {
    if ((paging.currentPage + 1) <= paging.pageNumber) {
        ++paging.currentPage;
        paging.callbackMethod();
    }
});
// 刷新
$(".pagination").on("click", "#refresh", function () {
    paging.currentPage = 1;
    paging.callbackMethod();
});
// 页数跳转
$(".pagination").on("click", "#butConfirm", function () {
    var skipVal = $("#skipPage").val();
    if (skipVal > 0 && skipVal <= paging.pageNumber) {
        paging.currentPage = skipVal;
        paging.callbackMethod();
    }
});