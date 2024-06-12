function calculeazaPretTotal(input) {

    if (isNaN(input.value) == false && input.value != ""  && input.value > 0 && input.value[0] != "0" ) {
        var cantitate = parseInt(input.value);
        var pretBucata = parseFloat(document.getElementById("pretTotal").getAttribute("pret").replace(",", "."));

        var pretTotal = (cantitate * pretBucata);
        var pretIntreg = parseInt(pretTotal);
        var pretDecimal = parseInt((pretTotal - Math.floor(pretTotal)) * 100);

        document.getElementById("pretIntreg").textContent = pretIntreg;
        if (pretDecimal != 0) {
            document.getElementById("pretDecimal").textContent = pretDecimal;
        }
        

        input.style.borderColor = "black";
        document.getElementById("wrongInput").style.opacity = 0;
        document.getElementById("adaugaLink").style.display = "inline";
    }
    else if (input.value != "" && isNaN(input.value)) {
        document.getElementById("wrongInput").style.opacity = 1;
        input.style.borderColor = "red";
        document.getElementById("adaugaLink").style.display = "none";
    }
}
function resetareInput(input) {
    if (input.value == "" || input.value == "0" || input.value[0] == "0") {
        input.value = "1";
        calculeazaPretTotal(input);
    }
}
function validareInput(input) {
    var eroare = document.getElementById("wrongInput");
    eroare.textContent = "Introduceti un numar intreg!";
    eroare.style.textAlign = "center";
    if (input.value == "" || input.value == "0" || input.value[0] == "0") {
        input.value = "1";
        eroare.style.opacity = 0;
        input.style.borderColor = "black";
        document.getElementById("actualizeazaStoc").style.display = "inline";
    }
    else
        if (input.value != "" && isNaN(input.value)) {
        eroare.style.opacity = 1;
        input.style.borderColor = "red";
        document.getElementById("actualizeazaStoc").style.display = "none";
    }
    else {
        eroare.style.opacity = 0;
        input.style.borderColor = "black";
        document.getElementById("actualizeazaStoc").style.display = "inline";
    }
}

