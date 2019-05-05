
//封装查询功能
function search() {
    const search = $("#search").val();

    let datapram = {
        "vip_AN": search,
        "vip_mp": search,
        "vip_Email": search
    }

    $.ajax({
        url: "/Agent/GetAllVIP",
        method: 'get',
        data: datapram,
        dataType: 'json'
    }).done(function (data) {
        objs = data.rows;
        //调用列表数据可视化函数
        showList(objs);
    })

}

var objs = {};

//编辑用户信息--进行用户表单的填充 ,传入的是一个用户下标
function editVIP(id) {
    //将编辑表单的项拿下来
    let edit_vip_AN = $("#edit_vip_AN");
    let edit_vip_mp = $("#edit_vip_mp");
    let edit_vip_email = $("#edit_vip_email");
    let edit_status_id = $("#edit_status_id");
    let v_id = $("#v_id");
    let agent_id = $("#agent_id");
    //使用下标去获取这个对象
    let vip = objs[id];
    console.log(vip)
    //表单值填充
    edit_vip_AN.val(vip.VipAccountName);
    edit_vip_mp.val(vip.VipMobliePhone);
    edit_vip_email.val(vip.VipEmail);
    edit_status_id.val(vip.StatusID);
    v_id.val(vip.VipID);
    agent_id.val(vip.AgentID);

}
//发送请求，带这个VIP去进行修改
function editToVIP() {
    let edit_vip_AN = $("#edit_vip_AN");
    let edit_vip_mp = $("#edit_vip_mp");
    let edit_vip_email = $("#edit_vip_email");
    let edit_status_id = $("#edit_status_id");
    let vip_id = $("#v_id");
    let agent_id = $("#agent_id");

    //将数据封装
    let datapram = {
        "vip_AN": edit_vip_AN.val(),
        "vip_mp": edit_vip_mp.val(),
        "vip_email": edit_vip_email.val(),
        "status_id": edit_status_id.val(),
        "ID": vip_id.val(),
        "agent_id": agent_id.val()
    }

    $.ajax({
        url: '/VIP/Update',
        method: 'post',
        data: datapram,
        dataType: 'json'
    }).done((data) => {
        if (data.success) {
            //console.log(data)
            $("#editVIP").modal('hide');
            onloadData();
            alert(data.msg)
        }
    });
}

//添加用户
function addVIP() {
    //拿表单数据
    let vip_AN = $("#vip_AN");
    let vip_pwd = $("#vip_pwd");
    let vip_mp = $("#vip_mp");
    let vip_email = $("#vip_email");
    let status_id = $("#status_id");
    //将数据封装
    let datapram = {
        "ID":0,
        "vip_AN": vip_AN.val(),
        "vip_pwd": vip_pwd.val(),
        "vip_mp": vip_mp.val(),
        "vip_email": vip_email.val(),
        "status_id": status_id.val(),
        "agent_id": localStorage.getItem("Agent_ID")
    }
    //发送ajax请求
    $.ajax({
        url: '/VIP/Update',
        method: 'post',
        data: datapram,
        dataType: 'json'
    }).done((data) => {

        if (data.success) {
            vip_AN.val("");
            vip_pwd.val("");
            vip_mp.val("");
            vip_email.val("");
            status_id.val("");
            $("#exampleModalCenter").modal('hide');
            onloadData();
            alert("添加成功");
        } else {
            alert("添加失败");
        }
    });
    //console.log(vip_AN + status_id)
    //清除表单数据并关闭添加窗口
}

//页面加载时，去后台拿数据
function onloadData() {

    $.ajax({
        url: "/VIP/GetAll",
        method: 'get',
        dataType: 'json'
    }).done(function (data) {
        objs = data.rows;
        //调用列表数据可视化函数
       
        showList(objs);
    })
}

//封装列表显示函数，传入列表对象进行渲染页面
function showList(objs) {
   
    $("#tbody").empty();
    $.each(objs, function (index, obj) {
       
        const trs = $("<tr></tr>");


        const vip_mp = $("<td>" + obj.VipMobliePhone + "</td>");
        trs.append(vip_mp)
        const vip_AN = $("<td>" + obj.VipAccountName + "</td>");
        trs.append(vip_AN)
        const vip_email = $("<td>" + obj.VipEmail + "</td>");
        trs.append(vip_email)
        if (obj.StatusID == 1) {
            const status_id = $("<td>" + "启用" + "</td>");
            trs.append(status_id)
        } else {
            const status_id = $("<td>" + "禁用" + "</td>");
            trs.append(status_id)
        }
        const vip_CDT = $("<td>" + obj.CreateTime + "</td>");
        trs.append(vip_CDT)

        const vip_Btn = $("<td><button type='button' class='btn btn - secondary' data-toggle='modal' data-target='#editVIP'onclick='editVIP(" + index + ")'>编辑</button></td>");
        trs.append(vip_Btn)
        $("#tbody").append(trs)
    })
}



//入口函数
$(document).ready(function () {
        onloadData();
});


