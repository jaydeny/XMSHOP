new Vue({
    el: "#app",
    data: {
        startTime: '',
        endTime: '',
        title: '',
        content: '',
        //所有会员
        unseleTable: [],
        //选择的会员
        seleTable: [],
        page: 1,
        rows: 20,
        count: '',
        pageCount: ''

    },
    created: function () {
        this.init()
        this.getAllVIP();
    },
    computed: {
        getPageCount() {
            this.pageCount = Math.ceil(this.count / this.rows);
            return this.pageCount;
        }
    },
    methods: {
        //获取该代理商下的所有vip用户
        getAllVIP() {
            const param = {
                page: this.page,
                rows: this.rows
            }
            $.ajax({
                url: "/VIP/GetAll",
                data: param,
                dataType: 'json'
            }).then((data) => {
                this.count = data.total;
                this.unseleTable = data.rows;
            });
        },
        //初始化
        init() {
            let date = new Date();
            this.startTime = date.getFullYear() + "-" + this.dateChage(date.getMonth() + 1) + "-" + this.dateChage(date.getDate());
            let day = date.getDate();
            date.setDate(day + 7);
            this.endTime = date.getFullYear() + "-" + this.dateChage(date.getMonth() + 1) + "-" + this.dateChage(date.getDate());
        },
        //日期格式变换
        dateChage(date) {
            if (date < 10)
                date = "0" + date;
            return date
        },
        //发布公告请求
        send() {
            let str = '';

            this.seleTable.forEach((data) => {
                str += data.VipAccountName + ',';
            })
            str = str.substring(0, str.length - 1);
            const param = {
                title: this.title,
                content: this.content,
                StartDate: this.startTime,
                EndDate: this.endTime,
                receiver: str
            }
            $.ajax({
                url: "/Notice/ReleaseNotic",
                data: param,
                dataType: 'json'
            }).then((data) => {
                alert(data.msg);
                location.href = "/Notice/NoticRecord"
            });
        },
        //上一页
        before() {
            if (this.page <= 1) {
                alert("已经是第一页了")
            } else {
                this.page--;
                this.getAllVIP();
            }
        },
        //下一页
        next() {
            if (this.page >= this.pageCount) {
                alert("已经是最后一页了")
            } else {
                this.page++;
                this.getAllVIP();
            }
        },
        //添加到选中事件
        add_sele(index) {
            const item = this.unseleTable[index];
            if (this.seleTable.indexOf(item) == -1) {
                this.seleTable.splice(0, 0, item)
            }
        },
        del_sele(index) {
            this.seleTable.splice(index, 1);
        }
    }
});