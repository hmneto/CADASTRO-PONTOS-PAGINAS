function fillInputsLatLng() {
    const { lat, lng } = getLatLong()
    document.getElementById('latitude').value = lat.toFixed(6)
    document.getElementById('longitude').value = lng.toFixed(6)
}