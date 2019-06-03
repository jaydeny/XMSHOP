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
                console.log(data);
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
                console.log(data)
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
        //撤销活动
        edit_Activity( index,event) {
            //const param = {
            //    id: id
            //}
            //$.ajax({
            //    url: "/Notice/Del_NoticbyID",
            //    data: param,
            //    dataType: 'json'
            //}).then((data) => {
            //    if (data.success) {
            //        alert(data.msg);
            //        this.getNoticData();
            //    } else {
            //        alert(data.msg)
            //    }
            //});

            //获取当前时间
            let date = new Date();
            date.setMonth(date.getMonth() + 1);
            //获取点击记录信息
            //this.RecordIndexData = this.RecordTable[index]
            let arr = new Array();
            arr[0] = this.RecordTable[index];
            this.editData = arr;
            
            const typeID = this.editData[0].Ac_type;
            const ID = this.editData[0].id;
            this.getInfoData(typeID, ID);
           
            //将数据进行转换
            //let start = new Date(this.RecordIndexData.StartDate);
            //let end = new Date(this.RecordIndexData.EndDate);
            console.log(this.editData)
            //判断当天是否是在活动范围内 如果是就进行编辑
            //if (start <= date && date <= end) {

            //    console.log("date" + date)
            //    console.log("start" + start)
            //    console.log("end" + end)

            //    console.log(this.RecordIndexData)
                
            //    alert("活动已经开始，无法进行编辑");
            //} else {
            //    console.log("bbb")
            //    if (id == 1) {
            //        //this.edit_subByID(this.RecordIndexData.id,1011);
            //    }
            //}

            //if (id == 1) {
            //    this.edit_subByID(this.RecordIndexData.id, 1011);
            //}
            
            $("#showInfoData").modal("show");
            event.stopPropagation();
        },
        //编辑记录请求
        edit_sub() {
            const data = this.editData[0];
            const infodata = this.editRes[0];
            console.log(infodata)
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
                discount: infodata.Discount * 100,
                count: infodata.Times,
                status: data.status_id
            }
            $.ajax({
                url: "/Activity/Activity4Add",
                data: param,
                dataType: 'json'
            }).then((data) => {
                console.log(data);
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
                console.log(data)
                return data;
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
            //console.log(time)
            if (time) {
                return time.replace("T", " ").substring(0, 19)
            } else {
                return "无法显示"
            }
            //return time.replace("T", " ").substring(0, 19)
        }

    }
});