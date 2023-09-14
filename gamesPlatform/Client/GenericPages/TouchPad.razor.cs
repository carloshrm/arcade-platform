using System.Runtime.InteropServices.JavaScript;
using System.Runtime.Versioning;

namespace cmArcade.Client.GenericPages;

[SupportedOSPlatform("browser")]
public partial class TouchPad
{
    [JSImport("vibrate", "TouchPad")]
    internal static partial void Vibrate();

}
