new Vue({
    el: "#app",
    data: {
        //分页参数
        page: 1,
        rows: 10,
        count: '',
        page_count:1,
        //公告数据
        RecordTable: []
    },
    created: function () {
        this.getNoticData();
    },
    computed: {
        total_page() {
            this.page_count = Math.ceil(this.count / this.rows);
            return Math.ceil(this.count / this.rows)
        }
    },
    methods: {
        //获取公告历史数据
        getNoticData() {
            const param = {
                page: this.page,
                rows: this.rows
            }
            $.ajax({
                url: "/Notice/Manager",
                data: param,
                dataType: 'json'
            }).then((data) => {
                this.page_count = data.total;
                this.RecordTable = data.rows;
                this.count = data.records;
            });
        },
        //撤销公告
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
                    narn('sueecss', data.msg)
                    this.getNoticData();
                } else {
                    narn('warn', data.msg)
                }
            });
        },
        //分页
        //上一页
        before() {
            if (this.page <= 1) {
                narn('log', '第一页')
            } else {
                this.page--;
                this.getNoticData();
            }
        },
        //下一页
        next() {
            if (this.page >= this.page_count) {
                narn('log', '最后一页')
            } else {
                this.page++;
                this.getNoticData();
            }
        },
        //时间转换
        chageTime(time) {
            return time.replace("T", " ").substring(0, 19)
        },
        //公告接收人数据呈现方式改变
        chageReceiver(receiver) {
            //console.log(receiver)
            if (receiver != null) {
                return receiver + ""
            }
            return "全体会员"
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