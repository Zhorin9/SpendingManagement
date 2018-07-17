
var GetDataForChart = function (drawChart) {

    var init = function () {
        $("td > button").click(function () {
            var selectedCategoryName = $(this).attr("js-graph-category");
            getChartsData(selectedCategoryName);
        });
        getChartsData();
    };
    var getChartsData = function (selectedCategory) {
        if (selectedCategory == undefined)
            selectedCategory = "";
        getPieChart(selectedCategory);
        getLineChart(selectedCategory);
    }

    var getPieChart = function (selectedCategory) {
        $.ajax({
            url: "/api/record/GetPieChart",
            method: "GET",
            dataType: "json",
            data: {
                categoryName: selectedCategory,
                dateFromParam: $("#rangeFrom").val(),
                dateToParam: $("#rangeTo").val()
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
    var getLineChart = function (selectedCategory) {
        $.ajax({
            url: "/api/record/GetLineChart",
            method: "GET",
            dataType: "json",
            data: {
                categoryName: selectedCategory,
                dateFromParam: $("#rangeFrom").val(),
                dateToParam: $("#rangeTo").val()
            },
        })
            .done(function (response) {
                var title = 'Udział poszczególnych podkategorii';
                if (selectedCategory === "" || selectedCategory == undefined) {
                    title = 'Udział poszczególnych kategorii';
                }
                drawChart.drawLineChart(response, title, selectedCategory);
            })
            .fail(function () {
                alert("Nie udało pobrać się wykresu");
            });
    };

    return {
        init: init,
    };

}(DrawChart);