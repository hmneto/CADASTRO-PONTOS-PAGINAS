async function contatoNovo() {
    const contato = {
        "infoContato": document.getElementById('infoContato').value
    }

    await httpPost("/Contato/Novo", contato).then(x=>openViewTable("listaContato"));
}

