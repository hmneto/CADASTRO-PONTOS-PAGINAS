async function listaSiteInteracao() {
  const listaSites = await httpGet("/Site/ListaTodos")
  // console.log(listaSites)
  const tabelaListaSites = document.getElementById('tabelaListaSites')

  const listaSiteSelecionado = document.getElementById('listaSiteSelecionado')
  // listaSiteSelecionado.appendChild(document.createElement('br'))

  listaSites.forEach(element => {
    const tr = document.createElement('tr')
    const td1 = document.createElement('td')
    const td2 = document.createElement('td')
    const td3 = document.createElement('td')

    td2.style="word-wrap: break-word;min-width: 160px;max-width: 400px;"

    td1.innerHTML = element.nomeSite

    td2.innerHTML = element.linkSite
    const btnEditar = document.createElement('button')
    btnEditar.innerHTML = "EDITAR"


    const btnUsar = document.createElement('button')
    btnUsar.innerHTML = "USAR"


    btnUsar.addEventListener('click', async function () {

      montaPaginaSite(element)


    })

    btnEditar.addEventListener('click', async function () {
      openViewTable('site',true, element.idSite)
    })

    td3.appendChild(btnEditar)

    td3.appendChild(btnUsar)



    tr.appendChild(td1)
    tr.appendChild(td2)
    tr.appendChild(td3)

    tabelaListaSites.appendChild(tr)
  });
}

