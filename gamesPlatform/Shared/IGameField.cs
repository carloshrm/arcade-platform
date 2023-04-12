namespace cmArcade.Shared
{
    public interface IGameField
    {
        public Object getPlayer();
        public void updateGameState(Score s);
        public bool checkGameOver();
        public void setMessage(string msg);
        public void setScoreMultiplier(int val);
    }
}