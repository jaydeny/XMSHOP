(function (root, $) {
    function Paging(container, eventContainer) {
        // 基于Bootstrap分页插件
        var that = this;
        //总条数
        this.total = 100;
        // 页面条数
        this.pageTotal = 8;
        // 显示页数(奇数)
        this.showPage = 7;
        // 当前页
        this.currentPage = 1;
        // 总页数
        this.pageNumber = 1;
        // 分页合作
        this.pagingBox = container || ".pagination";
        // 事件依附
        this.eventAttachment = eventContainer || container || ".pagination";
        // 回调方法
        this.callbackMethod;
        // 最大页数
        this.maxpage = 10;
        // 刷新
        this.renderPaging = function () {
            // 显示页判断
            if (that.showPage < 5) {
                that.showPage = 5;
            } else if ((that.showPage % 2) == 0) {
                that.showPage = that.showPage + 1;
            }
            var strHtml = '';
            var booPageTotal = false;
            // 上一页
            var up_page =
                '<li class="up_page"><a><span class="glyphicon glyphicon-step-backward" aria-hidden="true"></span></a></li>';
            // 下一页
            var below_page =
                '<li class="below_page"><a><span class="glyphicon glyphicon-step-forward" aria-hidden="true"></span></a></li>';
            // 页的模板
            var beforePage = '<li class="page"><a>@</a></li>';
            var fuzzyPage = '<li><a>...</a></li>';
            // 总页数
            that.pageNumber = Math.ceil(that.total / that.pageTotal);

            //			console.log("总页数:" + that.pageNumber)
            //			console.log("最大页数:" + that.maxpage)
            //			console.log("显示页数:" + that.showPage)
            //			console.log("当前页数:" + that.currentPage)

            // 当总页数小于2页
            if (that.pageNumber < 2) {
                $(that.pagingBox).html("");
                return false;
            }
            // 总页数小与最大显示页数
            else if (that.pageNumber <= that.maxpage) {
                for (var i = 1; i < that.pageNumber + 1; i++) {
                    if (i == that.currentPage) {
                        strHtml += beforePage.replace('@', i).replace('page', "active page");
                    } else {
                        strHtml += beforePage.replace('@', i);
                    }
                }
            }
            // 显示页在前
            else if (that.currentPage <= ((that.showPage + 1) / 2) && that.currentPage < (that.pageNumber - ((that.showPage + 1) / 2))) {
                for (var i = 1; i < that.showPage; i++) {
                    if (i == that.currentPage) {
                        strHtml += beforePage.replace('@', i).replace('page', "active page");
                    } else {
                        strHtml += beforePage.replace('@', i);
                    }
                }
                strHtml += fuzzyPage;
                strHtml += beforePage.replace('@', that.pageNumber);
                strHtml = up_page + strHtml + below_page
                booPageTotal = true;
            }
            // 显示页在后
            else if (that.currentPage > (that.pageNumber - ((that.showPage + 1) / 2))) {
                strHtml += beforePage.replace('@', 1);
                strHtml += fuzzyPage;
                for (var i = that.pageNumber - that.showPage + 2; i <= that.pageNumber; i++) {
                    if (i == that.currentPage) {
                        strHtml += beforePage.replace('@', i).replace('page', "active page");
                    } else {
                        strHtml += beforePage.replace('@', i);
                    }
                }
                strHtml = up_page + strHtml + below_page
                booPageTotal = true;
            }
            // 显示页在中
            else {
                strHtml += beforePage.replace('@', 1);
                strHtml += fuzzyPage;
                var end = that.currentPage - ((that.showPage - 2 - 1) / 2);
                for (; end < that.currentPage; end++) {
                    strHtml += beforePage.replace('@', end);
                }
                strHtml += beforePage.replace('@', that.currentPage).replace('page', "active page");
                end = that.currentPage + ((that.showPage - 1) / 2);
                for (var i = that.currentPage + 1; i < end; i++) {
                    strHtml += beforePage.replace('@', i);
                }
                strHtml += fuzzyPage;
                strHtml += beforePage.replace('@', that.pageNumber);
                strHtml = up_page + strHtml + below_page;
                booPageTotal = true;
            }
            // 刷新
            var showRefresh = '<li class="refresh"><a>刷新</a></li>';
            strHtml += showRefresh;
            // 显示条数
            var showTotal = '<li><a>共<span>' + that.total + '</span>条</a></li>';
            strHtml += showTotal;
            if (booPageTotal) {
                var showSetSkipPage = '<li><a>到第<input class="skipPage" value=' +
                    that.currentPage + '>/' + that.pageNumber +
                    '页</a></li><li class="li-btn"><a><button type="button" class="btn btn-primary butConfirm">确定</button></a></li>';
                strHtml += showSetSkipPage;
            }
            strHtml = '<ul class="pagination">' + strHtml + '</ul>';
            $(that.pagingBox).html(strHtml);
        }

        // 单击页
        $(this.eventAttachment).on("click", ".page a", function () {
            var currentPage = parseInt($(this).text());
            if (that.currentPage != currentPage && currentPage > 0 && currentPage <= that.pageNumber) {
                that.currentPage = currentPage;
                that.callbackMethod();
            }
        });
        // 上一页
        $(this.eventAttachment).on("click", ".up_page", function () {
            if ((that.currentPage - 1) > 0) {
                --that.currentPage;
                that.callbackMethod();
            }
        });
        // 下一页
        $(this.eventAttachment).on("click", ".below_page", function () {
            if ((that.currentPage + 1) <= that.pageNumber) {
                ++that.currentPage;
                that.callbackMethod();
            }
        });
        // 刷新
        $(this.eventAttachment).on("click", ".refresh", function () {
            that.currentPage = 1;
            that.callbackMethod();
        });
        // 页数跳转
        $(this.eventAttachment).on("click", ".butConfirm", function () {
            var skipVal = parseInt($(that.pagingBox).find(".skipPage").val());
            if (skipVal > 0 && skipVal <= that.pageNumber) {
                that.currentPage = skipVal;
                that.callbackMethod();
            }
        });

    }

    if (typeof exports === "object") {
        module.exports = Paging
    } else if (typeof define === "function" && define.amd) {
        define([], function () {
            return Paging
        })
    } else {
        root.Paging = Paging
    }
})(this, jQuery);