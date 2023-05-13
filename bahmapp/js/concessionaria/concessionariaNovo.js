async function concessionariaNovo() {
    const concessionaria = {
        "nomeConcessionaria": document.getElementById('nomeConcessionaria').value,
        "infoConcessionaria": document.getElementById('infoConcessionaria').value
    }

    await httpPost("/Concessionaria/Novo", concessionaria).then(x=>openViewTable("listaConcessionaria"));
}

