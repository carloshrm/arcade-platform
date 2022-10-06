namespace cmArcade.Shared
{
    public class GameDecal : GraphicAsset
    {
        public override string spriteId { get; set; }
        public override int width { get; init; }
        public override int height { get; init; }

        public GameDecal()
        {
            spriteId = "hit";
            width = 24;
            height = 22;
        }

        public static readonly GameDecal[] invaderDecals = new GameDecal[]
        {
            new GameDecal { spriteId = "hit", width = 24, height = 22 },
            new GameDecal { spriteId = "laser", width = 4, height = 20 },
            new GameDecal { spriteId = "heart", width = 28, height = 28 },
            new GameDecal { spriteId = "splash", width = 18, height = 16 },
            new GameDecal { spriteId = "barrier", width = 100, height = 70 },
        };

        public static GameDecal getInvaderDecal(string name)
        {
            return Array.Find(invaderDecals, x => x.spriteId == name);
        }
    }
}
