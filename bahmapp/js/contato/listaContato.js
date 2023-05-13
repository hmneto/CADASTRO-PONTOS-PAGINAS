async function listaContatoInteracao() {
  const listaContatos = await httpGet("/Contato/ListaTodos")
  const tabelaListaContatos = document.getElementById('tabelaListaContatos')

  const listaContatoSelecionado = document.getElementById('listaContatoSelecionado')
  // listaContatoSelecionado.appendChild(document.createElement('br'))

  listaContatos.forEach(element => {
    const tr = document.createElement('tr')
    const td1 = document.createElement('td')
    const td2 = document.createElement('td')

    td1.innerHTML = element.infoContato

    const btnEditar = document.createElement('button')
    btnEditar.innerHTML = "EDITAR"


    const btnUsar = document.createElement('button')
    btnUsar.innerHTML = "USAR"


    btnUsar.addEventListener('click', async function () {

      montaPaginaContato(element)


    })

    btnEditar.addEventListener('click', async function () {
      openViewTable('contato',true, element.idContato)
    })

    td2.appendChild(btnEditar)

    td2.appendChild(btnUsar)

    tr.appendChild(td1)
    tr.appendChild(td2)

    tabelaListaContatos.appendChild(tr)
  });
}

