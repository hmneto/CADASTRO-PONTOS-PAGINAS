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
        latMax = Math.round(Number(getCenterLatMap()) - Math.abs(ditancia));
        latMin = Math.ceil(Number(getCenterLatMap()) + Math.abs(ditancia));
        longMax = Math.round(Number(getCenterLngMap()) - Math.abs(ditancia));
        longMin = Math.ceil(Number(getCenterLngMap()) + Math.abs(ditancia));
  
        retorno_dados =
          this._latMax !== latMax &&
          this._latMin !== latMin &&
          this._longMax !== longMax &&
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
          latMax: this._latMax,
          latMin: this._latMin,
          longMax: this._longMax,
          longMin: this._longMin
        };
      else return null;
    }
  }
  