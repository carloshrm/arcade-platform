namespace cmArcade.Shared.Breaker
{
    public interface IGameField
    {
        public void setMessage(string msg);
        public string getMessages();
        public GameObject getPlayer();
        public void setScoreMultiplier(int val);
    }
}