function rowDOM(tipoDom, id) {
  const row = document.createElement('div')
  row.id = tipoDom + "-" + id
  row.className = "row"
  return row
}


function colMd10DOM() {
  const colMd10 = document.createElement('div')
  colMd10.className = "col-md-10"
  return colMd10
}

function formGroupDOM() {
  const formGroup = document.createElement('div')
  formGroup.className = "form-group"
  return formGroup
}


// function buttonDOM(element){
//   const button = document.createElement('button')
//   button.innerHTML = "DELETE"
//   button.className = "btn btn-danger form-control"
//   button.addEventListener('click', function () {
//     document.getElementById('contato-' + element.idContato).remove()
//   })
//   return button
// }



function buttonColMd2DOM() {
  const colMd2 = document.createElement('div')
  colMd2.className = "col-md-2"
  return colMd2
}

function buttonPaginaExcluirDOM(element2, title) {

  const buttonPaginaExcluir = document.createElement('button')
  buttonPaginaExcluir.innerHTML = "DELETE"
  buttonPaginaExcluir.className = "btn btn-danger form-control"
  buttonPaginaExcluir.addEventListener('click', function () {
    document.getElementById(title + '-' + element2).remove()
  })

  return buttonPaginaExcluir
}




function buttonUpDOM(idElement, title, elementList, elementDom, listaInputElem) {
  const buttonUp = document.createElement('button')
  buttonUp.id = title + "-" + idElement
  buttonUp.addEventListener('click', function () {
    const collection = document.getElementsByClassName(elementList)
    const ListContatoDto = []
    for (let i = 0; i < collection.length; i++) {
      ListContatoDto.push({
        IdContato: collection[i].value
      })
    }
    const ListDOMElements = [];
    for (let index = 0; index < ListContatoDto.length; index++) {
      const elementas = ListContatoDto[index];
      const currentElement = document.getElementById(elementDom + '-' + elementas.IdContato);
      ListDOMElements.push(currentElement);
    }
    let aux
    let indexDom
    for (let index = 0; index < ListDOMElements.length; index++) {
      const elementd = ListDOMElements[index];

      if (!elementd.id.indexOf(elementDom + '-' + idElement)) {
        aux = ListDOMElements[index]
        indexDom = index
      }
    }

    if (indexDom < 1) return
    ListDOMElements.splice(indexDom, 1);
    document.getElementById(listaInputElem).innerHTML = ""
    for (let index = 0; index < ListDOMElements.length; index++) {
      const element = ListDOMElements[index];
      if ((indexDom - 1) == index) {
        document.getElementById(listaInputElem).appendChild(aux)
      }
      document.getElementById(listaInputElem).appendChild(element)
    }
  })

  buttonUp.innerHTML = "UP"
  return buttonUp
}


function buttonDownDOM(idElement, title, elementSelected,elementDom, listaInputElem) {
  const buttonDown = document.createElement('button')
  buttonDown.innerHTML = "DOWN"
  buttonDown.id = title + "-" + idElement
  buttonDown.addEventListener('click', function () {
    const collection = document.getElementsByClassName(elementSelected)
    const ListContatoDto = []
    for (let i = 0; i < collection.length; i++) {
      ListContatoDto.push({
        IdContato: collection[i].value
      })
    }
    const ListDOMElements = [];
    for (let index = 0; index < ListContatoDto.length; index++) {
      const elementas = ListContatoDto[index];
      const currentElement = document.getElementById(elementDom+'-' + elementas.IdContato);
      ListDOMElements.push(currentElement);
    }
    let aux
    let indexDom
    for (let index = 0; index < ListDOMElements.length; index++) {
      const elementd = ListDOMElements[index];
      if (!elementd.id.indexOf(elementDom+'-' + idElement)) {
        aux = ListDOMElements[index]
        indexDom = index
      }
    }
    if (indexDom >= ListDOMElements.length - 1) return
    ListDOMElements.splice(indexDom, 1);
    document.getElementById(listaInputElem).innerHTML = ""
    for (let index = 0; index < ListDOMElements.length; index++) {
      const element = ListDOMElements[index];
      console.log(indexDom, ListDOMElements.length - 1)
      if ((indexDom + 1) == index) {
        document.getElementById(listaInputElem).appendChild(aux)

      }
      document.getElementById(listaInputElem).appendChild(element)
      if (indexDom == ListDOMElements.length - 1) {
        document.getElementById(listaInputElem).appendChild(aux)
      }
    }
  })
  return buttonDown
}


function inputInfoContatoDOM(element) {
  const inputInfoContato = document.createElement('input')
  inputInfoContato.className = "form-control"
  inputInfoContato.value = element.infoContato
  inputInfoContato.disabled = true
  return inputInfoContato
}





function montaPaginaContato(element) {
  const row = rowDOM("contato", element.idContato)
  const colMd10 = colMd10DOM()
  const formGroup = formGroupDOM()
  const colMd2 = buttonColMd2DOM()
  const buttonPaginaExcluir = buttonPaginaExcluirDOM(element.idContato, 'contato')
  const buttonUp = buttonUpDOM(element.idContato, "paginaContatoUp", "contatoSelecionadoClass", "contato", "listaContatoSelecionado")
  const buttonDown = buttonDownDOM(element.idContato, "paginaContatoDown","contatoSelecionadoClass","contato", "listaContatoSelecionado")
  colMd2.appendChild(buttonUp)
  colMd2.appendChild(buttonDown)
  colMd2.appendChild(buttonPaginaExcluir)
  const inputInfoContato = inputInfoContatoDOM(element)
  const inputIdContato = document.createElement('input')
  inputIdContato.value = element.idContato
  inputIdContato.className = "contatoSelecionadoClass"
  inputIdContato.hidden = true
  row.appendChild(colMd10)
  row.appendChild(colMd2)
  colMd10.appendChild(formGroup)
  formGroup.appendChild(inputInfoContato)
  formGroup.appendChild(inputIdContato)
  listaContatoSelecionado.appendChild(row)
}



function montaPaginaSite(element) {
  const row = rowDOM("site", element.idSite)
  const colMd10 = colMd10DOM()
  const formGroup = formGroupDOM()
  const colMd2 = buttonColMd2DOM()
  const buttonPaginaExcluir = buttonPaginaExcluirDOM(element.idSite, 'site')

  const buttonUp = buttonUpDOM(element.idSite, "paginaSiteUp", "siteSelecionadoClass", "site", "listaSiteSelecionado")
  const buttonDown = buttonDownDOM(element.idSite, "paginaSiteDown","siteSelecionadoClass","site", "listaSiteSelecionado")



  colMd2.appendChild(buttonUp)
  colMd2.appendChild(buttonDown)
  colMd2.appendChild(buttonPaginaExcluir)


  const inputInfoSite = document.createElement('input')
  const inputIdSite = document.createElement('input')
  inputInfoSite.className = "form-control"
  inputInfoSite.value = element.nomeSite
  inputInfoSite.disabled = true
  inputIdSite.value = element.idSite
  inputIdSite.className = "siteSelecionadoClass"
  inputIdSite.hidden = true
  colMd10.appendChild(formGroup)
  row.appendChild(colMd10)
  row.appendChild(colMd2)

  formGroup.appendChild(inputInfoSite)
  formGroup.appendChild(inputIdSite)
  listaSiteSelecionado.appendChild(row)
}


function carregaTipoSite() {
  const listaTipoSite = ["STREET", "FOTO", "WIKIMAPIA_SAT", "WIKIMAPIA_FRIO", "PM", "SITE", "ABCR", "CONCESSIONARIA", "FOTO_MAPA", "WIKIPEDIA", "LEI"]
  const tipoSite = document.getElementById('tipoSite')

  // console.log(listaTipoSite)

  for (i = 0; i < listaTipoSite.length; i++) {
    // console.log(listaTipoSite[i])
    const option = document.createElement('option')
    option.value = listaTipoSite[i]
    option.innerHTML = listaTipoSite[i]
    tipoSite.appendChild(option)
  }
}


function carregaAcao() {
  const listaAcao = ["SIM", "NÃO"]
  const tipoSite = document.getElementById('acaoIcone')

  // console.log(listaAcao)

  for (i = 0; i < listaAcao.length; i++) {
    console.log(listaAcao[i])
    const option = document.createElement('option')
    option.value = listaAcao[i]
    option.innerHTML = listaAcao[i]
    tipoSite.appendChild(option)
  }
}