async function clienteNovo() {
    const cliente = {
        "nomeCliente": document.getElementById('nomeCliente').value
    }

    await httpPost("/Cliente/Novo", cliente).then(x=>openView("listaCliente"));
}

