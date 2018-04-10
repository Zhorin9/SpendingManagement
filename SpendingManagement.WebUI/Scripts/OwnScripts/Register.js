var password = document.getElementById("password");
var confirmPassword = document.getElementById("confirmPassword");
var username = document.getElementById("username");

confirmPassword.addEventListener("blur", comparePasswords);
password.addEventListener("blur", function (e) {
    chceckInput(e);
});
username.addEventListener("blur", function (e) {
    chceckInput(e);
});

function chceckInput(e) {
    var input = e.target;
    if (input.value.length < 6) {
        if (input.id == "password") {
            input.nextElementSibling.textContent = "Hasło jest za krótkie. Minimum 6 znaków";
        }
        else {
            input.nextElementSibling.textContent = "Nazwa użytkownika jest za krótka. Minimum 6 znaków"
        }
        $(input).addClass("input-validation-error");
    }
    else {
        input.nextElementSibling.textContent = "";
        $(input).removeClass("input-validation-error");
    }
}
function comparePasswords() {
    var text = password.value;
    if (confirmPassword.value != text) {
        confirmPassword.nextElementSibling.textContent = "Hasła się różnią";
        $(confirmPassword).addClass("input-validation-error");
    }
    else {
        confirmPassword.nextElementSibling.textContent = "";
        $(confirmPassword).removeClass("input-validation-error");
    }
}
function validateEmail(email) {
    var filter = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;
    return filter.test(email);
}

$(function () {
    var $userInput = $('#username');
    var $emailInput = $('#email');
    $userInput.on("blur", function () {
        if (this.value.length < 6) {
            $userInput.addClass("input-validation-error");
            $userInput.next().text("Nazwa użytkownika jest za krótka, miminum 6 znaków");
        }
        else {
            $.ajax({
                type: 'GET',
                url: '/Account/IsLoginExist',
                dataType: 'text',
                data: {
                    'login': this.value,
                },
                success: function (data) {
                    if (data == "True") {
                        $userInput.next().text("");
                        $userInput.removeClass("input-validation-error");
                    }
                    else {
                        $userInput.next().text("Nazwa użytkownika jest zajęta, spróbuj innej");
                        $userInput.addClass("input-validation-error");
                    }
                }
            })
        }
    });
    $emailInput.on('blur', function () {
        if (validateEmail(this.value)) {
            $.ajax({
                type: 'GET',
                url: '/Account/IsEmailExist',
                dataType: 'text',
                data: {
                    'email': this.value,
                },
                success: function (data) {
                    if (data == "True") {
                        $emailInput.next().text("");
                        $emailInput.removeClass("input-validation-error");
                    }
                    else {
                        $emailInput.next().text("Email jest już używany");
                        $emailInput.addClass("input-validation-error");
                    }
                }
            })
        }
        else {
            this.nextElementSibling.textContent = "Błędy mail";
            $emailInput.addClass("input-validation-error");
        }
    }); 
});