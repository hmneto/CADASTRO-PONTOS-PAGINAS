async function clienteEditaInteracao(id) {
    document.getElementById('salvar').hidden = true
    document.getElementById('editar').hidden = false
    const detalhesCliente = await httpGet("/Cliente/Detalhes?id=" + id)
    document.getElementById('idCliente').value = detalhesCliente.idCliente
    document.getElementById('nomeCliente').value = detalhesCliente.nomeCliente
}

async function clienteEdita() {
    const cliente = {
        "idCliente": document.getElementById('idCliente').value,
        "nomeCliente": document.getElementById('nomeCliente').value

    }

    await httpPut("/Cliente/Edita", cliente).then(x => openView("listaCliente"));
}