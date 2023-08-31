using cmArcade.Shared.Breaker;

namespace cmArcade.Shared
{
    public class GameDecal : GraphicAsset
    {
        public override string spriteId { get; set; } = string.Empty;
        public override int width { get; init; }
        public override int height { get; init; }

        public static readonly IReadOnlyCollection<GameDecal> invaderDecals = new List<GameDecal>
        {
            new GameDecal { spriteId = "hit", width = 24, height = 22 },
            new GameDecal { spriteId = "laser", width = 4, height = 20 },
            new GameDecal { spriteId = "barrier", width = 100, height = 70 },
        };

        public static readonly IReadOnlyCollection<GameDecal> genericDecals = new List<GameDecal>
        {
            new GameDecal { spriteId = "heart", width = 28, height = 28 },
        };

        public static readonly IReadOnlyCollection<GameDecal> breakerDecals = new List<GameDecal>
        {
            new GameDecal { spriteId = "crack", width = BlockModel.blocks.First().width, height = BlockModel.blocks.First().height },
            new GameDecal { spriteId = "powerup", width = 10, height = 10 },
        };

        public static GameDecal getInvaderDecal(string name)
        {
            return invaderDecals.First(d => d.spriteId.Equals(name));
        }
    }
}
