async function pontoNovo() {
    const ponto = {
        NomePonto: document.getElementById('nomePonto').value,
        LatitudePonto: document.getElementById('latitude').value,
        LongitudePonto: document.getElementById('longitude').value,
        IconeId: document.getElementById("idIcone").value,
        PaginaId: document.getElementById("idPagina").value
    }

    await httpPost("/Ponto/Novo", ponto).then(x => {
        if (x) {
            openView("mapa");
        } else {
            alert("erro")
        }
    })
}



async function pontoNovoInteracao() {
    fillInputsLatLng();
}