// class GoogleMaps {
let mapiii
let latLgnii
function myMap() {
  let { lat, long, zooml } = getUrlParams();

  const { latitude, longitude, zoom } = getInputSearchMapMemoryOrUrl(
    lat,
    long,
    zooml
  );

  mapiii = new google.maps.Map(
    document.getElementById("googleMap"),
    {
      center: getLatLngMaps(latitude, longitude),
      zoom: zoom,
      mapTypeId: google.maps.MapTypeId.HYBRID,
    }
  );
}

function eventDragMap(callback) {
  mapiii.addListener("drag", () => {
    callback()
  });
}


function eventZoomMap(callback) {
  mapiii.addListener("zoom_changed", () => {
    callback()
  });
}

function getLatLngMaps(lat, lng) {
  return new google.maps.LatLng(parseFloat(lat), parseFloat(lng));
}

function createMark(position, icon, id) {
  return new google.maps.Marker({ position, icon, id: 'marker' + id });
}

function getZoomMap() {
  return mapiii.getZoom();
}

function getCenterLatMap() {
  return mapiii.getCenter().lat().toFixed(6);
}

function getCenterLngMap() {
  return mapiii.getCenter().lng().toFixed(6);
}

function getLastLatLngClick() {
  if (latLgnii == undefined) alert("Clique no mapa para selecionar um ponto!")
  if (latLgnii == undefined) return false
  const { lat, lng } = latLgnii;
  return {
    lat: lat.toFixed(6),
    lng: lng.toFixed(6),
  };
}

function cleanLastLatLngClick() {
  latLgnii = null
}

function eventClickMap() {
  let point = new google.maps.Marker({});
  mapiii.addListener("click", (mapsMouseEvent) => {
    latLgnii = mapsMouseEvent.latLng.toJSON();

    const { lat, lng } = latLgnii;
    point.setMap(null);

    if(sessionStorage.getItem("loginProfile") == "admin") {
      point = createMark(
        getLatLngMaps(lat, lng)
      );
      point.setMap(mapiii);
    }



    let infoWindow = new google.maps.InfoWindow({});
    point.addListener("click", () => {
      infoWindow.close();
      infoWindow = new google.maps.InfoWindow({
        position: mapsMouseEvent.latLng,
      });
      infoWindow.setContent(`${lat.toFixed(6)}, ${lng.toFixed(6)}`);
      infoWindow.open(mapiii, point);
    });
  });
}

function getLatLongZoom() {
  return {
    lat: getCenterLatMap(),
    lng: getCenterLngMap(),
    zoom: mapiii.getZoom(),
  };
}

function goToLatLngMap(lat, lng, zoom) {
  const latLng = getLatLngMaps(lat, lng);

  if(window.pointSearch != undefined)
  window.pointSearch.setMap(null);

  window.pointSearch = createMark(
    getLatLngMaps(lat, lng), './images/TRUCK.png'
  );
  window.pointSearch.setMap(mapiii);



  mapiii.panTo(latLng);
  mapiii.setZoom(Number(zoom));
}

function getLatLong() {
  return latLgnii
}
