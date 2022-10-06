
namespace cmArcade.Shared
{
    public abstract class GraphicAsset
    {
        public abstract string spriteId { get; set; }
        public abstract int width { get; init; }
        public abstract int height { get; init; }
    }
}
