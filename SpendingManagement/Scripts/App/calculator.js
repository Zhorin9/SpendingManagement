var currentValue = 0;
var earlierValue = 0;
var currentOperator = '';
var earlierNumberTextBox = document.getElementById("displayRowOne");
var resultTextBox = document.getElementById("displayRowTwo");
var numberTextBox = document.getElementById("displayRowThree");
var el = document.getElementById('buttons');                //Button click event


if (el.addEventListener) {
    el.addEventListener('click', function (event) {
        checkButton(event);
    });
}
else {
    el.attachEvent('onclick', function (event) {
        checkButton(event);
    });
}

document.addEventListener('keydown', function (event) {
    switch (event.keyCode) {                            //ancii code
        case 13:                                        //numpad enter
            calculateResult();
            break;
        //Numbers 0-1 keyboard  and numpad
        case 48:
        case 96:
            insertSign(0);
            break;
        case 49:
        case 97:
            insertSign(1);
            break;
        case 50:
        case 98:
            insertSign(2);
            break;
        case 51:
        case 99:
            insertSign(3);
            break;
        case 52:
        case 100:
            insertSign(4);
            break;
        case 53:
        case 101:
            insertSign(5);
            break;
        case 54:
        case 102:
            insertSign(6);
            break;
        case 55:
        case 103:
            insertSign(7);
            break;
        case 56:
        case 104:
            insertSign(8);
            break;
        case 57:
        case 105:
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
})  //Keydown event
function checkButton(event) {
    var targetValue = event.target.value;
    if (targetValue >= 0 && targetValue <= 9 || targetValue == '.') {
        insertSign(targetValue);
    }
    if (targetValue == '+' || targetValue == '-' || targetValue == '*' || targetValue == '/') {
        changeOperator(targetValue);
    }
    switch (targetValue) {
        case '+/-':
            changeSign();
            break;
        case '=':
            calculateResult();
            break;
        case '<':
            deleteLastSign();
            break;
        case 'C':
            clearAll();
            break;
        case 'CE':
            clearTextBox();
            break;
    }
}

function insertSign(sign) {
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
function changeSign() {
            if (numberTextBox.value[0] == '-') {
                numberTextBox.value = numberTextBox.value.slice(1, numberTextBox.value.length);
                currentValue = Math.abs(currentValue);
            }
            else {
                numberTextBox.value = '-' + numberTextBox.value;
                currentValue = currentValue * (-1);
            }
}
function changeOperator(sign) {
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
    if (numberTextBox.value != '' && earlierNumberTextBox.value != '' && currentOperator != '') {
        currentValue = numberTextBox.value;
        numberTextBox.value = doOperation();
        earlierNumberTextBox.value = '';
        earlierValue = 0;
        resultTextBox.value = '';
    } 
}

//Clear windows
function deleteLastSign() {
    numberTextBox.value = numberTextBox.value.slice(0, - 1);
    if (numberTextBox.value[0] == '-' && numberTextBox.value.length == 1) {
        numberTextBox.value = '';
    }
}
function clearAll() {
    earlierNumberTextBox.value = '';
    resultTextBox.value = '';
    numberTextBox.value = '';
}
function clearTextBox() {
    numberTextBox.value = '';
}