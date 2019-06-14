
new Vue({
    el: "#msg",
    data: {
        //分页参数
        page: 1,
        rows: 10,
        count: '',
        page_count: 1,

        dataTable: [],
        dataList: [],
        dataDetails: [],
        is_show: true,
        title: '',
        startTime: "",
        endTime: "",
        status: "",
        btn_show: true,
        sear_vipName: ""
    },
    created: function () {
        const date = new Date();
        let dateMonth = date.getMonth() + 1;
        if (dateMonth < 10) {
            dateMonth = "0" + dateMonth;
        }
        let day = date.getDate();
        if (day < 10) {
            day = "0" + day;
        }
        this.startTime = date.getFullYear() + "-" + dateMonth + "-" + "01"
        this.endTime = date.getFullYear() + "-" + dateMonth + "-" + day;
        this.status = 6;
        const param = {
            endTime: this.retEndTime(),
            status: 6,
        }
        const url = "/Examine/QryDayRechargeTotal";
        this.onLoadData(url, param);
    },
    computed: {
        total_page() {
            this.page_count = Math.ceil(this.count / this.rows);
            return Math.ceil(this.count / this.rows)
        }
    },
    methods: {
        //时间段数据
        onLoadData(url, param) {
            $.ajax({
                url: url,
                dataType: 'json',
                data: param
            }).then((data) => {
                let datas = data.rows;
                this.dataSort(datas);
            });
        },
        //日数据
        onLoadDayData(url, param) {
            $.ajax({
                url: url,
                dataType: 'json',
                data: param
            }).then((data) => {
                
                this.dataList = data.rows;
                this.count = data.total;
                this.page_count = Math.ceil(this.count / this.rows);
            });
        },
        //进行日期排序
        dataSort(datas) {
            this.dataTable = datas.sort(function (a, b) {
                let x = a["date"];
                let y = b["date"];
                return ((x < y) ? -1 : ((x > y) ? 1 : 0));
            });
        },
        // 选择显示
        show(e) {
            $("#home_tab")[0].className = "nav-item nav-link"
            $("#profile_tab")[0].className = "nav-item nav-link"
            $("#regression_tab")[0].className = "nav-item nav-link"

            e.target.className = "nav-item nav-link active";

            const date = new Date(this.endTime);
            let param = {
                "startTime": this.startTime,
                "endTime": this.retEndTime()
            }
            if (e.target.id == 'home_tab') {
                param.status = 6
                this.status = 6;
                this.btn_show = true
            } else if (e.target.id == 'profile_tab') {
                param.status = 7
                this.status = 7;
                this.btn_show = false
            } else {
                param.status = 8
                this.status = 8;
                this.btn_show = false
            }
            this.onLoadData("/Examine/QryDayRechargeTotal", param)
        },
        //返回最后的正确时间
        retEndTime() {
            let date = new Date(this.endTime);
            date.setDate(date.getDate()+1);
            return date.getFullYear() + "-" + (date.getMonth() + 1) + "-" + date.getDate();
        },
        //页面跳转
        jumpData(date) {
            this.page = 1;
            this.title = date
            this.is_show = !this.is_show;
            this.btn_sub();
        },
        //时间段搜索
        searches() {
            //将日期拿下了，因为数据绑定已失效
            this.startTime = $("#startlattice").val();
            this.endTime = $("#endlattice").val();

            let date = new Date(this.endTime);
            let dateMonth = date.getMonth() + 1;
            if (dateMonth < 10) {
                dateMonth = "0" + dateMonth;
            }
            date.setDate(date.getDate() + 1);
            const param = {
                "startTime": this.startTime,
                "endTime": this.retEndTime(),
                "status": this.status,
            }
            const url = "/Examine/QryDayRechargeTotal";
            this.onLoadData(url, param);
        },
        //根据名字进行搜索
        schByName() {
            this.page = 1;
            this.btn_sub();
        },
        //通过审核方法
        btn_adopt(id, integral, name, $event) {
            $event.stopPropagation();
            const param = {
                "type": 0,
                "id": id,
                "integral": integral,
                "name": name
            }
            this.onExamine(param);
        },
        //回退审核的方法
        btn_Backward(id, integral, name, $event) {
            $event.stopPropagation();
            const param = {
                "type": 1,
                "id": id,
                "integral": integral,
                "name": name
            }
            this.onExamine(param);
        },
        //审核请求方法
        onExamine(param ) {
          
            $.ajax({
                url: "/Examine/RechargeAudit",
                data: param,
                dataType: 'json',
                method: 'post'
            }).then((data) => {
                const param = {
                    day: this.title,
                    status: this.status
                }
                this.onLoadDayData("/Examine/QryDayRechargeForm", param);
                narn('success', data.msg)
                });
            
        },
        //审核表单详情
        show_Details(index) {
            let myArray = new Array()
            myArray[0] = this.dataList[index];
            this.dataDetails = myArray;
            console.log(this.dataDetails)
            $("#showData").modal("show");
        },
        //时间转换
        chageTime(time) {
            return time.replace("T", " ").substring(0, 19)
        },
        //分页
        //上一页
        before() {
            if (this.page <= 1) {
                narn('log', '第一页')
            } else {
                this.page--;
                this.btn_sub();
            }
        },
        //下一页
        next() {
            if (this.page >= this.page_count) {
                narn('log', '最后一页')
            } else {
                this.page++;
                this.btn_sub();
            }
        },
        btn_sub() {
            
            if (this.page == null || this.page == '' || this.page == 0) {
                this.page = 1;
            }
            if (this.page_count == 0)
                this.page_count = 1;
            if (this.page >= this.page_count) {
                
                this.page = this.page_count;
            }
           
            const param = {
                page: this.page,
                rows: this.rows,
                day: this.title,
                status: this.status,
                vip_id: this.sear_vipName
            }
            this.onLoadDayData("/Examine/QryDayRechargeForm", param);
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