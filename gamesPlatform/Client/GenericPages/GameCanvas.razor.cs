using System.Runtime.InteropServices.JavaScript;
using System.Runtime.Versioning;

namespace cmArcade.Client.GenericPages;

[SupportedOSPlatform("browser")]
public partial class GameCanvas
{
    [JSImport("getWindowWidth", "GameCanvas")]
    internal static partial float GetWindowWidth();

    [JSImport("getWindowHeight", "GameCanvas")]
    internal static partial float GetWindowHeight();

    [JSImport("setFocus", "GameCanvas")]
    internal static partial void SetWindowFocus();

    [JSImport("isMobile", "GameCanvas")]
    internal static partial bool IsBrowserMobile();
}
