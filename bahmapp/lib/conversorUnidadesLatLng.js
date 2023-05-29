

// grau minuto segundo x grau decimal
function convertDMStoDD(dms) {
    var parts = dms.split(/[^\d\w.]+/); // Separa a string em partes numéricas
    var deg = parseInt(parts[0]); // Obtém os graus
    var min = parseInt(parts[1]); // Obtém os minutos
    var sec = parseFloat(parts[2]); // Obtém os segundos (como um número decimal)
    var sign = /[swSW]/.test(dms) ? -1 : 1; // Define o sinal com base no hemisfério
    return sign * (deg + min / 60 + sec / 3600); // Calcula a coordenada em graus decimais
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
    var sec = ((absDd - deg - min / 60) * 3600).toFixed(2);
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
  