export function getWindowWidth() {
    return window.innerWidth;
}

export function getWindowHeight() {
    return window.innerHeight;
}

export function setFocus() {
    document.getElementById("game-container").focus();
}

export function isMobile() {
    return /android|webos|iphone|ipad|ipod|blackberry|iemobile|opera mini|mobile/i.test(navigator.userAgent);
}