const StatusLetters = {
    Default: 0,
    Contains: 1,
    Ok: 2,
    Locked: 3
}

function SetColor(status)
{
   
    if (status == "Contains")
           return "#b59f3b";
              
     else if (status == "Default")
           return "#21262b";
              
     else if (status == "Locked")
           return "#3a3a3c";
             
     else StatusLetters.Ok
           return "#538d4e";             
         
}  


function RotateRow(numberRow, lettersGril, maxColumLength)
{
   let _lettersGril = JSON.parse(lettersGril);  
   let lettesAnimeted = document.querySelectorAll(".element-animated-row-"+numberRow);
   
    for (let i = 0; i < maxColumLength; i++) {
        let index = i;
        const element = lettesAnimeted[i];        
        element.classList.add('rotate-word'); // Agrega la clase que define la animación
        element.style.animationDuration = i + 's'; // Establece la duración de la animación
        element.addEventListener('animationend', () => {   
            element.classList.add("status-" + _lettersGril[index])
            element.removeEventListener('animationend', this);
        }, { once: true });
    }
    
}

function RotateRowWinner(numberRow, maxColumLength) {    
    let lettesAnimeted = document.querySelectorAll(".element-animated-row-" + numberRow);
    for (let i = 0; i < maxColumLength; i++) {
        let index = i;
        const element = lettesAnimeted[i];
        element.classList.add('rotate-word'); // Agrega la clase que define la animación
        element.style.animationDuration = i + 's'; // Establece la duración de la animación
        element.addEventListener('animationend', () => {
            element.classList.add("status-Ok" )
            element.removeEventListener('animationend', this);
        }, { once: true });
    }
}

function ResetStyleGril(numberOfLettersToClearStyle) {
    let lettesAnimeted = document.querySelectorAll(".animation-card")
    console.log(lettesAnimeted)

    for (let i = 0; i < lettesAnimeted.length; i++) {
        let element = lettesAnimeted[i];
        element.classList.remove("status-Default")
        element.classList.remove("status-Locked")
        element.classList.remove("status-Contains")
        element.classList.remove("status-Ok")
        element.classList.remove('rotate-word');
    }
}


function VibrateInvalidWord(numberRow) { 
    let elementAlert = document.querySelector(".alert");
    let rowLetter = document.getElementById("row-grid-" + numberRow); 
    rowLetter.classList.add("invalid-word");
    rowLetter.addEventListener("animationend", () => {
        rowLetter.classList.remove("invalid-word")
       // rowLetter.removeEventListener('animationend', this);
    })
    ShowAlert(elementAlert);
    setTimeout(() => {
        HideAlert(elementAlert);
    }, 1500)
}

function ShowAlert(alert) { 
    alert.style.display = 'flex';
}
function HideAlert(alert) {  
    alert.style.display = 'none';
}
