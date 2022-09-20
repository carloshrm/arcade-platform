namespace cmArcade.Shared
{
    public class GameDecal : GameAsset
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

        public static readonly GameDecal[] decals = new GameDecal[]
        {
            new GameDecal { spriteId = "hit", width = 24, height = 22 },
            new GameDecal { spriteId = "laser", width = 4, height = 20 },
            new GameDecal { spriteId = "heart", width = 14, height = 14 },
            new GameDecal { spriteId = "splash", width = 18, height = 16 },
        };

        public static GameDecal GetDecal(string name)
        {
            return Array.Find(decals, x => x.spriteId == name);
        }
    }
}
