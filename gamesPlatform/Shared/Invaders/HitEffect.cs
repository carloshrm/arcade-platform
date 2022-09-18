namespace cmArcade.Shared.Invaders
{
    public class HitEffect : GameAsset
    {
        public override int spriteId { get; set; }
        public override int width { get; init; }
        public override int height { get; init; }

        public HitEffect()
        {
            spriteId = -1;
            width = 24;
            height = 22;
        }
    }
}
