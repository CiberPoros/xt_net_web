let str = `У попа была собака???!!!`;
let separators = ' ?!:;,.';

let strings = splitString(str, separators);

let repeatedChars = [];
for (let i = 0; i < strings.length; i++) {
    repeatedChars = repeatedChars.concat(getRepeatedWords(strings[i]));
}

console.log(removeChars(str, repeatedChars));

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

function getRepeatedWords(str) {
    let result = [];
    let chars = new Set();

    for (let i = 0; i < str.length; i++) {
        if (chars.has(str.charAt(i))) {
            if (!result.includes(str.charAt(i))) {
                result.push(str.charAt(i));
            }
        } else {
            chars.add(str.charAt(i));
        }
    }

    return result;
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