
namespace cmArcade.Shared
{
    public abstract class GameAsset
    {
        public abstract int spriteId { get; set; }
        public abstract int width { get; init; }
        public abstract int height { get; init; }
        public abstract int spriteSelect { get; set; }
    }
}
