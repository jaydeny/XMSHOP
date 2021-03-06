﻿// 地址容器
var myAddress;
// 修改地址ID
var updateId = 0;
// 地址模板
var addressTemplate = function (i, obj) {
    return "<li data-index='" + i + "'><div class='flex-1' ><input type='checkbox' /></div ><div class='flex-2'>" + obj.address_name + "</div><div class='flex-1'><a class='update'>修改</a>&nbsp;&nbsp;<a class='delete'>删除</a></div><div class='flex-2' ><input class='site' type='checkbox' style='vertical-align: middle;margin: 0;' data-val='1015' /> <span></span></div ></li >";
}
// 清空
var emptyAddress = function () {
    $("#txt_address").val("");
    updateId = 0;
    $("#btnAddAddress").text("新添地址");
}

// 查询会员地址
var addressAll = function () {
    // 清空
    emptyAddress();
    $(".site-list>ul > li:not(:first-child)").remove();
    $.post("/vipinfo/Address",null,function (data) {
        // 保存数据
        myAddress = data.rows;
        $.each(myAddress, function (i, n) {
            $(".site-list>ul").append(addressTemplate(i, n));
        })
    }, "json")
}
// 清空按钮
$("#btnEmptyAddress").click(function () {
    emptyAddress();
});

// 地址删除
$(".site-list").on("click", ".delete", function () {
    var address_id = myAddress[$(this).closest("li").data("index")].id;
    $.get("/vipinfo/DeleteAddress", { "address_id": address_id }, function (data) {
        if (data.success) {
            narn('success', "删除收货地址成功") 
            $(".site-list>ul > li:not(:first-child)").remove();
            addressAll();
        } else {
            narn('warn', "删除收货地址失败") 
        }
    }, "json")
})
// 地址修改
$(".site-list").on("click", ".update", function () {
    // 保存Id
    updateId = myAddress[$(this).closest("li").data("index")]
    $("#btnAddAddress").text("修改地址");
    $("#txt_address").val(updateId.address_name);
})
// 确定
$("#btnAddAddress").click(function () {
    var site = $.trim($("#txt_address").val());
    if (site == "") {
        narn('log', '请输入地址')
        return false;
    }
    var url = "InsertAddress";
    if (updateId != 0) {
        url = "UpdateAddress"
    }
    var tolerant = $("#tolerant").prop("checked") == true ? 1 : 0;
    $.post("/vipinfo/" + url, { "address_id": updateId, "address_name": site, status_id: tolerant }, function (data) {
        if (data.success) {
            if (data.msg == "vip021") {
                narn('success', "添加收货地址成功") 
            }
            $("#tolerant").prop("checked", false);
            narn('success', "修改收货地址成功") 
            addressAll();
        } else {
            if (data.msg == "vip022") {
                narn('warn', "添加收货地址失败")
            }
            narn('warn', "修改收货地址失败") 
        }
    }, "json")
});

// 初始化
addressAll();

// 默认地址
$(".site-list").on("click", ".site", function () {
    var address_id = myAddress[$(this).closest("li").data("index")].id;
    $.get("/vipinfo/SiteTolerant", { "address_id": address_id }, function (data) {
        if (data.success) {
            narn('success', "设置成功")
            $(".site-list>ul > li:not(:first-child)").remove();
            addressAll();
        } else {
            narn('warn', "设置失败")
        }
    }, "json")
})


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