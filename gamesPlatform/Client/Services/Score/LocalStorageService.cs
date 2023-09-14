using System.Runtime.InteropServices.JavaScript;
using System.Runtime.Versioning;

namespace cmArcade.Client.Services;

[SupportedOSPlatform("browser")]
public partial class LocalStorageService
{
    [JSImport("globalThis.localStorage.setItem")]
    internal static partial void SetItem([JSMarshalAs<JSType.String>] string label, [JSMarshalAs<JSType.String>] string jsonData);

    [JSImport("globalThis.localStorage.getItem")]
    internal static partial string? GetItem([JSMarshalAs<JSType.String>] string label);
}