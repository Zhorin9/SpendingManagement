var CategoriesForm = function () {
    var categoriesObject;
    var categoriesContainer = $("#categoryList");
    var initCategoriesList = function () {
        getCategoriesObject();
        $("#openCategoryWindow").click(function () {
            preapareCategoriesView();
            $("#categoryWindow").show();
        });
        $("#closeCategory").click(function () {
            categoriesContainer.empty();
            $("#categoryWindow").hide();
        });
        categoriesContainer.click(function (event) {
            checkCategories(event);
        })
    }

    var preapareCategoriesView = function () {
        $(categoriesContainer).empty();
        for (var i = 0; i < Object.keys(categoriesObject).length; i++) {
            var categoryItem = $("<li/>")
                .text(Object.keys(categoriesObject)[i])
                .addClass("category-item")
                .attr("id", i)
                .appendTo(categoriesContainer);
        }
        $("#categoryTitle").text('Wybierz kategorię');
    }

    var preapareSubcategoriesView = function (id) {
        $(categoriesContainer).empty();
        var categoryItem = $("<li/>")
            .text("Powrót")
            .addClass("category-item")
            .attr("id", "categoryReturn")
            .appendTo(categoriesContainer);
        for (var i = 0; i < Object.entries(categoriesObject)[id][1].length; i++) {
            var categoryItem = $("<li/>")
                .text(Object.entries(categoriesObject)[id][1][i])
                .addClass("category-item")
                .attr("id", 'subcategory' + i)
                .appendTo(categoriesContainer);
        }
        $("#categoryTitle").text('Wybierz podkategorię');
    }

    var checkCategories = function (event) {
        var id = event.target.id;
        if (id.includes('categoryReturn')) {
            $("Category").text();
            preapareCategoriesView();
        }
        else if (id.includes('sub')) {
            $("#Subcategory").text(event.target.innerText);
            closeWindow();
        }
        else {
            $("#Category").text(event.target.innerText);
            preapareSubcategoriesView(id[id.length - 1]);
        }
    }

    var closeWindow = function () {
        categoriesContainer.empty();
        $("#categoryWindow").hide();
    }

    var getCategoriesObject = function () {
        $.ajax({
            url: "/api/record/PopulateCategoriesDicitonary",
            method: "GET",
            dataType: "json"
        })
            .done(function (response) {
                categoriesObject = jQuery.parseJSON(response);
            })
            .fail(function () {
                alert("Nie udało się");
            });
    }
    return {
        init: initCategoriesList
    };
}();