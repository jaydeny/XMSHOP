new Vue({
    el: "#app",
    data: {
        //分页参数
        page: 1,
        rows: 10,
        count: '',
        page_count: 1,
        //活动数据
        RecordTable: [],
        //详细信息返回数据
        res: [],
        //单击获取的信息
        RecordIndexData: [],
        //标题搜索
        title: '',
        editData: [],
        editRes:[]

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
        //获取活动历史数据
        getNoticData() {
            if (this.page == null || this.page == '' || this.page == 0) {
                this.page = 1;
            }
            if (this.page > this.page_count) {
                this.page = this.page_count;
            }
            const param = {
                page: this.page,
                rows: this.rows,
                title: this.title
            }
            $.ajax({
                url: "/Activity/getAllActtvity",
                data: param,
                dataType: 'json'
            }).then((data) => {
                this.page_count = data.total;
                this.RecordTable = data.rows;
                this.count = data.records;
            });
        },
        //获取详细信息
        getInfoData(typeID, id) {
            const param = {
                typeNum: typeID,
                id: id
            }
            
            $.ajax({
                url: "/Activity/detailedInfo",
                data: param,
                dataType: 'json'
            }).then((data) => {
                this.res = data.rows;
                this.editRes = data.rows;
                });
        },
        btn_getInfo(typeID, id, index) {
            let arr = new Array();
            arr[0] = this.RecordTable[index];
            this.RecordIndexData = arr;
            this.getInfoData(typeID, id);
            //弹出模态框
            $("#showData").modal("show");
        },
        //修改活动
        edit_Activity( index,event) {

            //获取当前时间
            let date = new Date();
            date.setMonth(date.getMonth() + 1);
            //将要修改的内容放到editData里面
            let arr = new Array();
            arr[0] = this.RecordTable[index];
            this.editData = arr;

            const typeID = this.editData[0].Ac_type;
            const ID = this.editData[0].id;

            this.getInfoData(typeID, ID);
            
            $("#showInfoData").modal("show");
            //阻止事件冒泡
            event.stopPropagation();
        },
        //编辑记录请求
        edit_sub() {
            const data = this.editData[0];
            const infodata = this.editRes[0];
            const param = {
                allType: 2,
                actID: data.id,
                title: data.Title,
                content: data.Content,
                StartDate: this.chageTime(data.StartDate),
                EndDate: this.chageTime(data.EndDate),
                typeNum: data.Ac_type,
                full: infodata.Ac_full,
                minus: infodata.Minus,
                discount: infodata.Discount,
                count: infodata.Times,
                status: data.status_id
            }
            $.ajax({
                url: "/Activity/Activity4Add",
                data: param,
                dataType: 'json'
            }).then((data) => {
             
                narn('success', data.msg)
                });
            $("#showInfoData").modal("hide");
        },
        //编辑记录请求
        edit_subByID(id,status) {
            $.ajax({
                url: "/Activity/Activity4Add",
                data: {
                    actID: id,
                    allType: 2,
                    status: status
                },
                dataType: 'json'
            }).then((data) => {
                //console.log(data)
                return data;
                if (data.success) {
                    narn('success', data.msg)
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
            //console.log(time)
            if (time) {
                return time.replace("T", " ").substring(0, 19)
            } else {
                return "无法显示"
            }
            //return time.replace("T", " ").substring(0, 19)
        },
        isNaN(num) {
            if (isNaN(num))
                return "";
            return num;
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