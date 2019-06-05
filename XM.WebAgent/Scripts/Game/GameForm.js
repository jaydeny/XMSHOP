new Vue({
    el: "#ReportsForm",
    data: {
        //代理游戏类型
        gameTypeTable: [],
        //日程游戏记录
        timeGameForm: [],
        //对应天的游戏记录数据
        dateFrom: [],
        //详细记录信息
        allDate: [],
        startTime: "",
        endTime: "",
        //游戏ID
        id: '',
        //记录点击对应日期
        theTime: '',
        is_show: true,
        rows: '10',
        page: '1',
        page_count: '0',
        dataCount: '0'
    },
    created: function () {
        //初始化页面内容
        //显示所有游戏类型
        this.getGameTypeData("/Game/GetGameOrByAccount", 'get', '');

    },
    computed: {
        //计算页数
        total_page() {
            let count = Math.ceil(this.dataCount / this.rows);
            this.page_count = count;
            return count;
        }
    },
    methods: {
        //获取游戏类型
        getGameTypeData(url, type, param) {
            $.ajax({
                url: url,
                data: param,
                method: type,
                dataType: 'json'
            }).then((data) => {
                console.log(data)
                if (data.errorCode == 0) {

                    this.gameTypeTable = data.result;
                } else {
                    norn('warn', data.errorMsg);
                }
            });
        },
        //获取对应游戏记录信息
        getTimeGameForm(url, param) {
            $.ajax({
                url: url,
                data: param,
                dataType: 'json'
            }).then((data) => {
                console.log(data)
                if (data.errorCode == 0) {

                    this.timeGameForm = data.result;
                } else {
                    norn('warn', data.errorMsg);
                }
            });
        },
        //获取对应天的数据
        getDateForm(url, param) {
            $.ajax({
                url: url,
                data: param,
                dataType: 'json'
            }).then((data) => {
                console.log(data)
                if (data.errorCode == 0) {
                    this.page = data.result.pageNum;
                    this.rows = data.result.pageSize;
                    this.dataCount = data.result.total;

                    this.dateFrom = data.result.data;

                } else {
                    norn('warn',data.errorMsg);
                }
            });
        },
        //将一个月的日程游戏记录数据数据可视化
        on_btn_type(id, even) {
            const date = new Date();
            let dateMonth = date.getMonth() + 1;
            if (dateMonth < 10) {
                dateMonth = "0" + dateMonth;
            }
            this.startTime = date.getFullYear() + "-" + dateMonth + "-" + "01"
            this.endTime = date.getFullYear() + "-" + dateMonth + "-" + date.getDate();
            this.id = id;
            const param = {
                ID: this.id,
                startTime: this.startTime,
                endTime: this.endTime
            }
            this.getTimeGameForm("/Game/GetRecordCollectByAgency", param);
            //this.btn_sub();
            this.btn_type_cgClass(even)
        },
        //改变按钮点击样式
        btn_type_cgClass(e) {
            const game_TypeList = $(".list-group-item");
            $.each(game_TypeList, function (i, v) {
                v.className = "list-group-item list-group-item-action";
            });
            e.target.className = "list-group-item list-group-item-action active";
        },
        //将对应天的数据可视化
        theDayTimeData(date) {
            this.theTime = date;
            this.is_show = false;
            this.btn_sub();
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
        //确认跳转数据页面
        btn_sub() {
            const param = {
                "page": this.page,
                "rows": this.rows,
                "time": this.theTime,
                "ID": this.id
            }
            this.getDateForm("/Game/GetRecord", param);
        },
        //搜索
        search() {
            const param = {
                ID: this.id,
                startTime: this.startTime,
                endTime: this.endTime
            }
            this.getTimeGameForm("/Game/GetRecordCollectByAgency", param);
        },
        //获取具体游戏记录数据
        getAllData(url, param) {
            $.ajax({
                url: url,
                data: param,
                method: 'get',
                dataType: 'json'
            }).then((data) => {
                //console.log(data)
                narn('log', '抱歉，暂无数据')
            });
            ///这里有BUG前端发送后台没有得到响应，数据已经是可以传输到后台的，可能是游戏API端出现问题
            narn('log', '抱歉，暂无数据')
        },
        //单击具体游戏记录
        btn_getAllData(id) {
            // console.log(id)
            var params = {
                ID: id
            }
            this.getAllData("/Game/GetRecordSpecific", params);
        },
        //时间转换
        chageTime(time) {
            return time.replace("T", " ").substring(0, 19)
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