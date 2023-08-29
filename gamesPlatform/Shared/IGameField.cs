namespace cmArcade.Shared
{
    public interface IGameField
    {
        public Object GetPlayer();
        public void UpdateGameState(Score s);
        public bool CheckGameOver();
        public void ShowFieldMessage(string msg);
        public void SetScoreMultiplier(int val);
    }
}