
var GetDataForChart = function (drawChart) {
    var xSeries;
    var ySeries;

    var init = function () {
        $("td > button").click(function () {
            var dateFrom = $("#rangeFrom").val();
            var dateTo = $("#rangeTo").val();
            var selectedCategoryName = $(this).attr("js-graph-category");
            getChart(dateFrom, dateTo, selectedCategoryName);
        });
        getChart(null, null, "");
    };

    var getChart = function (dateFrom, dateTo, selectedCategory) {
        $.ajax({
            url: "/api/record/GetPieChart",
            method: "GET",
            dataType: "json",
            data: {
                categoryName: selectedCategory,
                dateFromParam: dateFrom,
                dateToParam: dateTo
            },
        })
            .done(function (response) {
                var title = 'Udział poszczególnych podkategorii';
                if (selectedCategory === "" || selectedCategory == undefined) {
                    title = 'Udział poszczególnych kategorii';
                }
                drawChart.drawPieChart(response, title);
            })
            .fail(function () {
                alert("Nie udało pobrać się wykresu");
            });
    };
    return {
        init: init
    };
}(DrawChart);