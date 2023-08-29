namespace cmArcade.Shared
{
    public interface IGameField
    {
        public Object GetPlayer();
        public void updateGameState(Score s);
        public bool checkGameOver();
        public void setMessage(string msg);
        public void setScoreMultiplier(int val);
    }
}