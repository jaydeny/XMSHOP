new Vue({
    el: "#app",
    data: {
        "agentTable": [],
        "agentPassWord": "",
        "newPassWord": "",
        "againPassWord": "",
        "msg": "",
        "email": "",
        "phone": ""
    },
    created: function () {
        this.onLoadData();
    },
    methods: {

        onLoadData() {
            $.ajax({
                url: "/AgentInfo/QryAgentInfo",
                dataType: 'json'
            }).then((data) => {
                this.agentTable = data.rows;
                //填充页面模态框信息
                this.phone = this.agentTable.MobliePhone;
                this.email = this.agentTable.Email;
            });
        },

        //修改密码
        updatePswSubmir() {
            console.log(this.agentTable)
            if (this.agentPassWord !== this.agentTable.AgentPassword) {
                this.msg = "抱歉，当前密码与原密码不一致";
            } else if (this.newPassWord !== this.againPassWord) {
                this.msg = "抱歉两次输入的密码不一致";
            } else {
                const param = {
                    "agent_pwd": this.againPassWord
                }
                $.ajax({
                    url: "/Home/Update",
                    method: "post",
                    data: param,
                    dataType: 'json'
                }).then((data) => {
                    if (data.success) {
                        narn('success', '修改成功')
                        this.onLoadData();
                    } else {
                        narn('warn', '修改失败请联系管理员')
                    }
                    $('#updatePsw').modal('hide')
                });

            }
            this.msg = "";
        },
        //修改信息
        updateInfoSubmir() {
            const param = {
                "agent_mp": this.phone,
                "agent_email": this.email
            }
            $.ajax({
                url: "/Home/Update",
                method: "post",
                data: param,
                dataType: "json"
            }).then((data) => {
                if (data.success) {
                    narn('success', '修改成功')
                    this.onLoadData();
                } else {
                    narn('warn', '修改失败请联系管理员')
                }
            });
            $('#updateInfo').modal('hide');
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