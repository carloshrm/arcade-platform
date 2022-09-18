namespace cmArcade.Shared
{
    public class LaserModel : GameAsset
    {
        public override int spriteId { get; set; }
        public override int width { get; init; }
        public override int height { get; init; }

        public LaserModel()
        {
            spriteId = -2;
            width = 4;
            height = 20;
        }
    }
}
