class CenterMap {

  _latMax;
  _latMin;
  _longMax;
  _longMin;

  centerMap(zoom, ditancia) {

    let latMax;
    let latMin;
    let longMax;
    let longMin;

    let retorno_dados;

    if (getZoomMap() >= zoom) {
      latMax = Math.round(getCenterLatMap() - ditancia);
      latMin = Math.ceil(getCenterLatMap() + ditancia);
      longMax = Math.round(getCenterLngMap() - ditancia);
      longMin = Math.ceil(getCenterLngMap() + ditancia);

      retorno_dados =
        this._latMax !== latMax ||
        this._latMin !== latMin ||
        this._longMax !== longMax ||
        this._longMin !== longMin;
      if (retorno_dados) {
        this._latMax = latMax;
        this._latMin = latMin;
        this._longMax = longMax;
        this._longMin = longMin;
      }
    }

    if (retorno_dados)
      return {
        lat: getCenterLatMap(),
        lng: getCenterLngMap(),
      };
    else return null;
  }
}


function getUrlParams() {
  const latLongZoom = new getUrlVal(["lat", "long", "zooml"]);
  return latLongZoom.get_list();
}

function getInputSearchMapMemoryOrUrl(lat, long, zooml) {
  let latitude;
  let longitude;
  let zoom;

  if (lat, long, zooml) {
    latitude = lat;
    longitude = long;
    zoom = Number(zooml);
    window.location.search = "";
  } else {
    latitude = localStorage.getItem("lat");
    longitude = localStorage.getItem("long");
    zoom = Number(localStorage.getItem("zoom"));
  }

  return {
    latitude,
    longitude,
    zoom,
  };
}

function savePositionsInStorage(lat, lng, zoom) {
  localStorage.setItem("lat", lat);
  localStorage.setItem("long", lng);
  localStorage.setItem("zoom", zoom);
}



function setPositionsInInputs(lat, lng, zoom) {


  if (document.getElementById("latInput")){

    if(document.getElementById('tipoPosicaoSatelite').value  == 'GRAU DECIMAL'){
      document.getElementById("latInput").value = lat;
    }else if(document.getElementById('tipoPosicaoSatelite').value  == 'GRAU MINUTO SEGUNDO'){
      document.getElementById("latInput").value = convertDDtoDMS(lat, true);
    }else if(document.getElementById('tipoPosicaoSatelite').value  == 'GRAU MINUTO'){
      document.getElementById("latInput").value = convertDDtoDMM(lat, "lat")
    }
  }

  if (document.getElementById("longInput")){

    if(document.getElementById('tipoPosicaoSatelite').value  == 'GRAU DECIMAL'){
      document.getElementById("longInput").value = lng;
    }else if(document.getElementById('tipoPosicaoSatelite').value  == 'GRAU MINUTO SEGUNDO'){
      document.getElementById("longInput").value = convertDDtoDMS(lng, false);
    }else if(document.getElementById('tipoPosicaoSatelite').value  == 'GRAU MINUTO'){
      document.getElementById("longInput").value = convertDDtoDMM(lng, "lon")
    }

  }
  if (document.getElementById("zoom"))
    document.getElementById("zoom").value = zoom;
}



function mountPointsInTheMap(list) {
  for (let index = 0; index < pontosMaps.length; index++) {
    pontosMaps[index].setMap(null)
  }

  for (let index = 0; index < list.length; index++) {
    const element = list[index];
    

    let point = createMark(
      getLatLngMaps(element.latitudePonto, element.longitudePonto), element.icone.linkIcone.indexOf("http") === -1 ? linkApi + "/imagem/Imagem?i=" + element.icone.linkIcone
        : element.icone.linkIcone,
        element.idPonto
    )

    pontosMaps.push(point)

    
    // point.id = element.id
    point.setMap(mapiii);
    let infoWindow = new google.maps.InfoWindow({});
    point.addListener("click", () => {



    const markerId = point.get('id');
    console.log(markerId)

      dadosPonto = element
      infoWindow.close();
      infoWindow = new google.maps.InfoWindow({
        position: getLatLngMaps(
          element.latitudePonto,
          element.longitudePonto
        ),
      });

      const div = document.createElement("div");
      const text = document.createElement("text");
      text.innerHTML = element.nomePonto;
      div.appendChild(text);
      if (element.tipo_icone !== "KM") text.style.cursor = "pointer";
      text.addEventListener("click", async function () {
        if (element.icone.acaoIcone === "NÃO") return;
        openView("paginaMapa", false, element.paginaId)
      });

      infoWindow.setContent(div);
      infoWindow.open(this.map, point);
    });
  }
}



function eventFitMap(callback) {
  window.addEventListener("resize", function () {
    callback()
  });
}

function setUpInitalStorage() {
  if (
    !localStorage.getItem("lat") ||
    !localStorage.getItem("long") ||
    !localStorage.getItem("zoom")
  ) {
    localStorage.setItem("lat", "-21");
    localStorage.setItem("long", "-50");
    localStorage.setItem("zoom", "9");
  }
}


function fitMap() {
  const sizeMapInnerHeight = window.innerHeight;
  const sizeMapInnerWidth = window.innerWidth;
  document.getElementById("googleMap").style.height =
    sizeMapInnerHeight + "px";
  document.getElementById("googleMap").style.width = sizeMapInnerWidth + "px";
}



function goToPosition() {
  let lat, lng

  if(document.getElementById('tipoPosicaoSatelite').value  == 'GRAU DECIMAL'){
    lat = document.getElementById("latInput").value
  }else if(document.getElementById('tipoPosicaoSatelite').value  == 'GRAU MINUTO SEGUNDO'){
    lat = convertDMStoDD(document.getElementById("latInput").value, true)
  }else if(document.getElementById('tipoPosicaoSatelite').value  == 'GRAU MINUTO'){
    lat = convertDMtoDD(document.getElementById("latInput").value)
  }


  if(document.getElementById('tipoPosicaoSatelite').value  == 'GRAU DECIMAL'){
    lng = document.getElementById("longInput").value
  }else if(document.getElementById('tipoPosicaoSatelite').value  == 'GRAU MINUTO SEGUNDO'){
    lng = convertDMStoDD(document.getElementById("longInput").value, false)
  }else if(document.getElementById('tipoPosicaoSatelite').value  == 'GRAU MINUTO'){
    lng = convertDMtoDD(document.getElementById("longInput").value)
  }

  goToLatLngMap(
    lat,
    lng,
    document.getElementById("zoom").value
  );
}

function isPointClicked() {
  if (!dadosPonto) alert("não clicado")
  if (!dadosPonto) return false
  else return true
}

function setLinkLatLng() {
  const { lat, lng, zoom } = getLatLongZoom();
  const link = `${window.location}?lat=${lat}&long=${lng}&zooml=${zoom}`;

  if (document.getElementById("link"))
    document.getElementById("link").value = link;
}


class getUrlVal {
  lista = null;
  constructor(parametros_url) {
    let lista_parametros_url_resolvidas = {};
    const get = (name) => {
      if (
        (name = new RegExp("[?&]" + encodeURIComponent(name) + "=([^&]*)").exec(
          window.location.search
        ))
      )
        return decodeURIComponent(name[1]);
    };

    parametros_url.forEach((element) => {
      lista_parametros_url_resolvidas[element] = get(element);
    });
    this.lista = lista_parametros_url_resolvidas;
  }

  get_list() {
    return this.lista;
  }
}


async function MontaDados(centerMap) {
  const { lat, lng, zoom } = getLatLongZoom();
  savePositionsInStorage(lat, lng, zoom);
  setPositionsInInputs(lat, lng, zoom);
  const centro = centerMap.centerMap(8, 2);
  if (!centro) return;
  const pontos = await httpPost("/ponto/Pontos", {
    LatitudePonto: centro.lat,
    LongitudePonto: centro.lng,
    Zoom: getZoomMap(),
    ObservacaoPonto : "pontos"
  }).then(x => x.json());
  if (pontos == undefined) return
  mountPointsInTheMap(pontos);
}


function initMap(){
  const centerMap = new CenterMap()
  setUpInitalStorage();
  myMap();
  MontaDados(centerMap);
  eventDragMap(function () {
    MontaDados(centerMap);
  })
  eventZoomMap(function () {
    MontaDados(centerMap);
  });
  eventClickMap();
  fitMap();
  eventFitMap(function () {
    fitMap();
  });
}



function mapaInteracao() {
  httpGet('/ApiMaps/Google').then(x=>{
      console.log('api google maps')
      const scriptMaps = document.createElement('script')
      scriptMaps.src=`https://maps.googleapis.com/maps/api/js?key=${x.apiMaps}&callback=initMap`
      document.getElementById('content').appendChild(scriptMaps)
  
  })
}



// grau minuto segundo x grau decimal
function convertDMStoDD(dms) {
  var parts = dms.split(/[^\d\w.]+/); // Separa a string em partes numéricas
  var deg = parseInt(parts[0]); // Obtém os graus
  var min = parseInt(parts[1]); // Obtém os minutos
  var sec = parseFloat(parts[2]); // Obtém os segundos (como um número decimal)
  var sign = /[swSW]/.test(dms) ? -1 : 1; // Define o sinal com base no hemisfério
  return sign * (deg + min/60 + sec/3600); // Calcula a coordenada em graus decimais
}

// Exemplo de uso:
// console.log(convertDMStoDD("51° 30' 26.00\" N")); // Saída: 51.507222222222225
// console.log(convertDMStoDD("0° 7' 39.53\" W")); // Saída: -0.12764722222222222





// grau decimal x grau minuto segundo
function convertDDtoDMS(dd, isLatitude) {
  var sign = dd < 0 ? (isLatitude ? "S" : "W") : (isLatitude ? "N" : "E"); // Determina o sinal a ser usado
  var absDd = Math.abs(dd); // Remove o sinal para trabalhar com valores positivos
  var deg = Math.floor(absDd);
  var min = Math.floor((absDd - deg) * 60);
  var sec = ((absDd - deg - min/60) * 3600).toFixed(2);
  return deg + "°" + min + "'" + sec + '"' + sign; // Concatena o sinal no final da string
}

// // Exemplo de uso:
// console.log(convertDDtoDMS(51.507222, true)); // Saída: "51° 30' 26.00" N"
// console.log(convertDDtoDMS(-0.127647, false)); // Saída: "0° 7' 39.53" W"









// grau minuto x grau decimal
function convertDMtoDD(dm) {
  var regex = /(\d+)°(\d+\.\d+)'([WENS])/i; // Expressão regular para extrair graus, minutos e direção
  var matches = dm.match(regex);

  var degrees = parseInt(matches[1]); // Obtém os graus inteiros
  var minutes = parseFloat(matches[2]); // Obtém os minutos decimais
  var dir = matches[3]; // Obtém a direção (leste, oeste, norte, sul)

  var dd = degrees + (minutes / 60); // Calcula o valor em graus decimais

  if (dir === "W" || dir === "S") {
    dd *= -1; // Ajusta o sinal para oeste e sul
  }

  return dd;
}

// Exemplo de uso:
//23° 54.35273’ S , 46° 8.9098166666667’ W
//console.log(convertDMtoDD("51°30.433'N")); // Saída: 51.507216666666665
















// grau decimal x grau minuto segundo
function convertDDtoDMM(dd, type) {
  //var sign = dd < 0 ? "-" : ""; // Determina o sinal a ser usado
  var absDd = Math.abs(dd); // Remove o sinal para trabalhar com valores positivos
  var deg = Math.floor(absDd); // Obtém os graus
  var min = (absDd - deg) * 60; // Converte a parte decimal em minutos
  var formattedMin = min.toFixed(3); // Formata os minutos com 3 casas decimais
  var direction = "";
  
  // Verifica se é uma latitude ou longitude e determina a direção correta
  if (type === "lat") {
    direction = dd >= 0 ? "N" : "S";
  } else if (type === "lon") {
    direction = dd >= 0 ? "E" : "W";
  }
  
  return deg + "°" + formattedMin + "'" + direction; // Concatena a string com o sinal, os graus e minutos formatados e a direção
}

// Exemplo de uso:
// console.log(convertDDtoDMM(51.507222, "lat")); // Saída: "51 30.433° N"
// console.log(convertDDtoDMM(-0.127647, "lon")); // Saída: "0 7.659° W"



// for (let index = 0; index < pontosMaps.length; index++) {
//   console.log(pontosMaps[index].setMap(null))
// }



// for (let index = 0; index < pontosMaps.length; index++) {
//   if(index == 0){
//       pontosMaps[index].setMap(null)
//       //pontosMaps.pop()
//   }
// }




// for (let index = 0; index < pontosMaps.length; index++) {
//   console.log(pontosMaps[index])
// }
