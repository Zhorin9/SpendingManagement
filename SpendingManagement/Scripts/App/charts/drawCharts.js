
var DrawChart = function () {

    var drawPieChart = function (chartObject, title) {

        var chart = {
            plotBackgroundColor: null,
            plotBorderWidth: null,
            plotShadow: false
        };
        var title = {
            text: title,
        };
        var plotOptions = {
            pie: {
                allowPointSelect: true,
                cursor: 'pointer',
            }
        };
        var series = [{
            type: 'pie',
            name: 'Wartość',
            data: []
        }];

        for (var item in chartObject) {
            series[0].data.push([item, chartObject[item]]);
        }

        var json = {};
        json.chart = chart;
        json.title = title;
        json.series = series;
        json.plotOptions = plotOptions;
        Highcharts.setOptions({
            colors: ['#14b068', '#49b355', '#6bb541', '#89b52b', '#a6b411', '#c4b200', '#e2ad00', '#ffa600', '#ffa600']
        });
        $('#pieChart').empty().highcharts(json);
    }

    var drawLineChart = function (chartObject, title, selectedCategory) {
        var xSerie = Object.values(chartObject[0]);
        var yValues = Object.values(chartObject[1]);

        if (selectedCategory == undefined || selectedCategory === "") {
            selectedCategory = "Łącznie";
        }
        var title = {
            text: title
        };

        var xAxis = {
            categories: xSerie,
            labels: {
                enabled: false,
            }
        };

        var legend = {
            enabled: false
        };

        var yAxis = {
            title: 'Wartość [zł]',
            min: 0,
        };

        var tooltip = {
            valueSuffix: ' zł'
        }

        var series = [{
            name: selectedCategory,
            data: yValues,
            color: '#14b068'
        },
        ];

        var json = {};
        json.series = series;
        json.title = title;
        json.xAxis = xAxis;
        json.yAxis = yAxis;
        json.tooltip = tooltip;
        json.legend = legend;
        $('#lineChart').empty().highcharts(json);
    }

    return {
        drawPieChart: drawPieChart,
        drawLineChart: drawLineChart
    }
}();