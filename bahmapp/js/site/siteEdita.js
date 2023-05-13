async function siteEditaInteracao(id) {
    carregaTipoSite()

    
    document.getElementById('salvarSite').hidden = true
    document.getElementById('editarSite').hidden = false
    const detalhesSite = await httpGet("/Site/Detalhes?id=" + id)
    document.getElementById('idSite').value = detalhesSite.idSite


    document.getElementById('nomeSite').value = detalhesSite.nomeSite
    document.getElementById('linkSite').value = detalhesSite.linkSite
    document.getElementById('tipoSite').value = detalhesSite.tipoSite


}

async function siteEdita() {
    const site = {
        "idSite": document.getElementById('idSite').value,
        "nomeSite": document.getElementById('nomeSite').value,
        "linkSite": document.getElementById('linkSite').value,
        "tipoSite": document.getElementById('tipoSite').value,

    }

    await httpPut("/Site/Edita", site).then(x => openViewTable("listaSite"));
}