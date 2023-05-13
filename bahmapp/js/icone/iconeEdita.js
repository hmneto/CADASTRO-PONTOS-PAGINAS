async function iconeEditaInteracao(id) {
    carregaAcao()

    document.getElementById('salvarIcone').hidden = true
    document.getElementById('editarIcone').hidden = false
    const detalhesIcone = await httpGet("/Icone/Detalhes?id=" + id)
    document.getElementById('idIcone').value = detalhesIcone.idIcone
    document.getElementById('nomeIcone').value = detalhesIcone.nomeIcone
    document.getElementById('linkIcone').value = detalhesIcone.linkIcone
    document.getElementById('acaoIcone').value = detalhesIcone.acaoIcone
}

async function iconeEdita() {
    const icone = {
        "idIcone": document.getElementById('idIcone').value,
        "nomeIcone": document.getElementById('nomeIcone').value,
        "linkIcone": document.getElementById('linkIcone').value,
        "acaoIcone": document.getElementById('acaoIcone').value

    }
    await httpPut("/Icone/Edita", icone).then(x => openViewTable("listaIcone"));
}