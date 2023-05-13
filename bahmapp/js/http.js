function httpGet(link) {
    return fetch(linkApi + link, {
      method: "GET",
      headers: new Headers({
        "content-type": "application/json",
        Authorization: "Bearer " + sessionStorage.getItem("loginStore"),
      }),
    }).then(function (x) {
      if (x.status == 401) {
        openView("login")
        sessionStorage.setItem("loginStore", false)
      } else {
        return x.json()
      }
    })
  }
  
  function httpPost(link, obj) {
    return fetch(linkApi + link, {
      method: "POST",
      body: JSON.stringify(obj),
      headers: new Headers({
        "content-type": "application/json",
        Authorization: "Bearer " + sessionStorage.getItem("loginStore"),
      }),
    }).then(function (x) {
      if (x.status == 401) {
        openView("login")
        sessionStorage.setItem("loginStore", false)
      } 
      else if(x.status == 400){
        return false
      }
      
      else {
        return x
      }
    });
  }


  function httpPut(link, obj) {
    return fetch(linkApi + link, {
      method: "PUT",
      body: JSON.stringify(obj),
      headers: new Headers({
        "content-type": "application/json",
        Authorization: "Bearer " + sessionStorage.getItem("loginStore"),
      }),
    }).then(function (x) {
      if (x.status == 401) {
        openView("login")
        sessionStorage.setItem("loginStore", false)
      }
           
      else if(x.status == 400){
        return false
      }
      
      else {
        return x
      }
    });
  }