async function pontoEditaInteracao() {
    document.getElementById('salvar').hidden = true
    document.getElementById('editar').hidden = false
    const ponto = await httpGet("/Ponto/Detalhes?id=" + dadosPonto.idPonto);
    fillInputsLatLng();

    document.getElementById('nomePonto').value = ponto.nomePonto
    document.getElementById('idPonto').value = ponto.idPonto
    document.getElementById('pagina').value = ponto.pagina.nomePagina
    document.getElementById('idPagina').value = ponto.pagina.idPagina
    document.getElementById('icone').value = ponto.icone.nomeIcone
    document.getElementById('idIcone').value = ponto.icone.idIcone

}

async function pontoEdita() {
    const ponto = {
        IdPonto: document.getElementById('idPonto').value,
        NomePonto: document.getElementById('nomePonto').value,
        LatitudePonto: document.getElementById('latitude').value,
        LongitudePonto: document.getElementById('longitude').value,
        IconeId: document.getElementById("idIcone").value,
        PaginaId: document.getElementById("idPagina").value
    }

    await httpPut("/Ponto/Edita", ponto).then(x => openView("mapa"));
}