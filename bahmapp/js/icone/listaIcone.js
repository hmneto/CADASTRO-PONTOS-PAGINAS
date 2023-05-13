async function listaIconeInteracao() {
  const listaIcones = await httpGet("/Icone/ListaTodos")
  const tabelaListaIcones = document.getElementById('tabelaListaIcones')

  listaIcones.forEach(element => {
    const tr = document.createElement('tr')
    const td1 = document.createElement('td')
    const td2 = document.createElement('td')
    const td3 = document.createElement('td')
    const td4 = document.createElement('td')

    td1.innerHTML = element.nomeIcone
    td2.innerHTML = element.linkIcone
    td3.innerHTML = element.acaoIcone

    const btnEditar = document.createElement('button')
    btnEditar.innerHTML = "EDITAR"


    const btnUsar = document.createElement('button')
    btnUsar.innerHTML = "USAR"


    btnUsar.addEventListener('click', async function () {
      document.getElementById('icone').value = element.nomeIcone
      document.getElementById('idIcone').value = element.idIcone
      document.getElementById('fechaModal').click()
      
    })

    btnEditar.addEventListener('click', async function () {
      openViewTable('icone',true, element.idIcone)
    })

    td4.appendChild(btnEditar)

    td4.appendChild(btnUsar)

    tr.appendChild(td1)
    tr.appendChild(td2)
    tr.appendChild(td3)
    tr.appendChild(td4)

    tabelaListaIcones.appendChild(tr)
  });
}

