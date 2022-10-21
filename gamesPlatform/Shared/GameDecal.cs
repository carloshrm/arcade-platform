using cmArcade.Shared.Breaker;

namespace cmArcade.Shared
{
    public class GameDecal : GraphicAsset
    {
        public override string spriteId { get; set; }
        public override int width { get; init; }
        public override int height { get; init; }

        public static readonly IReadOnlyDictionary<string, GameDecal> invaderDecals = new Dictionary<string, GameDecal>
        {
            { "hit", new GameDecal { spriteId = "hit", width = 24, height = 22 } },
            { "laser", new GameDecal { spriteId = "laser", width = 4, height = 20 } },
            { "splash", new GameDecal { spriteId = "splash", width = 18, height = 16 } },
            { "barrier", new GameDecal { spriteId = "barrier", width = 100, height = 70 } },
        };

        public static readonly IReadOnlyDictionary<string, GameDecal> genericDecals = new Dictionary<string, GameDecal>
        {
            { "heart", new GameDecal { spriteId = "heart", width = 28, height = 28 } },
        };

        public static readonly IReadOnlyDictionary<string, GameDecal> breakerDecals = new Dictionary<string, GameDecal>
        {
            { "crack", new GameDecal { spriteId = "crack", width = BlockModel.blocks.First().width, height = BlockModel.blocks.First().height } },
        };

        public static GameDecal getInvaderDecal(string name)
        {
            return invaderDecals[name];
        }
    }
}
