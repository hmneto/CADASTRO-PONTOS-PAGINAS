
async function iconeSalvaInteracao(){
    carregaAcao()
}

async function iconeNovo() {
    const icone = {
        "nomeIcone": document.getElementById('nomeIcone').value,
        "linkIcone": document.getElementById('linkIcone').value,
        "acaoIcone": document.getElementById('acaoIcone').value
    }

    await httpPost("/Icone/Novo", icone).then(x=>openViewTable("listaIcone"));
}
