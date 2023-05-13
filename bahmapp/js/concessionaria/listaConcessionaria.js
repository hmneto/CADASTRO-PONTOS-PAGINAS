async function listaConcessionariaInteracao() {
  console.log('ok')
  const listaConcessionas = await httpGet("/Concessionaria/ListaTodos")
  const tabelaListaConcessionas = document.getElementById('tabelaListaConcessionarias')

  listaConcessionas.forEach(element => {
    const tr = document.createElement('tr')
    const td1 = document.createElement('td')
    const td2 = document.createElement('td')
    const td3 = document.createElement('td')

    td1.innerHTML = element.nomeConcessionaria
    td2.innerHTML = element.infoConcessionaria

    const btnEditar = document.createElement('button')
    btnEditar.innerHTML = "EDITAR"


    const btnUsar = document.createElement('button')
    btnUsar.innerHTML = "USAR"

    btnEditar.addEventListener('click', async function () {
      openViewTable('concessionaria',true, element.idConcessionaria)
    })


    btnUsar.addEventListener('click', async function () {
      document.getElementById('concessionaria').value = element.nomeConcessionaria
      document.getElementById('idConcessionaria').value = element.idConcessionaria
      document.getElementById('fechaModal').click()
      
    })

    td3.appendChild(btnEditar)
    td3.appendChild(btnUsar)

    tr.appendChild(td1)
    tr.appendChild(td2)
    tr.appendChild(td3)
    tabelaListaConcessionas.appendChild(tr)
  });
}