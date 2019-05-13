﻿
var keyValue = $.request("keyValue");
$(function () {
    if (!!keyValue) { //判断是否有值
        $.ajax({
            url: "/Vip/GetFormJson",
            data: { id: keyValue },
            dataType: "json",
            async: false,
            success: function (data) {
                console.log(data);
                $("#form1").formSerialize(data);
                $("#VipAccountName").attr('disabled', 'disabled');
                $("#AgentID").val(data.AgentID);
            }
        });
    }
    // 获取代理商
    //$.post("/Agent/GetAllUserInfo", function (data) {
    //    $.each(data.rows, function (i, obj) {
    //        $("#AgentID").append("<option value='" + obj.AgentID + "'>" + obj.AgentAccountName + "</option>");
    //    })
    //}, "json").done(function () {
        
    //});
});
function initControl() {
    $.each(top.clients.role, function (i, n) {
        $("#AgentID").append("<option value='" + obj.AgentID + "'>" + obj.AgentAccountName + "</option>");
    });

}
function submitForm() {
    if (!$('#form1').formValid()) {
        return false;
    }
    $.submitForm({
        url: "/Vip/Save?id=" + keyValue,
        param: $("#form1").formSerialize(),
        success: function (data) {
            $.currentWindow().$("#gridList").trigger("reloadGrid");
        }
    })
}