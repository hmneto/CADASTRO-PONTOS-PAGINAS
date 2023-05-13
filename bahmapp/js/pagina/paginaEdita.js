async function paginaEditaInteracao(id) {
    document.getElementById('salvar').hidden = true
    document.getElementById('editar').hidden = false
    const detalhesPagina = await httpGet("/pagina/Pagina?PaginaId=" + id)

    for (let i = 0; i < detalhesPagina.listContatoDto.length; i++) {
        montaPaginaContato(detalhesPagina.listContatoDto[i])
    }

    for (let i = 0; i < detalhesPagina.listSiteDto.length; i++) {
        montaPaginaSite(detalhesPagina.listSiteDto[i])
    }

    document.getElementById('idPagina').value = detalhesPagina.idPagina
    document.getElementById('nomePagina').value = detalhesPagina.nomePagina
    document.getElementById('enderecoPagina').value = detalhesPagina.enderecoPagina
    document.getElementById('concessionaria').value = detalhesPagina.concessionaria.nomeConcessionaria
    document.getElementById('idConcessionaria').value = detalhesPagina.concessionaria.idConcessionaria
}

async function paginaEdita() {

    const collection = document.getElementsByClassName("contatoSelecionadoClass")
    const ListContatoDto = []
    for (let i = 0; i < collection.length; i++) {
        ListContatoDto.push({ IdContato: collection[i].value })
    }

    const collection0 = document.getElementsByClassName("siteSelecionadoClass")
    const ListSiteDto = []
    for (let i = 0; i < collection0.length; i++) {
        ListSiteDto.push({ IdSite: collection0[i].value })
    }

    const pagina = {
        "IdPagina": document.getElementById('idPagina').value,
        "NomePagina": document.getElementById('nomePagina').value,
        "EnderecoPagina": document.getElementById('enderecoPagina').value,
        "ConcessionariaId": document.getElementById('idConcessionaria').value,
        ListContatoDto,
        ListSiteDto

    }

    await httpPut("/Pagina/Edita", pagina).then(x => {

        // if (x) {
            openView("listaPagina", true)
        // } else {
        //     alert("erro")
        // }
    });
}


