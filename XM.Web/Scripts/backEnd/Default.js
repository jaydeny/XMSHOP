$(function () {
    GetSalaryChart();
    GetLeaveChart();
    GetReportForm();
});
function GetSalaryChart() {
    var labels = [];
    var locations = [];
    var datasets = [];
    var oos = [];
    var obj = [];
    $.ajax({
        url: "/Home/GetDefault",
        type: "POST",
        dataType: "json",
        success: function (data) {
            obj = data;
            for (i = 0; i < obj.length; i++) {
                if (labels.indexOf(obj[i].date) === -1) {
                    labels.push(obj[i].date);
                }
            }
            for (i = 0; i < obj.length; i++) {
                if (locations.indexOf(obj[i].agent_AN) === -1) {
                    locations.push(obj[i].agent_AN);
                }
            }
            locations.forEach(function (location) {
                obj.forEach(function (report) {
                    if (report.agent_AN === location) {
                        oos.push(report.total);
                    }
                })
            });
            for (j = 0; j < obj.length; j++) {
                datasets.push({
                    label: obj[j].agent_AN,
                    fillColor: "rgba(" + (j * 5) * 2 + "," + (j + 5) * 3 + "," + (j + 15) * 2 + ",0.2)",
                    strokeColor: "rgba(" + (j * 5) * 2 + "," + (j + 5) * 3 + "," + (j + 15) * 2 + ",1)",
                    pointColor: "rgba(" + (j * 5) * 2 + "," + (j + 5) * 3 + "," + (j + 15) * 2 + ",1)",
                    pointStrokeColor: "#fff",
                    pointHighlightFill: "#fff",
                    pointHighlightStroke: "rgba(" + (j * 5) * 2 + "," + (j + 5) * 3 + "," + (j + 15) * 2 + ",1)",
                    data: [oos[j], oos[j + 1], oos[j + 2], oos[j + 3], oos[j + 4], oos[j + 5]]
                });
                var $span = $("div[name='agent']").append("<span style='margin-left: 10px; font-weight: 500;'></span>");
                $span.append("<i class='fa fa-square' style='color: " + "rgba(" + (j * 5) * 2 + ", " + (j + 5) * 2 + ", " + (j + 15) * 2 + ", 1)" + "; font-size: 20px; padding-right: 5px; vertical-align: middle; margin-top: -3px;'></i>" + obj[j].agent_AN);
                j += 5;
            }
            var lineChartData = {
                labels: labels,
                datasets: datasets
            };
            var ctx = document.getElementById("salarychart").getContext("2d");
            window.myLine = new Chart(ctx).Line(lineChartData, {
                responsive: false,
                bezierCurve: false
            });
        }
    });
}
function GetLeaveChart() {
    var date = new Date();
    var labels = [];
    var locations = [];
    var datasets = [];
    var oos = [];
    var obj = [];
    $.ajax({
        url: "/Home/GetBar",
        type: "POST",
        dataType: "json",
        data: {
            starttime: date.getFullYear() + "-" + (date.getMonth()),
            endtime: date.getFullYear() + "-" + (date.getMonth() + 2)
        },
        success: function (data) {
            obj = data.result;
            console.log(obj);
            for (i = 0; i < obj.length; i++) {
                if (labels.indexOf(obj[i].Date) === -1) {
                    labels.push(obj[i].Date);
                }
            }
            console.log(labels);
            for (i = 0; i < obj.length; i++) {
                if (locations.indexOf(obj[i].Agent) === -1) {
                    locations.push(obj[i].Agent);
                }
            }
            console.log(locations);
            locations.forEach(function (location) {
                obj.forEach(function (report) {
                    if (report.Agent === location) {
                        oos.push(report.IntegralSum);
                    }
                })
            });
            console.log(oos);
            for (j = 0; j < obj.length; j++) {
                datasets.push({
                    label: obj[j].Agent,
                    fillColor: "rgba(" + (j * 5) * 2 + "," + (j + 5) * 3 + "," + (j + 15) * 2 + ",0.2)",
                    strokeColor: "rgba(" + (j * 5) * 2 + "," + (j + 5) * 3 + "," + (j + 15) * 2 + ",1)",
                    pointColor: "rgba(" + (j * 5) * 2 + "," + (j + 5) * 3 + "," + (j + 15) * 2 + ",1)",
                    pointStrokeColor: "#fff",
                    pointHighlightFill: "#fff",
                    pointHighlightStroke: "rgba(" + (j * 5) * 2 + "," + (j + 5) * 3 + "," + (j + 15) * 2 + ",1)",
                    data: [oos[j], oos[j + 1], oos[j + 2]]
                });
                var $span = $("div[name='game']").append("<span style='margin-left: 10px; font-weight: 500;'></span>");
                $span.append("<i class='fa fa-square' style='color: " + "rgba(" + (j * 5) * 2 + ", " + (j + 5) * 2 + ", " + (j + 15) * 2 + ", 1)" + "; font-size: 20px; padding-right: 5px; vertical-align: middle; margin-top: -3px;'></i>" + obj[j].Agent);
                j += 3;
            }
            var lineChartData = {
                labels: labels,
                datasets: datasets
            };
            var ctx = document.getElementById("leavechart").getContext("2d");
            window.myLine = new Chart(ctx).Line(lineChartData, {
                responsive: false,
                bezierCurve: false
            });
        }
    });

}
function GetReportForm() {
    $("span[name='report']").on("click", function () {
        //window.location.href = "/Revenue/ReportForm";
        $("a[data-id='14']").click(function () {
            alert("ok");
        })
    })
}

