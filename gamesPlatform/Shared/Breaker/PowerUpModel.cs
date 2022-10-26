
namespace cmArcade.Shared.Breaker
{
    public partial class PowerUpModel : GraphicAsset
    {
        public override string spriteId { get; set; } = "powerup";
        public override int width { get; init; }
        public override int height { get; init; }

        public static readonly IReadOnlyDictionary<PowerupType, PowerUpModel> powerUps = new Dictionary<PowerupType, PowerUpModel>
        {
            { PowerupType.health , new PowerUpModel { width = 10, height = 10 } },
        };
    }
}
