var currentValue = 0;
var earlierValue = 0;
var currentOperator = '';
function insertMark(sign) {
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
function changeSign() {
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
        earlierValue = calculate();

        earlierNumberTextBox.value += ' ' + numberTextBox.value + ' ' + '= ';
        resultTextBox.value = earlierValue;
        numberTextBox.value = '';
        currentValue = 0;

    }
}
function calculate() {
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
        resultTextBox.value = calculate();
        numberTextBox.value = '';
    } 
}

//Clear windows
function deleteLastMark() {
    var display = document.getElementById("displayRowThree");
    display.value = display.value.slice(0, - 1);
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