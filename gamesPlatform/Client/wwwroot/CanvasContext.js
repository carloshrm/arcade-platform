let currentContext = undefined;
let currentCanvas = undefined;

let drawingContext = undefined;

export function setup() {
    currentCanvas = document.getElementById("game-display-canvas");
    currentContext = currentCanvas.getContext("2d");
}

export function fillText(msg, x, y) {
    drawingContext.fillText(msg, x, y);
}

export function setFont(font) {
    drawingContext.font = font;
}

export function setStrokeStyle(style) {
    drawingContext.strokeStyle = style;
}

export function stroke() {
    drawingContext.stroke();
}

export function setFillStyle(style) {
    drawingContext.fillStyle = style;
}

export function fill() {
    drawingContext.fill();
}

export function closePath() {
    drawingContext.closePath();
}

export function beginPath() {
    drawingContext.beginPath();
}

export function moveTo(x, y) {
    drawingContext.moveTo(x, y);
}

export function lineTo(x, y) {
    drawingContext.lineTo(x, y);
}

export function fillRect(x, y, width, height) {
    drawingContext.fillRect(x, y, width, height);
}

export function clearRect(x, y, width, height) {
    drawingContext.clearRect(x, y, width, height);
}

export function forceClear(x, y, width, height) {
    currentContext.clearRect(x, y, width, height);
}

export function drawImage(imageID, sx, sy, sWidth, sHeight, dx, dy, dWidth, dHeight) {
    const img = document.getElementById("im-" + imageID);

    if (sx === undefined || sy === undefined)
        drawingContext.drawImage(img, dx, dy, dWidth, dHeight);
    else if (dWidth === undefined || dHeight === undefined)
        drawingContext.drawImage(img, dx, dy);
    else
        drawingContext.drawImage(img, sx, sy, sWidth, sHeight, dx, dy, dWidth, dHeight);
}

export function startBatch() {
    currentCanvas.offscreenCanvas = new OffscreenCanvas(currentCanvas.width, currentCanvas.height);
    drawingContext = currentCanvas.offscreenCanvas.getContext("2d");
}

export function endBatch() {
    currentContext.drawImage(currentCanvas.offscreenCanvas, 0, 0);
    currentCanvas.offscreenCanvas = undefined;
    drawingContext = undefined;
}