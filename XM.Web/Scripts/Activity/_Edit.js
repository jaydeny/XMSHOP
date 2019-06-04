var typeNum = $.request("typeNum");
var id = $.request("id");
$(function () {
    if (!!id) { //判断是否有值
        $.ajax({
            url: "/Activity/detailedInfo",
            data: { typeNum: typeNum, id: id },
            dataType: "json",
            async: false,
            success: function (data) {
                let discount = data.rows[0].Discount
                if (discount) {
                  
                    data.rows[0].Discount = 100 * discount;
                }
                $("#form1").formSerialize(data.rows[0]);
                $.ajax({
                    url: "/Activity/getAllActtvity",
                    data: { id: id },
                    dataType: "json",
                    async: false,
                    success: function (data) {
                        
                        $("#form1").formSerialize(data.rows[0]);

                        if (data.rows[0].Ac_type == '1002') {
                            $("#Ac_type").val("满减优惠");
                        } else {
                            $("#Ac_type").val("折扣优惠");
                        }
                    }
                });
            }
        });
        $("#Ac_type").attr('disabled', 'disabled');
    }
});

function submitForm() {
    //进行数据验证
    //if (!$('#form1').formValid()) {
    //    return false;
    //}
    const param = {
        allType: 2,
        actID: id,
        title: $("#Title").val(),
        content: $("#Content").val(),
        StartDate: chageTime($("#StartDate").val()),
        EndDate: chageTime($("#EndDate").val()),
        status: $("#status_id").val(),
        discount: $("#Discount").val(),
        full: $("#Ac_full").val(),
        minus: $("#Minus").val(),
        count: $("#Times").val(),
        typeNum: typeNum
    };

    $.submitForm({
        
        url: "/Activity/Activity4Add",
        param: param,
        success: function () {
            $.currentWindow().$("#gridList").trigger("reloadGrid");
        } 
    })
}
//时间转换
function chageTime(time) {
    return time.replace("T", " ").substring(0, 19)
}