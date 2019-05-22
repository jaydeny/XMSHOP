var orderId = $.request("keyValue");
$(function () {
    if (!!orderId) { //判断是否有值
        $.ajax({
            url: "/Revenue/QryDetailOrder",
            data: { id: orderId },
            dataType: "json",
            async: false,
            success: function (data) {
                $("#form1").formSerialize(data.rows[0]);
                $("#id").attr('disabled', 'disabled');
                $("#order_date").attr('disabled', 'disabled');
                $("#vip_AN").attr('disabled', 'disabled');
                $("#order_mp").attr('disabled', 'disabled');
                $("#order_total").attr('disabled', 'disabled');
                $("#address_name").attr('disabled', 'disabled');
                $("#goods_name").attr('disabled', 'disabled');
                $("#goods_intro").attr('disabled', 'disabled');
                $("#buy_count").attr('disabled', 'disabled');
                $("#buy_total").attr('disabled', 'disabled');
            }
        });
    }
});