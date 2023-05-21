function loginInteracao(){
  document.getElementById('botaoLogin').disabled = false
}

function mySubmitFunction(e) {
  document.getElementById('botaoLogin').disabled = true
  e.preventDefault();
  fetch(linkApi + "/usuario/Login", {
    method: "POST",
    body: JSON.stringify({
      EmailUsuario: document.getElementById('inputEmailLogin').value,
      SenhaUsuario: document.getElementById('inputPasswordLogin').value,
    }),
    headers: new Headers({
      "content-type": "application/json",
      'Access-Control-Allow-Origin': '*'
    }),
  })
    .then((x) => x.json())
    .then((x) => {
      console.log(x)
      if (x.token) {
        sessionStorage.setItem("loginStore", x.token);
        sessionStorage.setItem("loginProfile", x.perfilUsuario);

        openView("mapa");
      } else {
        document.getElementById('botaoLogin').disabled = false

        alert(JSON.stringify(x.message))
      }
    });
}