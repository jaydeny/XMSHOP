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
                //console.log(data)
                $("#form1").formSerialize(data.rows[0]);

                $("#Ac_id").attr('disabled', 'disabled');
                if (data.rows[0].Ac_type == '1002') {
                    $("#Ac_type").val("满减优惠");
                } else {
                    $("#Ac_type").val("折扣优惠");
                }
                $("#Ac_type").attr('disabled', 'disabled');
                $("#Discount").attr('disabled', 'disabled');
                $("#Ac_full").attr('disabled', 'disabled');
                $("#Minus").attr('disabled', 'disabled');
                $("#Times").attr('disabled', 'disabled');
            }
        });
    }
});