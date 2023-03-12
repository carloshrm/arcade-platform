namespace cmArcade.Shared
{
    public interface IGameField
    {
        public GameObject getPlayer();
        public void updateGameState();
        public bool checkGameOver();
        public void setMessage(string msg);
        public void setScoreMultiplier(int val);
    }
}