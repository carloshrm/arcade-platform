namespace cmArcade.Shared.Tetris
{
    public class TetrisField : IGameField
    {
        private readonly (int row, int col) limits;

        public TetrisField((int row, int col) limits)
        {
            this.limits = limits;
        }


        public GameObject getPlayer()
        {
            throw new NotImplementedException();
        }

        public void setMessage(string msg)
        {
            throw new NotImplementedException();
        }

        public void setScoreMultiplier(int val)
        {
            throw new NotImplementedException();
        }

        public void updateGameState(Score current)
        {
            throw new NotImplementedException();
        }
        public bool checkGameOver()
        {

        }
    }
}
