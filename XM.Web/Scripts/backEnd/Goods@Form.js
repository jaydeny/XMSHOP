
var keyValue = $.request("keyValue");
$(function () {
    initControl();
    if (!!keyValue) { //判断是否有值
        $.ajax({
            url: "/Goods/GetFormJson",
            data: { id: keyValue },
            dataType: "json",
            async: false,
            success: function (data) {
                $("#form1").formSerialize(data);
            }
        });
    }
});
function initControl() {
    $.each(top.clients.type, function (i, n) {
        $("#GoodsType").append("<option value='" + n.id + "'>" + n.name + "</option>");
    });
}
function submitForm() {
    if (!$('#form1').formValid()) {
        return false;
    }
    if (!checkFile()) {
        return false;
    }

    var formData = new FormData($("#form1")[0]);
    
    $.ajax({
        url: "/Goods/Save?id=" + keyValue,
        type: "post",
        data: formData,
        dataType:"json",
        contentType: false,
        processData: false,
        success: function (data) {
            alert(data.msg)
            $.currentWindow().$("#gridList").trigger("reloadGrid");
        }
    })
}


var Pic = function () {
    var files = $('#GoodsPicture').prop('files')
    $("#img").attr("src", URL.createObjectURL(files[0]));
}

//验证图片是否为图片格式
var checkFile = function checkImg() {
    var files =  $('#GoodsPicture').prop('files')
    if (files.length > 0) {//未选中文件时，长度为0
        var file = files[0];//因为是单文件上传，所以只有一个文件（File）对象
        var documentName = file.name
        var index = documentName.indexOf("."); //（考虑严谨用lastIndexOf(".")得到）得到"."在第几位
        documentName = documentName.substring(index); //截断"."之前的，得到后缀
        if (documentName != ".bmp" && documentName != ".png" && documentName != ".gif" && documentName != ".jpg" && documentName != ".jpeg") {  //根据后缀，判断是否符合图片格式
            alert("不是指定图片格式,重新选择");
            $('#GoodsPicture').val() == '';  // 不符合，就清除，重新选择
            return false;
        }
    }
    return true;
}
