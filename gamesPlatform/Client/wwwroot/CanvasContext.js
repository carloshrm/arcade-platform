﻿let currentContext = undefined;
let currentCanvas = undefined;

let drawingContext = undefined;

export function setup() {
    currentCanvas = document.getElementById("game-display-canvas");
    currentContext = currentCanvas.getContext("2d");
    drawingContext = currentContext;
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

export function clearCanvas() {
    if (currentContext)
        currentContext.clearRect(0, 0, currentCanvas.width, currentCanvas.height);
}

export function drawImage(imageID, sx, sy, sWidth, sHeight, dx, dy, dWidth, dHeight) {
    const imageElement = document.getElementById("im-" + imageID);

    if (dWidth !== null && dHeight !== null) {
        if (sx !== null && sy !== null)
            drawingContext.drawImage(imageElement, sx, sy, sWidth, sHeight, dx, dy, dWidth, dHeight);
        else
            drawingContext.drawImage(imageElement, dx, dy, dWidth, dHeight);
    } else
        drawingContext.drawImage(imageElement, dx, dy);
}

export function startBatch() {
    currentCanvas.offscreenCanvas = new OffscreenCanvas(currentCanvas.width, currentCanvas.height);
    drawingContext = currentCanvas.offscreenCanvas.getContext("2d");
}

export function endBatch() {
    currentContext.drawImage(currentCanvas.offscreenCanvas, 0, 0);
    currentCanvas.offscreenCanvas = null;
    drawingContext = currentContext;
}