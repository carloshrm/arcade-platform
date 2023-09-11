using System.Runtime.InteropServices.JavaScript;
using System.Runtime.Versioning;

namespace cmArcade.Client.GenericPages;

[SupportedOSPlatform("browser")]
public partial class GameCanvas
{
    //[JSImport("Test", "GameCanvas")]
    //internal static partial string DoTest(string msg);

    [JSImport("getWindowWidth", "GameCanvas")]
    internal static partial float GetWindowWidth();

    [JSImport("getWindowHeight", "GameCanvas")]
    internal static partial float GetWindowHeight();

    [JSImport("setFocus", "GameCanvas")]
    internal static partial void SetWindowFocus();

    [JSImport("isMobile", "GameCanvas")]
    internal static partial bool IsBrowserMobile();
}
