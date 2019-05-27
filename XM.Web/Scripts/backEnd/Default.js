$(function () {
    GetSalaryChart();
    GetLeaveChart();
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
            startMonth: date.getMonth() - 1,
            endMonth: date.getMonth() + 1,
            agent_AN: $("#agent option:selected").val()
        },
        success: function (data) {
            obj = data;
            for (i = 0; i < obj.month.length; i++) {
                if (labels.indexOf(obj.month[i].date) === -1) {
                    labels.push(obj.month[i].date);
                }
            }
            if (obj.month.length > 3) {
                var $select = $("select[name='agent']").html("");
                for (i = 0; i < obj.month.length; i++) {
                    if (locations.indexOf(obj.month[i].agent_AN) === -1) {
                        locations.push(obj.month[i].agent_AN);
                        $select.append("<option value=" + obj.month[i].agent_AN + ">" + obj.month[i].agent_AN + "</option>");
                    }
                }
            } else {
                for (i = 0; i < obj.month.length; i++) {
                    if (locations.indexOf(obj.month[i].agent_AN) === -1) {
                        locations.push(obj.month[i].agent_AN);
                        //$select.append("<option value=" + obj.month[i].agent_AN + ">" + obj.month[i].agent_AN + "</option>");
                    }
                }
            }
            locations.forEach(function (location) {
                obj.month.forEach(function (report) {
                    if (report.agent_AN === location) {
                        oos.push(report.total);
                    }
                })
            });
            for (i = 0; i < obj.month.length; i++) {
                datasets.push([oos[i], oos[i + 1], oos[i + 2]]);
                if (obj.month.length > 3) {
                    i += 3;
                }
            }
            var doughnutData = {
                labels: labels,
                datasets: [
                    {
                        label: locations[0],
                        fillColor: "rgba(220,220,220,0.5)",
                        strokeColor: "rgba(220,220,220,0.8)",
                        highlightFill: "rgba(220,220,220,0.75)",
                        highlightStroke: "rgba(220,220,220,1)",
                        data: [oos[0], oos[1], oos[2]]
                    },
                    {
                        label: obj.game.agent_AN,
                        fillColor: "rgba(151,187,205,0.5)",
                        strokeColor: "rgba(151,187,205,0.8)",
                        highlightFill: "rgba(151,187,205,0.75)",
                        highlightStroke: "rgba(151,187,205,1)",
                        data: [0, 0, obj.game.total]
                    }
                ]
            };
            var ctx = document.getElementById("leavechart").getContext("2d");
            window.myDoughnut = new Chart(ctx).Bar(doughnutData, { responsive: true });
        }
    });
    //$("#agent").change(function () {
    //    console.log($("#agent option:selected").val());
    //    GetLeaveChart();
    //});
}

