document.addEventListener("DOMContentLoaded", function () {
    const adaugaBtn = document.getElementById("adaugaLink");
    const actualizeazaBtn = document.getElementById("actualizeazaStoc");

    // Logica pentru client
    if (isFurnizor === false && adaugaBtn) {
        adaugaBtn.addEventListener("click", function (e) {
            e.preventDefault();
            const nrBucInput = document.getElementById("nrBucati");
            const nrBucValue = parseInt(nrBucInput.value, 10);

            if (isNaN(nrBucValue) || nrBucValue <= 0) {
                afiseazaEroare(nrBucInput, "Introduceti un numar intreg!");
                return;
            }

            if (nrBucValue > stocProdus) {
                alert("Stoc insuficient!");
            } else {
                const url = adaugaUrlTemplate.replace("__PLACEHOLDER__", encodeURIComponent(nrBucValue)).replace("amp;", "");
                window.location.href = url;
            }
        });
    }

    // Logica pentru furnizor
    if (isFurnizor === true && actualizeazaBtn) {
        actualizeazaBtn.addEventListener("click", function (e) {
            e.preventDefault();

            const stocNouInput = document.getElementById("stocNou");
            const stocExistentInput = document.getElementById("stocExistent");

            const stocNou = parseInt(stocNouInput.value, 10);
            const stocExistent = parseInt(stocExistentInput.value, 10);

            if (isNaN(stocNou) || stocNou <= 0 || stocNou <= stocExistent) {
                alert("Stocul nou trebuie sa fie un număr valid și mai mare decât cel existent!");
            } else {
                const url = actualizareStocUrlTemplate.replace("__PLACEHOLDER__", encodeURIComponent(stocNou)).replace("amp;", "");
                window.location.href = url;
            }
        });
    }
});

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
        afiseazaEroare(input, "Introduceti un numar intreg!");
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


function afiseazaEroare(input, mesaj) {
    const eroare = document.getElementById("wrongInput");
    eroare.textContent = mesaj;
    eroare.style.opacity = 1;
    eroare.style.textAlign = "center";
    input.style.borderColor = "red";
}
