

// 地址模板
var addressTemplate = function (obj) {
    return "<li><div class='flex-1' > <input type='checkbox' checked /></div ><div class='flex-2'>深圳-龙华区新牛路-港深国际中心601-c06</div><div class='flex-1' data-id='"+obj.id+"'><a>修改</a><a>删除</a></div></li >";
}
// 查询会员地址
//$.post("/vip/Address", function (data) {
//    console.log(data);
//    $.each(new Array(6), function (i, n) {
//        addressTemplate(n);
//    })

//})
// 修改地址ID
var updateId = 0;
// 地址添加
$("#btnAddAddress").click(function () {
    var txt_address = $("#txt_address").val();
    $.post("/vip/InsertAddress", { address_name: txt_address, }, function (data) {
        if (data.success) {
            $("#txt_address").val("");
            alert(data.msg);
            updateId = 0;
            $("#btnAddAddress").text("新添地址");
        }
        else {
            alert(data.msg);
        }
    }, "json")
});
// 地址修改
$(".site-list").on("click", ".update", function () {
    var updateId = $(this).parent().data("id");
    $("#btnAddAddress").text("修改地址");
})
// 地址删除
$(".site-list").on("click", ".delete", function () {
    var address_id = $(this).parent().data("id");
    alert("删除");
})



