const nrBucInput = document.getElementById("nrBucati");
const nrBucValue = parseInt(nrBucInput.value, 10);

if (isNaN(nrBucValue) || nrBucValue <= 0) {
    afiseazaEroare(nrBucInput, "Introduceti un numar intreg!");
}
function Redirectionare() {
    if (nrBucValue > stocProdus) {
        alert("Stoc insuficient!");
    } else {
        var url = adaugaUrlTemplate.replace("__PLACEHOLDER__", encodeURIComponent(nrBucValue));
        url = url.replace(/&amp;/g, "&");
        window.location.href = url;
    }
}


function calculeazaPretTotal(input) {
    const pretTotalElement = document.getElementById("pretTotal");
    const pretIntregElement = document.getElementById("pretIntreg");
    const pretDecimalElement = document.getElementById("pretDecimal");
    const adaugaBtn = document.getElementById("adaugaLink");

    const cantitate = input.value;
    const pretUnitar = parseFloat(pretTotalElement.getAttribute("pret").replace(",", "."));

    if (esteNumarIntregValid(cantitate)) {
        const pretTotal = cantitate * pretUnitar;
        const intPart = Math.floor(pretTotal);
        const decPart = Math.round((pretTotal - intPart) * 100);

        pretIntregElement.textContent = intPart;
        if (pretDecimalElement) {
            pretDecimalElement.textContent = decPart !== 0 ? decPart : "";
        }

        input.style.borderColor = "black";
        document.getElementById("wrongInput").style.opacity = 0;
        if (adaugaBtn) adaugaBtn.style.display = "inline";
    } else {
        //afiseazaEroare(input, "Introduceti un numar intreg!");
        if (adaugaBtn) adaugaBtn.style.display = "none";
        document.getElementById("wrongInput").style.opacity = 1;
    }
}

function resetareInput(input) {
    if (!input.value || input.value === "0" || input.value[0] === "0") {
        input.value = "1";
        calculeazaPretTotal(input);
    }
}
function esteNumarIntregValid(valoare) {
    return /^\d+$/.test(valoare); // doar cifre
}

function validareInput(input) {
    const valoare = input.value.trim();
    const eroare = document.getElementById("wrongInput");

    eroare.textContent = "Introduceti un numar intreg!";
    eroare.style.textAlign = "center";

    if (!esteNumarIntregValid(valoare) || valoare == "0" || valoare[0] == "0") {
        input.style.borderColor = "red";
        eroare.style.opacity = 1;
        document.getElementById("actualizeazaStoc").style.display = "none";
    } else {
        input.style.borderColor = "black";
        eroare.style.opacity = 0;
        document.getElementById("actualizeazaStoc").style.display = "inline";
    }
}
/*function afiseazaEroare(input, mesaj) {
    const eroare = document.getElementById("wrongInput");
    eroare.textContent = mesaj;
    eroare.style.opacity = 1;
    eroare.style.textAlign = "center";
    input.style.borderColor = "red";
}*/