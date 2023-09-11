using System.Numerics;

namespace cmArcade.Shared.Invaders
{
    public class FieldBarrier : IGameObject
    {
        public Vector2 pos { get; set; }
        public int healthPoints { get; set; }
        public GraphicAsset model { get; set; }
        public int spriteSelect { get; set; }
        public List<GraphicAsset>? decals { get; set; } = null;

        public FieldBarrier(float row, float col)
        {
            model = GameDecal.getInvaderDecal("barrier");
            pos = new Vector2(col, row);
            healthPoints = 6;
            spriteSelect = 0;
        }

        public void hit()
        {
            healthPoints--;
            spriteSelect = healthPoints > 3 ? 0 : healthPoints == 0 ? 2 : 1;

        }

        public bool UpdatePosition((float row, float col) limits)
        {
            return false;
        }
    }
}
