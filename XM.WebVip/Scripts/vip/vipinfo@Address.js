// 地址容器
var myAddress = [{ AddressName: "21212", AddressID: "1" }];
// 修改地址ID
var updateId = 0;
// 地址模板
var addressTemplate = function (i, obj) {
    return "<li><div class='flex-1' ><input type='checkbox' checked /></div ><div class='flex-2'>" + obj.AddressName + "</div><div class='flex-1' data-index='" + i + "'><a class='update'>修改</a><a class='delete'>删除</a></div></li >";
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
    $.post("/vip/Address", function (data) {
        // 保存数据
        myAddress = data.rows;
        $.each(myAddress, function (i, n) {
            $(".site-list>ul").append(addressTemplate(i, n));
        })
    }, "josn")
}
// 清空按钮
$("#btnEmptyAddress").click(function () {
    emptyAddress();
});

// 地址删除
$(".site-list").on("click", ".delete", function () {
    var address_id = myAddress[$(this).parent().data("index")].AddressID;
    $.get("/vip/DeleteAddress", { "address_id": address_id }, function () {
        if (data.success) {
            alert(data.msg);
            $(".site-list>ul > li:not(:first-child)").remove();
            addressAll();
        } else {
            alert(data.msg);
        }
    }, "josn")
})
// 地址修改
$(".site-list").on("click", ".update", function () {

    // 保存Id
    updateId = myAddress[$(this).parent().data("index")].AddressID;
    $("#btnAddAddress").text("修改地址");
    $("#txt_address").val(myAddress[$(this).parent().data("index")].AddressName);
})
// 确定
$("#btnAddAddress").click(function () {
    var site = $.trim($("#txt_address").val());
    if (site == "") {
        alert("请输入地址");
        return false;
    }
    var url = "InsertAddress";
    if (updateId != 0) {
        url = "UpdateAddress"
    }
    $.post("/vip/" + url, { "address_id": updateId, "AddressName": site }, function (data) {
        if (data.success) {
            alert(data.msg);
            addressAll();
        } else {
            alert(data.msg);
        }
    }, "josn")
});

// 初始化
addressAll();

