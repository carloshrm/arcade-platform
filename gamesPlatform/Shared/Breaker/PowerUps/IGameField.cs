namespace cmArcade.Shared.Breaker
{
    public interface IGameField
    {
        public GameObject getPlayer();
        public void setScoreMultiplier(int val);
    }
}