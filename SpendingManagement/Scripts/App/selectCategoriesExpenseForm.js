var categoryWindow = document.getElementById('categoryWindow');
var categoryTitle = document.getElementById('categoryTitle');
var categoryList = document.getElementById('categoryList');
var closeCategoryList = document.getElementById('closeCategory');

var openCategoryWindow = document.getElementById('categoryButton');
var choosedCategory = '';
var avaiableCategories = [
    {
        name: 'Jedzenie',
        subCategories: ['Bar', 'Sklep', 'Kanapki', 'Inne'],
    }, {
        name: 'Osobiste',
        subCategories: ['Ubranie', 'Prezent', 'Kosmetyki','Fryzjer', 'Inne'],
    }, {
        name: 'Alkohol',
        subCategories: ['Sklep', 'Bar'],
    }, {
        name: 'Dom',
        subCategories: ['Sprzęt', 'Środki czystości', 'Inne'],
    }, {
        name: 'Opłaty',
        subCategories: ['Mieszkanie', 'Telefon', 'Internet', 'Inne'],
    }, {
        name: 'Przejazdy',
        subCategories: ['Powrót do domu', 'Miesięczny', 'Inne'],
    }, {
        name: 'Inne',
        subCategories: ['Książka', 'Inne'],
    }, {
        name: 'Rozrywka',
        subCategories: ['Koncerty', 'Kino','StandUp', 'Sporty', 'Inne'],
    }, {
        name: 'Hobby',
        subCategories: ['Terrarystyka', 'Elektronika', 'Grawerowanie'],
    }];

function checkCategories(event) {
    var id = event.target.id;
    if (id.includes('categoryReturn')) {
        var displayCategory = document.getElementById('Category');
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
        showSubcategoriesList(id[id.length -1]);
    }
}


function showBasicCategoriesList() {
    if (categoryList.hasChildNodes()) {
        categoryList.innerHTML = '';
    }
    for (var i = 0; i < avaiableCategories.length; i++) {
        var category = document.createElement('li');
        category.textContent = avaiableCategories[i].name;
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

    for (var i = 0; i < avaiableCategories[id].subCategories.length; i++) {
        category = document.createElement('li');
        category.textContent = avaiableCategories[id].subCategories[i];
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