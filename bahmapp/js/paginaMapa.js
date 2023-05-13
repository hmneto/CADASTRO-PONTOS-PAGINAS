topFunction = function () {
  document.body.scrollTop = 0;
  document.documentElement.scrollTop = 0;
}

function hoverTop(e) {
  e.style.backgroundColor = '#555'
}

function hoverTop2(e) {
  e.style.backgroundColor = 'red'
}



async function paginaMapaInteracao(id) {

  var mybutton = document.getElementById("myBtn");
  window.onscroll = function () { scrollFunction() };

  function scrollFunction() {
    if (document.body.scrollTop > 20 || document.documentElement.scrollTop > 20) {
      mybutton.style.display = "block";
    } else {
      mybutton.style.display = "none";
    }
  }

  const pagina = await httpGet('/pagina/Pagina?PaginaId=' + id)
  console.log(pagina)

  const {
    listContatoDto,
    enderecoPagina,
    nomePagina,
    listSiteDto,
    concessionaria
  } = pagina


  const {
    infoConcessionaria
  } = concessionaria

  var img = document.getElementById("scream");
  img.src = './images/BAHMCAPA.jpeg'

  img.onload = function () {
    // alert('carregado')
    var canvas = document.getElementById("myCanvas");
    var ctx = canvas.getContext("2d");

    canvas.width = 1150;
    canvas.height = 850;

    canvas.style.width = '100%';
    ctx.drawImage(img, 0, 0);
    ctx.font = "20px Calibri";
    ctx.fillStyle = "white";

    let linhaa = 25;
    let altura = 600;
    let horizontal = 30;
    ctx.fillText(nomePagina, horizontal, altura);
    altura += linhaa;
    ctx.fillText(enderecoPagina, horizontal, altura);

    altura += linhaa;

    listContatoDto.forEach((element) => {
      ctx.fillText(
        `${element.infoContato ? element.infoContato : ""}`,
        horizontal,
        altura
      );
      altura += linhaa;
    });

    ctx.fillText(infoConcessionaria, horizontal, altura);

  }


  document.getElementById('pageContainer').appendChild(document.createElement('br'))

  const obj = {};
  for (var i = 0; i < listSiteDto.length; i++) {
    if (!obj[listSiteDto[i].tipoSite]) obj[listSiteDto[i].tipoSite] = [];
    obj[listSiteDto[i].tipoSite].push(listSiteDto[i].linkSite);
  }

  const {
    STREET,
    LEI,
    FOTO_MAPA,
    FOTO,
    WIKIMAPIA_FRIO,
    WIKIMAPIA_SAT,
    WIKIPEDIA,
    PM,
    SITE,
    ABCR,
    CONCESSIONARIA,
  } = obj;

  montaPagina(STREET)

  montaPagina2(FOTO)

  montaPagina(WIKIMAPIA_SAT)

  montaPagina(WIKIMAPIA_FRIO)

  montaPagina(PM)

  montaPagina(SITE)

  montaPagina(ABCR)

  montaPagina(CONCESSIONARIA)

  montaPagina2(FOTO_MAPA)

  montaPagina(WIKIPEDIA)

  montaPagina(LEI)
}


function montaPagina(array) {
  if (!array) return
  for (let index = 0; index < array.length; index++) {
    const element = array[index];
    const iframe = document.createElement('iframe')
    iframe.src = element
    iframe.style.width = '100%';
    iframe.style.height = '850px';
    document.getElementById('pageContainer').appendChild(iframe)
    document.getElementById('pageContainer').appendChild(document.createElement('br'))
    document.getElementById('pageContainer').appendChild(document.createElement('br'))
  }
}

function montaPagina2(array) {
  if (!array) return
  for (let index = 0; index < array.length; index++) {
    const element = array[index];
    const iframe = document.createElement('img')
    if (element.indexOf('http') === -1) {
      iframe.src = linkApi + '/imagem/Imagem?i=' + element
    } else {
      iframe.src = element
    }
    iframe.style.width = '100%';
    // iframe.style.height = '850px';
    document.getElementById('pageContainer').appendChild(iframe)
    document.getElementById('pageContainer').appendChild(document.createElement('br'))
    document.getElementById('pageContainer').appendChild(document.createElement('br'))
  }
}