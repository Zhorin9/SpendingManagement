$(document).ready(function () {
    AccountController.init();
});
var AccountController = function () {
    var init = function () {
        $(".js-delete-user").click(checkBox);
    };
    var checkBox = function () {
        bootbox.confirm({
            message: "Czy na pewno chcesz usunąć konto? Stracisz wszystkie dane!!!",
            buttons: {
                confirm: {
                    label: 'Tak',
                    className: 'btn-success'
                },
                cancel: {
                    label: 'Nie',
                    className: 'btn-danger'
                }
            },
            callback: function (result) {
                if (result) {
                    bootbox.confirm("<h3>Podaj hasło:</h1><form id='deleteUser' action='/Account/Delete' method='POST'><div class='form-group'><input class='form-control' type='password' name='password' value=''/></div></form>", function (result) {
                        if (result)
                            $('#deleteUser').submit();
                    });
                }
            }
        });
    };
    return {
        init: init
    };
}();

