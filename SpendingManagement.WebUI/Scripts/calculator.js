var currentValue = 0;
var earlierValue = 0;
var currentOperator = '';

document.addEventListener('keydown', function (event) {
    switch (event.keyCode) {                            //ancii code
        case 13:                                        //numpad enter
            calculateResult();
            break;
        //Numbers 0-1 on keyboard 
        case 48:
            insertSign(0);
            break;
        case 49:
            insertSign(1);
            break;
        case 50:
            insertSign(2);
            break;
        case 51:
            insertSign(3);
            break;
        case 52:
            insertSign(4);
            break;
        case 53:
            insertSign(5);
            break;
        case 54:
            insertSign(6);
            break;
        case 55:
            insertSign(7);
            break;
        case 56:
            insertSign(8);
            break;
        case 57:
            insertSign(9);
            break;

        //NumPad 0-9
        case 96:
            insertSign(0);
            break;
        case 97:
            insertSign(1);
            break;
        case 98:
            insertSign(2);
            break;
        case 99:
            insertSign(3);
            break;
        case 100:
            insertSign(4);
            break;
        case 101:
            insertSign(5);
            break;
        case 102:
            insertSign(6);
            break;
        case 102:
            insertSign(7);
            break;
        case 103:
            insertSign(8);
            break;
        case 104:
            insertSign(9);
            break;

        //Numpad Operators and key ","
        case 106:
            changeOperator('*');
            break;
        case 107:
            changeOperator('+');
            break;
        case 109:
            changeOperator('-');
            break;
        case 110:
            insertSign('.');
            break;
        case 111:
            changeOperator('/');
            break;
 
    }
})
function insertSign(sign) {
            var numberTextBox = document.getElementById("displayRowThree");
            if (numberTextBox.value.length < 20) {
                if (sign != '.') {
                    if (numberTextBox.value.length == 1 && numberTextBox.value[0] == '0') {
                        numberTextBox.value = sign;
                        currentValue = sign;
                    }
                    else {
                        numberTextBox.value += sign;
                        currentValue += sign;
                    }

                }
                else {
                    var i = 0;
                    var findMark;
                    for (i = 0; i < numberTextBox.value.length; i++) {
                        if (numberTextBox.value[i] == '.') {
                            findMark = true;
                            break;
                        }
                    }
                    if (!findMark) {
                        if (numberTextBox.value.length < 1) {
                            numberTextBox.value = '0' + sign;
                        }
                        else {
                            numberTextBox.value += sign;
                        }
                    }
                }
            }
        }
function changeNumberSign() {
            var display = document.getElementById("displayRowThree");
            if (display.value[0] == '-') {
                display.value = display.value.slice(1, display.value.length);
                currentValue = Math.abs(currentValue);
            }
            else {
                display.value = '-' + display.value;
                currentValue = currentValue * (-1);
            }
}
function changeOperator(sign) {
    var numberTextBox = document.getElementById("displayRowThree");
    var earlierNumberTextBox = document.getElementById("displayRowOne");
    if (numberTextBox.value != '' && earlierNumberTextBox.value == '') {
        earlierValue = numberTextBox.value;
        earlierNumberTextBox.value = earlierValue + ' ' + sign;
        numberTextBox.value = '';
    }
    if (numberTextBox.value != '' && earlierNumberTextBox.value != '') {
        earlierNumberTextBox.value = earlierValue + ' ' + currentOperator;
        executeOperation();
    }
    currentOperator = sign;

}
function executeOperation() {
    var result = 0;
    var numberTextBox = document.getElementById("displayRowThree");
    var resultTextBox = document.getElementById("displayRowTwo");
    var earlierNumberTextBox = document.getElementById("displayRowOne");
    currentValue = numberTextBox.value;
    if (earlierValue != '' && numberTextBox.value != '') {
        earlierValue = doOperation();

        earlierNumberTextBox.value += ' ' + numberTextBox.value + ' ' + '= ';
        resultTextBox.value = earlierValue;
        numberTextBox.value = '';
        currentValue = 0;

    }
}
function doOperation() {
    var result = 0;
    switch (currentOperator) {
        case '-':
            result = +earlierValue - (+currentValue);
            break;
        case '+':
            result = +earlierValue + (+currentValue);
            break;
        case '*':
            result = earlierValue * currentValue;
            break;
        case '/':
            if (currentValue == 0) {
                result = 'Nie można wykonać';
            }
            else {
                result = earlierValue / currentValue;
            }
            break;
    }
    return result;

}


function calculateResult() {
    var numberTextBox = document.getElementById("displayRowThree");
    var resultTextBox = document.getElementById("displayRowTwo");
    var earlierNumberTextBox = document.getElementById("displayRowOne");
    if (numberTextBox.value != '' && earlierNumberTextBox.value != '' && currentOperator != '') {
        currentValue = numberTextBox.value;
        numberTextBox.value = doOperation();
        earlierNumberTextBox.value = '';
        earlierValue = 0;
        resultTextBox.value = '';
    } 
}

//Clear windows
function deleteLastMark() {
    var display = document.getElementById("displayRowThree");
    display.value = display.value.slice(0, - 1);
    if (display.value[0] == '-' && display.value.length == 1) {
        display.value = '';
    }
}
function clearAll() {
    document.getElementById("displayRowThree").value = '';
    document.getElementById("displayRowTwo").value = '';
    document.getElementById("displayRowOne").value = '';
}
function clearTextBox() {
    var display = document.getElementById("displayRowThree");
    display.value = '';
}