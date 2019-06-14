new Vue({
    el: "#ReportsForm",
    data: {
        //显示日期营收的数据
        dateTable: [],
        startTime: "",
        endTime: "",
        is_show: true,
        is_show_tb: true,
        dateTile: '',
        //一天的数据
        dataForm: [],
        //一天数据总和
        dataCount: 0,
        //分页设置
        rows: '20',
        page: '1',
        page_count: '0',
        detailedData: {}
    },
    created: function () {
        const dates = new Date();

        const param = {
            "year": dates.getFullYear(),
            "month": dates.getMonth() + 1
        }

        this.onloadData("/Form/QryDayTotal", param)
    },
    computed: {
        //计算总营业额
        sum_turnover() {
            let count = 0;
            for (item of this.dateTable) {
                count += item.total;
            }
            return count;
        },
        //计算页数
        total_page() {
            let count = Math.ceil(this.dataCount / this.rows);
            this.page_count = count;
            return count;
        }
        
    },
    methods: {
        //加载首页数据，将数据初始化
        onloadData(url, param) {

            $.ajax({
                url: url,
                method: "get",
                data: param,
                dataType: 'json'
            }).then((data) => {

                let datas = data.rows;
                //进行页面布局排序
                this.dateTable = datas.sort(function (a, b) {
                    let x = a["date"];
                    let y = b["date"];
                    return ((x < y) ? -1 : ((x > y) ? 1 : 0));
                });
                const dateTable = this.dateTable;
                const len = dateTable.length;
                this.startTime = dateTable[len - len].date;
                this.endTime = dateTable[len - 1].date;

            });
        },
        //页面跳转，到对应天的数据
        JumpPage(date) {
            this.page = '1';
            this.dateTile = date;
            this.is_show = false;
            this.btn_sub();
        },
        //加载对应天的数据
        onloadPageData(url, param) {
            $.ajax({
                url: url,
                method: 'get',
                data: param,
                dataType: 'json'
            }).then((data) => {
                this.dataForm = data.rows;
                this.dataCount = data.total;
            });
        },
        //加载订单详细数据
        onloadProductData(url, param) {
            $.ajax({
                url: url,
                method: 'get',
                data: param,
                dataType: 'json'
            }).then((data) => {
                if (data.rows.length > 0) {
                    this.detailedData = data.rows;
                    $("#showData").modal("show");
                } else {
                    narn('warn', '数据繁忙,请稍后再试!')
                }
            });
        },
        //日期内时间搜索
        searches() {
            //将日期拿下了，因为数据绑定已失效
            this.startTime = $("#startlattice").val();
            this.endTime = $("#endlattice").val();

            const endTime = new Date(this.endTime);
            const startTime = new Date(this.startTime);
            const time = (endTime - startTime) / (1000 * 60 * 60 * 24);
            if (time > 90) {
                narn('warn', '当前用户可以查阅三个月内的记录')
            } else {
                const param = {
                    "year": startTime.getFullYear(),
                    "startMonth": startTime.getMonth() + 1,
                    "endMonth": endTime.getMonth() + 1,
                    "startDay": startTime.getDate(),
                    "endDay": endTime.getDate()
                }
                this.onloadData("/Form/QryDayTotal", param)
            }
        },
        //进行数值和金钱的转化
        tranfoMoney(num) {
            return this.tranfoMoneyber(num, 2);
        },
        tranfoMoneyber(number, n) {
            if (n != 0) {
                n = (n > 0 && n <= 20) ? n : 2;
            }
            number = parseFloat((number + "").replace(/[^\d\.-]/g, "")).toFixed(n) + "";
            var sub_val = number.split(".")[0].split("").reverse();
            var sub_xs = number.split(".")[1];

            var show_html = "";
            for (i = 0; i < sub_val.length; i++) {
                show_html += sub_val[i] + ((i + 1) % 3 == 0 && (i + 1) != sub_val.length ? "," : "");
            }

            if (n == 0) {
                return show_html.split("").reverse().join("");
            } else {
                return show_html.split("").reverse().join("") + "." + sub_xs;
            }
        },
        //显示详细信息
        showData(id) {
            const param = {
                "order_id": id
            }
            this.onloadProductData("/Form/QryDetailOrder", param);
        },
        //数据图示化
        showEcharts() {
            this.is_show_tb = !this.is_show_tb;
            var myChart = echarts.init(document.getElementById('main'));
            let date = new Array();
            let total = new Array();
            for (let item of this.dateTable) {

                date.push(item.date);
                total.push(item.total);
            }

            // 指定图表的配置项和数据
            var option = {
                title: {
                    text: this.startTime + '~' + this.endTime
                },
                tooltip: {},
                legend: {
                    data: ['营收额']
                },
                xAxis: {
                    data: date
                },
                yAxis: {},
                series: [{
                    name: '营收额',
                    type: 'bar',
                    data: total
                }]
            };

            // 使用刚指定的配置项和数据显示图表。
            myChart.setOption(option);
        },
        //时间转换
        chageTime(time) {
            return time.replace("T", " ").substring(0, 19)
        },
        //分页:
        before() {
            if (this.page <= 1) {
                narn('log', '第一页')
            } else {
                this.page--;
                this.btn_sub();
            }
        },
        next() {

            if (this.page >= this.page_count) {
                narn('log', '最后一页')
            } else {
                this.page++;
                this.btn_sub();
            }
        },
        btn_sub() {
            const param = {
                page: this.page,
                rows: this.rows,
                day: this.dateTile
            }
            this.onloadPageData("/Form/QryDayForm", param);
        }

    }
});

//提示框弹出方法
function narn(type, text) {
    naranja()[type]({
        title: '温馨提示',
        text: text,
        timeout: '5000',
        buttons: [{
            text: '接受',
            click: function (e) {
                naranja().success({
                    title: '通知',
                    text: '通知被接受'
                })
            }
        }, {
            text: '取消',
            click: function (e) {
                e.closeNotification()
            }
        }]
    })
}
