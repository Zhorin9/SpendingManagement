$(document).ready(function () {
    AccountController.init();
});
var AccountController = function () {
    var init = function () {
        $(".js-delete-user").click(checkBox);
    };
    var checkBox = function () {
        bootbox.confirm("<h3>Czy na pewno chcesz usunąć konto?</h1><form id='deleteUser' action='/Account/Delete' method='POST'><div class='form-group'><label class='ml-4'>Hasło:  </label><input class='form-control' type='password' name='password' value=''/></div></form>", function (result) {
            if (result)
                $('#deleteUser').submit();
        });
    };

    return {
        init: init
    };
}();

