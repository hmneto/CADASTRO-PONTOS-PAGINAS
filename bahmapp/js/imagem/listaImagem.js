async function listaImagemInteracao(edita) {
  const listaImagems = await httpGet("/Imagem/ListaTodos")
  console.log(listaImagems)
  const tabelaListaImagems = document.getElementById('tabelaListaImagems')

  const listaImagemSelecionado = document.getElementById('listaImagemSelecionado')
  // listaImagemSelecionado.appendChild(document.createElement('br'))

  listaImagems.forEach(element => {
    const tr = document.createElement('tr')
    const td1 = document.createElement('td')
    const td2 = document.createElement('td')
    const td3 = document.createElement('td')
    const td4 = document.createElement('td')

    td2.style="word-wrap: break-word;min-width: 160px;max-width: 400px;"

    td1.innerHTML = element.nomeImagem

    td2.innerHTML = element.linkImagem
    const btnEditar = document.createElement('button')
    btnEditar.innerHTML = "EDITAR"


    const btnUsar = document.createElement('button')
    btnUsar.innerHTML = "USAR"


    const btnVerImagem = document.createElement('button')
    btnVerImagem.innerHTML = "IMAGEM"


    btnVerImagem.addEventListener('click', async function () {

      
      window.open(linkApi + '/Imagem/Imagem?i='+element.nomeImagem)

    })


    btnUsar.addEventListener('click', async function () {

      montaPaginaImagem(element)


    })

    btnEditar.addEventListener('click', async function () {
      openViewTable('imagem',true, element.idImagem)
    })

    td3.appendChild(btnEditar)

    
    if(!edita){
    td3.appendChild(btnUsar)
    }

    td4.appendChild(btnVerImagem)


    tr.appendChild(td1)
    tr.appendChild(td2)
    tr.appendChild(td3)
    tr.appendChild(td4)

    tabelaListaImagems.appendChild(tr)
  });
}

