
var ActivityTypeNum = '';

function submitForm() {
    if ($(".content").val() == "") {
        alert("请输入内容后重试!");
        return false;
    }
    else {

        $.submitForm({
            url: "/Activity/Activity4Add",
            param: { "title": $(".title").val(), "content": $(".content").val(), "StartDate": $("#StartDate").val(), "EndDate": $("#EndDate").val(), "receiver": $("#receiver").val(), "full": $("#full").val(), "minus": $("#minus").val(), "discount": $("#discount").val(), "count": $("#count").val(), "typeNum": ActivityTypeNum },
                success: function () {
                $.currentWindow().$("#gridList").trigger("reloadGrid");
            }
        })
    }
}

var getAgent = function () {
    var ListAgent = [];
    $.ajax({
        url: "/Notic/GetAllAgent",
        success: function (data) {
            var e = JSON.parse(data)
            $.each(e.rows, function (index, obj) {
                let op = "<option>" + obj.Agent_AN + "</option>"
                $("#selectL").append(op);
            });
        }
    })
}

//获取数据库中的优惠类型并进行数据渲染
var getActivityType = function () {
    var ListAgent = [];
    $.ajax({
        url: "/Activity/ActivityType",
        success: function (data) {
            var e = JSON.parse(data)
            // console.log(e)
            $.each(e, function (index, obj) {
                let op = "<input type='radio' name='radio' onclick='rdo_change()' value='" + obj.id + "'/>" + obj.name + "";
                //console.log(op)
                $("#Discounts").append(op);
            });
        }
    })
}


   //优惠方案改变时，对应的规则
   var rdo_change = function () {
       
       $("#Discount").empty();
      
       if (event.target.value == '1002') {
           ActivityTypeNum = '1002'
           var text = $(" <span>满</span> <input type='number' id='full'  value='' /> <span>减</span> <input type='number' id='minus'  value='' /> ");
        }
       else if (event.target.value == '1003') {
           ActivityTypeNum = '1003'
           var text = $(" <span>打</span> <input type='number' value='' id='discount' /> <span>%折扣</span>");
       }
       $("#Discount").append(text);
    };

getActivityType();
getAgent();

var leftSel = $("#selectL");
var rightSel = $("#selectR");

leftSel.dblclick(function () {
    $(this).find("option:selected").each(function () {
        $(this).remove().appendTo(rightSel);
    });
});

rightSel.dblclick(function () {
    $(this).find("option:selected").each(function () {
        $(this).remove().appendTo(leftSel);
    });
});

$("#sub").click(function () {
    var selVal = [];
    rightSel.find("option").each(function () {
        selVal.push(this.value);
    });
    selVals = selVal.join(",");
    //selVals = rightSel.val();
    if (selVals == "") {
        $("#receiver").val(selVals);
    } else {
        $("#receiver").val(selVals);
    }
});