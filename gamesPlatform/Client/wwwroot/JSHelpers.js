function getWindowWidth() {
    return window.innerWidth;
}

function getWindowHeight() {
    return window.innerHeight;
}

function setFocus() {
    document.getElementById("game-container").focus();
}

function isMobile() {
    return /android|webos|iphone|ipad|ipod|blackberry|iemobile|opera mini|mobile/i.test(navigator.userAgent);
}

function vibrateFeedback() {
    navigator.vibrate([50]);
}