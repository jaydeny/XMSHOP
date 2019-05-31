
new Vue({
    el: "#msg",
    data: {
        //分页参数
        page: 1,
        rows: 10,
        count: '',

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
        this.startTime = date.getFullYear() + "-" + dateMonth + "-" + "01"
        this.endTime = date.getFullYear() + "-" + dateMonth + "-" + date.getDate();
        this.status = 6;
        const param = {
            endTime: date.getFullYear() + "-" + dateMonth + "-" + (1 + date.getDate()),
            status: 6,
        }
        const url = "/Examine/QryDayRechargeTotal";
        this.onLoadData(url, param);
    },
    computed: {
        total_page() {
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
        show(e) {
            $("#home_tab")[0].className = "nav-item nav-link"
            $("#profile_tab")[0].className = "nav-item nav-link"
            $("#regression_tab")[0].className = "nav-item nav-link"

            e.target.className = "nav-item nav-link active";

            const date = new Date(this.endTime);
            let param = {
                "startTime": this.startTime,
                "endTime": date.getFullYear() + "-" + (date.getMonth() + 1) + "-" + (1 + date.getDate())
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
        //页面跳转
        jumpData(date) {
            this.page = 1;
            this.title = date
            this.is_show = !this.is_show;
            this.btn_sub();
        },
        //时间段搜索
        searches() {
            let date = new Date(this.endTime);
            let dateMonth = date.getMonth() + 1;
            if (dateMonth < 10) {
                dateMonth = "0" + dateMonth;
            }
            date.setDate(date.getDate() + 1);
            const param = {
                "startTime": this.startTime,
                "endTime": date.getFullYear() + "-" + dateMonth + "-" + 1 + date.getDate(),
                "status": this.status,
            }
            const url = "/Examine/QryDayRechargeTotal";
            this.onLoadData(url, param);
        },
        schByName() {
            this.page = 1;
            this.btn_sub();
        },
        //通过审核方法
        btn_adopt(id, integral, name) {
            const param = {
                "type": 0,
                "id": id,
                "integral": integral,
                "name": name
            }
            this.onExamine(param);
        },
        //回退审核的方法
        btn_Backward(id, integral, name) {
            const param = {
                "type": 1,
                "id": id,
                "integral": integral,
                "name": name
            }
            this.onExamine(param);
        },
        //审核请求方法
        onExamine(param) {
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
                alert(data.msg);
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
                alert("已经是第一页了")
            } else {
                this.page--;
                this.getNoticData();
            }
        },
        //下一页
        next() {
            if (this.page >= this.page_count) {
                alert("已经是最后一页了")
            } else {
                this.page++;
                this.getNoticData();
            }
        },
        btn_sub() {
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
