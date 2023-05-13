function siteSalvaInteracao(){
    carregaTipoSite()
}
async function siteNovo() {
    const site = {
        "nomeSite": document.getElementById('nomeSite').value,
        "linkSite": document.getElementById('linkSite').value,
        "tipoSite": document.getElementById('tipoSite').value,
    }

    await httpPost("/Site/Novo", site).then(x=>{
        if(x){
            openViewTable("listaSite")
        }else{
            alert("erro")
        }   
    })
}

