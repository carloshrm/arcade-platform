using System.Runtime.InteropServices.JavaScript;
using System.Runtime.Versioning;

namespace cmArcade.Client.GenericPages;

[SupportedOSPlatform("browser")]
internal partial class CanvasContext
{
    [JSImport("setup", "CanvasContext")]
    internal static partial void SetupContext();
    [JSImport("setupImage", "CanvasContext")]
    internal static partial void SetupImage();
    [JSImport("startBatch", "CanvasContext")]
    internal static partial void StartBatch(); 
    [JSImport("endBatch", "CanvasContext")]
    internal static partial void EndBatch();


    [JSImport("setStrokeStyle", "CanvasContext")]
    internal static partial void SetStrokeStyle(string style);
    [JSImport("stroke", "CanvasContext")]
    internal static partial void Stroke();
    [JSImport("setFillStyle", "CanvasContext")]
    internal static partial void SetFillStyle(string style);
    [JSImport("fill", "CanvasContext")]
    internal static partial void Fill();


    [JSImport("closePath", "CanvasContext")]
    internal static partial void ClosePath();
    [JSImport("beginPath", "CanvasContext")]
    internal static partial void BeginPath();
    [JSImport("moveTo", "CanvasContext")]
    internal static partial void MoveTo(float x, float y);
    [JSImport("lineTo", "CanvasContext")]
    internal static partial void LineTo(float x, float y);
    [JSImport("fillRect", "CanvasContext")]
    internal static partial void FillRect(float x, float y, float width, float height);
    [JSImport("clearRect", "CanvasContext")]
    internal static partial void ClearRect(float x, float y, float width, float height);
    [JSImport("forceClear", "CanvasContext")]
    internal static partial void ForceClear(float x, float y, float width, float height);


    [JSImport("fillText", "CanvasContext")]
    internal static partial void FillText(string text, float x, float y);
    [JSImport("setFont", "CanvasContext")]
    internal static partial void SetFont(string font);


    [JSImport("drawImage", "CanvasContext")]
    internal static partial void DrawImage(string imgID, 
        float? sx, float? sy, float? sWidth, float? sHeight, 
        float dx, float dy, float? dWidth, float? dHeight);

    internal static void DrawImage(string imgID, float dx, float dy, float? dWidth, float dHeight) =>
        DrawImage(imgID, null, null, null, null, dx, dy, dWidth, dHeight);

    internal static void DrawImage(string imgID, float dx, float dy) =>
        DrawImage(imgID, null, null, null, null, dx, dy, null, null);
}