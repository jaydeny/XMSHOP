﻿new Vue({
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
                console.log(data)
                this.agentTable = data.rows;
                //填充页面模态框信息
                this.phone = this.agentTable.MobliePhone;
                this.email = this.agentTable.Email;
            });
        },

        //修改密码
        updatePswSubmir() {
            if (this.agentPassWord.trim() == '' || this.againPassWord.trim() == '' || this.newPassWord.trim() == '') {
                alert("密码为空或格式出现问题");
            }
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
                        alert("修改成功");
                        this.onLoadData();
                    } else {
                        alert("修改失败请联系管理员")
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
                    alert("修改成功");
                    this.onLoadData();
                } else {
                    alert("修改失败请联系管理员")
                }
            });
            $('#updateInfo').modal('hide');
        }
    }
});