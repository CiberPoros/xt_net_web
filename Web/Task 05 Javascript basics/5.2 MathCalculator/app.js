let expressionstring = '3.5 +4*10-5.3 /5 =';

console.log(calcExpression(expressionstring));

function calcExpression(expression) {
    let operators = ['+', '-', '*', '/', '='];
    let expressionWithoutSpaces = removeChars(expression, [' ']);

    let splited = splitString(expressionWithoutSpaces, operators);

    if (splited.length == 0)
        return 0;

    let result = parseFloat(splited[0], 10);

    for (let i = 0, numIndex = 1; i < expressionWithoutSpaces.length; i++) {
        if (operators.includes(expressionWithoutSpaces.charAt(i))) {
            switch (expressionWithoutSpaces.charAt(i)) {
                case '+':
                    result += parseFloat(splited[numIndex], 10);
                    break;
                case '-':
                    result -= parseFloat(splited[numIndex], 10);
                    break;
                case '*':
                    result *= parseFloat(splited[numIndex], 10);
                    break;
                case '/':
                    result /= parseFloat(splited[numIndex], 10);
                    break;
                case '=':
                    return result.toFixed(2);
                default:
                    break;
            }

            numIndex++;
        }
    }

    return result.toFixed(2);
}

function splitString(strToSplit, separatorsString) {
    let splited = [];

    let currentChars = [];
    for (let i = 0; i < strToSplit.length; i++) {
        if (separatorsString.indexOf(strToSplit.charAt(i)) != -1) {
            let str = currentChars.join('');

            if (str.length > 0) {
                splited.push(str);
            }

            currentChars = [];
        } else {
            currentChars.push(strToSplit.charAt(i));
        }
    }

    let str = currentChars.join('');

    if (str.length > 0) {
        splited.push(str);
    }

    return splited;
}

function removeChars(str, charsArray) {
    let result = [];

    for (let i = 0; i < str.length; i++) {
        if (!charsArray.includes(str.charAt(i))) {
            result.push(str.charAt(i));
        }
    }

    return result.join('');
}