var vm = new Vue({
    el: "#app",
    data: {
        startTime: '',
        endTime: '',
        title: '',
        content: '',
        //活动类型
        activityTypeTable: [],
        page: 1,
        rows: 20,
        count: '',
        pageCount: '',
        typeNum: '',
        countNum: ''
    },
    created: function () {
        this.init()
        this.getActivityType();
    },
    computed: {
        getPageCount() {
            this.pageCount = Math.ceil(this.count / this.rows);
            return this.pageCount;
        }
        
    },
    methods: {
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
        //获取当前活动类型
        getActivityType() {
            $.ajax({
                url: "/Activity/ActivityType",
                dataType: 'json'
            }).then((data) => {
                //console.log(data)
                this.activityTypeTable = data;
            });
        },
        //数据校验
        checkData() {
           
            if (this.title.trim() == '') {
                narn('warn', '标题不能为空')
                return false
            }
            else if (this.content.trim() == '') {
                narn('warn', '内容不能为空')
                
                return false
            } else if (this.typeNum == '') {
                narn('warn', '请选择优惠')
                return false
            } else if (this.typeNum == '1002') {
                
                if ($("#full").val() == '' || $("#minus").val() == '') {
                    narn('warn', '优惠方案提供数据不完整')
                    return false
                }
            } else if (this.typeNum == '1003') {

                if ($("#discount").val() == '') {
                    narn('warn', '优惠方案提供数据不完整')
                    return false
                }
            }
            else if (this.count == '') {
                narn('warn', '请选择优惠次数')
                return false
            }
            return true
        },
        //发送添加活动请求到后台
        send() {
            if (this.checkData()) {
                const param = {
                    allType:1,
                    title: this.title,
                    content: this.content,
                    StartDate: this.startTime,
                    EndDate: this.endTime,
                    typeNum: this.typeNum,
                    full: $("#full").val(),
                    minus: $("#minus").val(),
                    discount: $("#discount").val(),
                    count: this.countNum
                }
                $.ajax({
                    url: "/Activity/Activity4Add",
                    data: param,
                    dataType: 'json'
                }).then((data) => {
                    if (data.success) { 
                        location.href = "/Activity/ActivityRecord"
                    }
                    narn('warn', data.msg)
                });
            }
        },

        //上一页
        before() {
            if (this.page <= 1) {
                narn('log','第一页')
            } else {
                this.page--;
                this.getAllVIP();
            }
        },
        //下一页
        next() {
            if (this.page >= this.pageCount) {
                narn('log', '最后一页')
            } else {
                this.page++;
                this.getAllVIP();
            }
        },
        //添加到选中事件
        add_sele(index) {
            const item = this.unseleTable[index];
            // this.unseleTable.splice(index, 1);
            if (this.seleTable.indexOf(item) == -1) {
                this.seleTable.splice(0, 0, item)
            }
        },
        //删除
        del_sele(index) {
            this.seleTable.splice(index, 1);
        }
    }
});
//对typeNum值进行监听
vm.$watch('typeNum', function () {
    $("#container").empty();
    if (this.typeNum == '1002') {

        var text = $(" <span>满</span> <input type='number' id='full'  value='' /> <span>减</span> <input type='number' id='minus'  value='' /> ");
    }
    else if (this.typeNum == '1003') {

        var text = $(" <span>打</span> <input type='number' value='' id='discount' /> <span>%折扣</span>");
    }
    $("#container").append(text);
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