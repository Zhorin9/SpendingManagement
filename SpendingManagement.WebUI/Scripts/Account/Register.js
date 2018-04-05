$(function () {
    var $userInput = $('#userLogin');
    var $userInputMessage = $('#userLoginFeedback');
    var $emailInput = $('#email');
    var $emailInputMessage = $('#emailFeedback');
    $userInput.on("blur", function () {
        if (this.value.length < 6) {
            $userInputMessage.text("Nazwa użytkownika jest za krótka, miminum 6 znaków");
        }
        else {
            $.ajax({
                type: 'GET',
                url: '/Account/IsLoginExist',
                dataType: 'text',
                data: {
                    'login': $userInput.val(),
                },
                success: function (data) {
                    if (data == "True") {
                        $userInputMessage.text("");
                    }
                    else {
                        $userInputMessage.text("Nazwa użytkownika jest zajęta, spróbuj innej");
                    }
                }
            })
            $userInputMessage.text("");
        }
    });
    $emailInput.on('blur', function () {
        if (validateEmail(this.value)) {
            $.ajax({
                type: 'GET',
                url: '/Account/IsEmailExist',
                dataType: 'text',
                data: {
                    'email': $emailInput.val(),
                },
                success: function (data) {
                    if (data == "True") {
                        $emailInputMessage.text("");
                    }
                    else {
                        $emailInputMessage.text("Email jest już używany");
                    }
                }
            })
        }
        else {
            $emailInputMessage.text("Błędy mail");
        }
    });
    $('form').on('submit', function (event) {
        if ($userInputMessage.text()) {
            event.preventDefault();
            $userInputMessage.text("Wprowadź nazwę użytkownika");
        }
        if ($emailInputMessage.text()) {
            event.preventDefault();
            $emailInputMessage.text("Wprowadź email");
        }
    });
});
function validateEmail(email) {
    var filter = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;
    return filter.test(email);
}