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


  if (document.getElementById("latInput")) {

    if (document.getElementById('tipoPosicaoSatelite').value == 'GRAU DECIMAL') {
      document.getElementById("latInput").value = lat;
    } else if (document.getElementById('tipoPosicaoSatelite').value == 'GRAU MINUTO SEGUNDO') {
      document.getElementById("latInput").value = convertDDtoDMS(lat, true);
    } else if (document.getElementById('tipoPosicaoSatelite').value == 'GRAU MINUTO') {
      document.getElementById("latInput").value = convertDDtoDMM(lat, "lat")
    }
  }

  if (document.getElementById("longInput")) {

    if (document.getElementById('tipoPosicaoSatelite').value == 'GRAU DECIMAL') {
      document.getElementById("longInput").value = lng;
    } else if (document.getElementById('tipoPosicaoSatelite').value == 'GRAU MINUTO SEGUNDO') {
      document.getElementById("longInput").value = convertDDtoDMS(lng, false);
    } else if (document.getElementById('tipoPosicaoSatelite').value == 'GRAU MINUTO') {
      document.getElementById("longInput").value = convertDDtoDMM(lng, "lon")
    }

  }
  if (document.getElementById("zoom"))
    document.getElementById("zoom").value = zoom;
}



function mountPointsInTheMap(list, centro, zoom) {

  for (let index = 0; index < list.length; index++) {
    const element = list[index];

    const linkFormatadoIcone = element.icone.linkIcone.indexOf("http") === -1 ?
      linkApi + "/imagem/Imagem?i=" + element.icone.linkIcone :
      element.icone.linkIcone

    let point = createMark(
      getLatLngMaps(element.latitudePonto, element.longitudePonto),
      linkFormatadoIcone,
      element.idPonto
    )


    if (pontosMaps.filter((ponto) => ponto.latitudePonto == element.latitudePonto && ponto.longitudePonto === element.longitudePonto).length < 1)
      pontosMaps.push({
        point,
        latitudePonto: element.latitudePonto,
        longitudePonto: element.longitudePonto
      })

    point.setMap(mapiii);
    let infoWindow = new google.maps.InfoWindow({});
    point.addListener("click", () => {
      const markerId = point.get('id');
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

        httpGet('/ApiMaps/IconePaginaZero?PontoId='+element.idPonto).then(y => { console.log(y) })


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

  if (document.getElementById('tipoPosicaoSatelite').value == 'GRAU DECIMAL') {
    lat = document.getElementById("latInput").value
  } else if (document.getElementById('tipoPosicaoSatelite').value == 'GRAU MINUTO SEGUNDO') {
    lat = convertDMStoDD(document.getElementById("latInput").value, true)
  } else if (document.getElementById('tipoPosicaoSatelite').value == 'GRAU MINUTO') {
    lat = convertDMtoDD(document.getElementById("latInput").value)
  }


  if (document.getElementById('tipoPosicaoSatelite').value == 'GRAU DECIMAL') {
    lng = document.getElementById("longInput").value
  } else if (document.getElementById('tipoPosicaoSatelite').value == 'GRAU MINUTO SEGUNDO') {
    lng = convertDMStoDD(document.getElementById("longInput").value, false)
  } else if (document.getElementById('tipoPosicaoSatelite').value == 'GRAU MINUTO') {
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

async function MontaDados(centerMap) {
  const { lat, lng, zoom } = getLatLongZoom();
  savePositionsInStorage(lat, lng, zoom);
  setPositionsInInputs(lat, lng, zoom);
  const centro = centerMap.centerMap(8, 5);
  if (!centro) return;
  const pontos = await httpPost("/ponto/Pontos", {
    LatitudePonto: centro.lat,
    LongitudePonto: centro.lng,
    Zoom: getZoomMap(),
    ObservacaoPonto: "pontos"
  }).then(x => x.json());
  if (pontos == undefined) return
  mountPointsInTheMap(pontos, centro, zoom);
}


function initMap() {
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

  if (sessionStorage.getItem("loginProfile") != "admin") {
    document.getElementById('controlSavarPonto').style.display = 'none'
    document.getElementById('controlEditarPonto').style.display = 'none'
    document.getElementById('controlPaginas').style.display = 'none'
    document.getElementById('controlImagens').style.display = 'none'
  }

  httpGet('/ApiMaps/Google').then(x => {
    httpGet('/ApiMaps/MapsCount').then(y => {
      const scriptMaps = document.createElement('script')
      scriptMaps.src = `https://maps.googleapis.com/maps/api/js?key=${x.apiMaps}&callback=initMap`
      document.getElementById('content').appendChild(scriptMaps)
    })
  })
}

