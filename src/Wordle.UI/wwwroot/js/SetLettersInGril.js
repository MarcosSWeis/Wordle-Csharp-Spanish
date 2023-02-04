function SetCharacterInGril(character, status, numGril = 0, type) {

    if (!type.split(".").includes("Enter") && !type.split(".").includes("CleanLetter")) {
        const rowGril = document.getElementById("row-grid-" + numGril).children

        console.log("character", character)
        console.log("status", status)
        // const rowGril = document.getElementById("row-grid-" + numGril).children
        console.log("rowGril", rowGril)
        positionEmpti = GetEmpty(rowGril, numGril);
        console.log("type", type.split(".").slice(-1))
        console.log("type", type.split(".").includes("Enter"))
        console.log("type", type.split(".").includes("CleanLetter"))
        console.log("StatusLetters", StatusLetters)
        positionEmpti.textContent = character;
        console.log(positionEmpti);

    }
    if (type.split(".").includes("CleanLetter")) {
        CleanCharacters(numGril);
    }

    if (type.split(".").includes("Enter")) {
        let word = Check(numGril);
        fetch("http://localhost:5249/Wordle/Check",
            {
                method: "POST",
                body: word,
                headers: {
                    'Content-Type': 'application/json'
                    // 'Content-Type': 'application/x-www-form-urlencoded',
                },

            }).then((res) => {
                return res.json();
            }).then((data) => {
                console.log(data)
                //logica para pintar la palabras y bloquear teclas dependieno de las palabras
            })


    }




}
function GetEmpty(rowGril) {

    for (var i = 0; i < rowGril.length; i++) {

        if (rowGril[i].children[0].textContent == "") {
            return rowGril[i].children[0]
        }
    }
    return rowGril[4].children[0]
}

function GetEmptyClear(rowGril) {
    for (var i = 0; i < rowGril.length; i++) {

        if (rowGril[i].children[0].textContent == "") {
            return i
        }
    }
    return rowGril.length

}




function CleanCharacters(numGril) {
    const rowGril = document.getElementById("row-grid-" + numGril).children
    let posisionAlimpiar = GetEmptyClear(rowGril);
    if (posisionAlimpiar != 0) {
        rowGril[posisionAlimpiar - 1].children[0].textContent = "";
    }
}

const StatusLetters = {
    Default: 0,
    Contains: 1,
    Ok: 2,
    Blocked: 3
}


function Check(numGril) {
    const rowGril = document.getElementById("row-grid-" + numGril).children
    let word = "";
    for (var i = 0; i < rowGril.length; i++) {
        word = word + rowGril[i].children[0].textContent
    }
    return word
}