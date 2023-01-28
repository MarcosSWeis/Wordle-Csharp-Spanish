// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


function SetCharacterInGril(character,status,numGril=0,type) {

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
    rowGril[posisionAlimpiar-1].children[0].textContent = "";
    }
}

const StatusLetters={
   Default :0,
   Contains :1,
   Ok:2,
   Blocked:3
}