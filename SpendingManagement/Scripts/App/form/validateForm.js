$(function () {
    $('#Name').on("blur", function () {
        if (this.value == "") {
            var $NameValidation = $(this).next();
            $NameValidation.text("Wprowadź nazwę rekordu");
            $NameValidation.css('display', 'block');
        }
        else {
            $(this).next().text("");
        }
    });
    $('#Date').on("blur", function () {
        if (this.value == "") {
            var $DateValidation = $(this).next();
            $DateValidation.text("Wprowadź datę zdarzenia");
            $DateValidation.css('display', 'block');
        }
        else {
            $(this).next().text("");
        }
    });
    $('#Charge').on("blur", function () {
        if (this.value == "") {
            var $ChargeValidation = $(this).next();
            $ChargeValidation.text("Wprowadź kwotę");
            $ChargeValidation.css('display', 'block');
        }
        else {
            if (validateCharge(this.value)) {
                $(this).next().text("");
            }
            else {
                var $ChargeValidation = $(this).next();
                if (this.value == 0) {
                    $ChargeValidation.text("Kwota powinna być większa od zera");
                }
                else {
                    $ChargeValidation.text("Wprowadź kwotę w poprawnym formacie");
                }
                $ChargeValidation.css('display', 'block');
            }
        }
    });
});
function validateCharge(charge) {
    var regex = /^[1-9]\d*(((.\d{3}){1})?(\,\d{0,2})?)$/;
    return regex.test(charge);
}