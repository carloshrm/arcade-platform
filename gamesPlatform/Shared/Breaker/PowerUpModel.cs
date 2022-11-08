
namespace cmArcade.Shared.Breaker
{
    public class PowerUpModel : GraphicAsset
    {
        public override string spriteId { get; set; }
        public override int width { get; init; }
        public override int height { get; init; }

        public static readonly IReadOnlyDictionary<PowerUpType, PowerUpModel> powerUps = new Dictionary<PowerUpType, PowerUpModel>
        {
            { PowerUpType.health, new PowerUpModel { spriteId = "powerup", width = 10, height = 10 } },
            { PowerUpType.ball, new PowerUpModel { spriteId = "powerup", width = 10, height = 10 } },
        };
    }
}
