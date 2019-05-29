new Vue({
    el: "#app",
    data: {
        //分页参数
        page: 1,
        rows: 10,
        count: '',

        //活动数据
        RecordTable: []
    },
    created: function () {
        this.getNoticData();
    },
    computed: {
        total_page() {
            return Math.ceil(this.count / this.rows)
        }
    },
    methods: {
        //获取活动历史数据
        getNoticData() {
            const param = {
                page: this.page,
                rows: this.rows
            }
            $.ajax({
                url: "/Activity/getAllActtvity",
                data: param,
                dataType: 'json'
            }).then((data) => {
                console.log(data)
                this.count = data.records;
                this.RecordTable = data.rows;
                this.count = data.records;
            });
        },
        //撤销活动
        del_Notic(id) {
            const param = {
                id: id
            }
            $.ajax({
                url: "/Notice/Del_NoticbyID",
                data: param,
                dataType: 'json'
            }).then((data) => {
                if (data.success) {
                    alert(data.msg);
                    this.getNoticData();
                } else {
                    alert(data.msg)
                }
            });
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
        //时间转换
        chageTime(time) {
            return time.replace("T", " ").substring(0, 19)
        }

    }
});