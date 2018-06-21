var test;
var avaiableCategories;
function wow() {
    $.ajax({
        url: "/api/record",
        method: "GET",
       dataType: "json" 
    })
        .done(function (response) {
            avaiableCategories = jQuery.parseJSON(response);
        })    
        .fail(function () {
            alert("Nie udało się");
        });
};
var categoryWindow = document.getElementById('categoryWindow');
    var categoryTitle = document.getElementById('categoryTitle');
    var categoryList = document.getElementById('categoryList');
    var closeCategoryList = document.getElementById('closeCategory');

    var openCategoryWindow = document.getElementById('categoryButton');
    var choosedCategory = '';
    
    function checkCategories(event) {
        var id = event.target.id;
        if (id.includes('categoryReturn')) {
            displayCategory = document.getElementById('Category');
            displayCategory.textContent = '';
            showBasicCategoriesList();
        }
        else if (id.includes('sub')) {
            var displaySubcategory = document.getElementById('Subcategory');
            displaySubcategory.textContent = event.target.innerText;
            displaySubcategory.hidden = '';
            closeCategoryWindow();
        }
        else {
            var displayCategory = document.getElementById('Category');
            displayCategory.textContent = event.target.innerText;
            displayCategory.hidden = '';
            showSubcategoriesList(id[id.length - 1]);
        }
    }


    function showBasicCategoriesList() {
        if (categoryList.hasChildNodes()) {
            categoryList.innerHTML = '';
        }
        for (var i = 0; i < Object.keys(avaiableCategories).length; i++) {
            var category = document.createElement('li');
            category.textContent = Object.keys(avaiableCategories)[i];
            category.className = 'category-item';
            category.id = 'category' + i;
            categoryList.appendChild(category);
        }
        categoryTitle.textContent = 'Wybierz kategorię';
    }
    function showSubcategoriesList(id) {
        if (categoryList.hasChildNodes()) {
            categoryList.innerHTML = '';
        }
        var category = document.createElement('li');
        category.textContent = 'Powrót';
        category.className = 'category-item';
        category.id = 'categoryReturn';
        categoryList.appendChild(category);

        for (var i = 0; i < Object.entries(avaiableCategories)[id][1].length; i++) {
            category = document.createElement('li');
            category.textContent = Object.entries(avaiableCategories)[id][1][i];
            category.className = 'category-item';
            category.id = 'subCategory' + i;
            categoryList.appendChild(category);
        }
        categoryTitle.textContent = 'Wybierz podkategorię';
    }
    function closeCategoryWindow() {
        categoryList.innerHTML = '';
        categoryWindow.style.display = 'none';
    }

    openCategoryWindow.addEventListener('click', function () {
        showBasicCategoriesList();
        categoryWindow.style.display = "block";
    });
    categoryList.addEventListener('click', function (event) {
        checkCategories(event);
    });
    closeCategoryList.addEventListener('click', closeCategoryWindow);