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

