function getWindowWidth() {
    return window.innerWidth;
}

function getWindowHeight() {
    return window.innerHeight;
}

function setFocus() {
    document.getElementById("snake-container").focus();
}

function isMobile() {
    return /android|webos|iphone|ipad|ipod|blackberry|iemobile|opera mini|mobile/i.test(navigator.userAgent);
}