function submitForm() {
    if ($(".content").val() == "") {
        alert("请输入内容后重试!");
        return false;
    }
    else {

        $.submitForm({
            url: "/Notic/Releasenotic",
            param: { "title": $(".title").val(), "content": $(".content").val(), "StartDate": $("#StartDate").val(), "EndDate": $("#EndDate").val(), "receiver": $("#receiver").val() },
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
                var op = "<option>" + obj.Agent_AN + "</option>"
                $("#selectL").append(op);
            });
        }
    })
}

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