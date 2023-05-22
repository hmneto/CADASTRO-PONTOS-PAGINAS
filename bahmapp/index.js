// const linkApi = "https://localhost:7143"
// const linkApi = "http://0.0.0.0"
// const linkApi = "https://localhost:5001"
const linkApi = "https://bahm.fly.dev"
// const linkApi = "http://localhost"
let dadosPonto = null

const pontosMaps = []

function openViewTable(page, edita, id) {
  fetch(`pages59/${page}.html`)
    .then(function (response) {
      return response.text();
    })
    .then(function (data) {
      document.getElementById("modalBody").innerHTML = data;
      addInteraction(page, edita, id);
    })
}

function openLoad(){
  document.body.style.backgroundColor = 'blue'
  document.getElementById('content').style.display = 'none'
  document.getElementById('loader').style.display = 'block'
}

function closeLoad(){
  document.body.style.backgroundColor = 'white'
  document.getElementById('loader').style.display = 'none'
  document.getElementById('content').style.display = 'block'
}

function openView(page, edita, id) {
  openLoad()

  if(page!='ponto'){
    dadosPonto = null
    latLgnii = null
  }
  fetch(`pages59/${page}.html`)
    .then(function (response) {
      return response.text();
    })
    .then(function (data) {
      document.getElementById("content").innerHTML = data;
      addInteraction(page, edita, id);
    }).then(function(){
      setTimeout(function(){
        closeLoad()
      },1000)
    })
    .catch((error) => {
    });
}

function addInteraction(content, edita, id) {
  if (content === "ponto") {
    if (edita) pontoEditaInteracao(id);
    else pontoNovoInteracao();
  }

  else if (content === "pagina") {
    if (edita) paginaEditaInteracao(id)
  }

  else if (content === "concessionaria") {
    if (edita) concessionariaEditaInteracao(id)
    else concessionariaSalvaInteracao()
  }

  else if (content === "contato") {
    if (edita) contatoEditaInteracao(id)
    else contatoSalvaInteracao()
  }

  else if (content === "icone") {
    if (edita) iconeEditaInteracao(id)
    else iconeSalvaInteracao()
  }

  else if (content === "site") {
    if (edita) siteEditaInteracao(id)
    else siteSalvaInteracao()
  }

  else if (content === "cliente") {
    if (edita) clienteEditaInteracao(id)
    else clienteSalvaInteracao()
  }


  //lista
  else if (content === "listaPagina") {
    listaPaginaInteracao(edita)
  }

  else if (content === "listaConcessionaria") {
    listaConcessionariaInteracao()
  }

  else if (content === "listaCliente") {
    listaClienteInteracao()
  }

  else if (content === "listaContato") {
    listaContatoInteracao()
  }

  else if (content === "listaIcone") {
    listaIconeInteracao()
  }

  else if (content === "listaSite") {
    listaSiteInteracao()
  }

  else if (content === "listaImagens") {
    listaImagemInteracao(edita)
  }
  

  // cliente
  else if (content === "paginaMapa") {
    paginaMapaInteracao(id)
  }

  else if (content === "mapa") {
    mapaInteracao();
  }


  else if (content === "login") {
    loginInteracao();
  }
}

if(sessionStorage.getItem("loginStore")){
  openView("mapa");
}else{
  openView("login");
}

closeLoad()