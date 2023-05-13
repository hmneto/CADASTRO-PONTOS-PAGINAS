async function paginaNovo() {

    const collection = document.getElementsByClassName("contatoSelecionadoClass")
    const ListContatoDto = []
    for (let i = 0; i < collection.length; i++) {
        ListContatoDto.push({IdContato:collection[i].value})
    }



    const collection0 = document.getElementsByClassName("siteSelecionadoClass")
    const ListSiteDto = []
    for (let i = 0; i < collection0.length; i++) {
        console.log(collection0[i].value)
        ListSiteDto.push({IdSite:collection0[i].value})
    }

    console.log(ListSiteDto)
    const pagina = {
        "NomePagina": document.getElementById('nomePagina').value,
        "EnderecoPagina": document.getElementById('enderecoPagina').value,
        "ConcessionariaId": document.getElementById('idConcessionaria').value,
        ListContatoDto,
        ListSiteDto
    }

    await httpPost("/Pagina/Novo", pagina).then(x=>{
        if(x){
            openView("listaPagina", true)
        }else{
            alert("erro")
        }
    });
}
