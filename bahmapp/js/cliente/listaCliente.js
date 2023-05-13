async function listaClienteInteracao() {
  const listaClientes = await httpGet("/Cliente/ListaTodos")
  const tabelaListaClientes = document.getElementById('tabelaListaClientes')

  console.log(listaClientes)

  listaClientes.forEach(element => {
    const tr = document.createElement('tr')
    const td1 = document.createElement('td')
    const td2 = document.createElement('td')

    td1.innerHTML = element.nomeCliente

    const btnEditar = document.createElement('button')
    btnEditar.innerHTML = "EDITAR"

    btnEditar.addEventListener('click', async function () {
      openView('cliente',true, element.idCliente)
    })

    td2.appendChild(btnEditar)

    tr.appendChild(td1)
    tr.appendChild(td2)
    tabelaListaClientes.appendChild(tr)
  });
}