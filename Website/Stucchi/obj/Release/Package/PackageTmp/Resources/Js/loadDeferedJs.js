function downloadJSAtOnload() {
    for (var idx = 0; idx < deferedJs.length; idx++) {
        var element = document.createElement("script");
        element.src = deferedJs[idx];
        element.async = false;
        document.body.appendChild(element);
    }
}

if (window.addEventListener)
    window.addEventListener("load", downloadJSAtOnload, false);
else if (window.attachEvent)
    window.attachEvent("onload", downloadJSAtOnload);
else window.onload = downloadJSAtOnload;