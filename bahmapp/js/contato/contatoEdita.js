async function contatoEditaInteracao(id) {
    document.getElementById('salvarContato').hidden = true
    document.getElementById('editarContato').hidden = false
    const detalhesContato = await httpGet("/Contato/Detalhes?id=" + id)
    document.getElementById('idContato').value = detalhesContato.idContato
    document.getElementById('infoContato').value = detalhesContato.infoContato
}

async function contatoEdita() {
    const contato = {
        "idContato": document.getElementById('idContato').value,
        "infoContato": document.getElementById('infoContato').value

    }

    await httpPut("/Contato/Edita", contato).then(x => openViewTable("listaContato"));
}