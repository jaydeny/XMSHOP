﻿var notice = new Vue({
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
        this.inits()
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
        inits() {
            let date = new Date();
            this.startTime = date.getFullYear() + "-" + this.dateChage(date.getMonth() + 1) + "-" + this.dateChage(date.getDate()) + " 00:00:00";
            let day = date.getDate();
            date.setDate(day + 7);
            this.endTime = date.getFullYear() + "-" + this.dateChage(date.getMonth() + 1) + "-" + this.dateChage(date.getDate()) + " 00:00:00";
        },
        
        //日期格式变换
        dateChage(date) {
            if (date < 10)
                date = "0" + date;
            return date
        },
        //发布公告请求
        send() {

            if (this.checkData()) {
                let str = '';

                this.seleTable.forEach((data) => {
                    str += data.VipAccountName + ',';
                })
                str = str.substring(0, str.length - 1);
                if (str == '') {
                    this.norn('log', "请选择你要发送公告的会员");
                } else {
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
                        this.narnLo('success', data.msg, "/Notice/NoticRecord")

                    });
                }
            }
        },
        //给所有会员发送公告
        sendAll() {

            if (this.checkData()) {
                const param = {
                    title: this.title,
                    content: this.content,
                    StartDate: this.endTime,
                    EndDate: this.startTime,
                    receiver: null
                }
                $.ajax({
                    url: "/Notice/ReleaseNotic",
                    data: param,
                    dataType: 'json'
                }).then((data) => {
                    this.narnLo('success', data.msg, "/Notice/NoticRecord");

                });
            }
        },
        checkData() {
            if (this.title.trim() == '') {
                this.narn('log', '标题不能为空')
                return false;
            }
            else if (this.content.trim() == '') {
                this.narn('log', '内容不能为空')
                return false;
            }
            return true;
        },
        //上一页
        before() {
            if (this.page <= 1) {
                this.narn('log', '第一页')
            } else {
                this.page--;
                this.getAllVIP();
            }
        },
        //下一页
        next() {
            if (this.page >= this.pageCount) {
                this.narn('log', '最后一页')
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
        },
        //消息弹框
        narn(type, text) {
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
        },
        //带URL的弹框
        narnLo(type, text, href) {
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

                        location.href = href
                    }
                }, {
                    text: '取消',
                    click: function (e) {
                        e.closeNotification()
                    }
                }]
            })
        }


    }
});

//开始时间选择器
laydate.render({
    elem: '#startDatetime'
    , type: 'datetime',
    done: function (val) {
        notice.startTime = val;
    }
});
//结束时间选择器
laydate.render({
    elem: '#endDatetime'
    , type: 'datetime',
    done: function (val) {
        notice.endTime = val;
    }
});
