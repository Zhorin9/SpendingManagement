var getSubcategories = function () {
    var xSeries;
    var ySeries;

    var init = function () {
        
    }

    var getChart = function () {
        $.ajax({
            url: "api/record/GetChart",
            method: "GET",
            dataType: "json",

        });
    }

    return {
        init: init
    };
}();