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

    const title = $("#Title").val();
    const Content = $("#Content").val();
    const StartDate = $("#StartDate").val();
    const EndDate = $("#EndDate").val();
    let Times = $("#Times").val();

    const dataToCheck = new Array ( title, Content, StartDate, EndDate, Times );

    if (checkData(dataToCheck)) {
        const param = {
            allType: 2,
            actID: id,
            title: title,
            content: Content,
            StartDate: chageTime(StartDate),
            EndDate: chageTime(EndDate),
            status: $("#status_id").val(),
            discount: $("#Discount").val(),
            full: $("#Ac_full").val(),
            minus: $("#Minus").val(),
            count: Times,
            typeNum: typeNum
        };

        $.submitForm({

            url: "/Activity/Activity4Add",
            param: param,
            success: function () {
                $.currentWindow().$("#gridList").trigger("reloadGrid");
            }
        });
    } else {
        alert("数据格式输入错误，请重新输入");
    }
}
//时间转换
function chageTime(time) {
    
    return time.replace("T", " ").substring(0, 19)
}

//数据验证
function checkData(parm) {
    const len = parm.length;
    console.log(parm);
    console.log(len)
    for (var i = 0; i < len; i++) {
        console.log(parm[i]);
        if (parm[i].trim() == '')
            return false;
    }
    return true;
}