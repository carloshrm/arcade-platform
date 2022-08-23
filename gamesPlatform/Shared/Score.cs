namespace gamesPlatform.Shared
{
    public class Score
    {
        public int id { get; set; }
        public int appID { get; set; }
        public int scoreValue { get; set; }
        public DateTime runStart { get; set; }
        public TimeSpan runLength { get; set; }
        public string nickname { get; set; }

        public Score()
        {

        }

        //public Score(int appID, int scoreValue, string nickname)
        //{
        //    this.appID = appID;
        //    this.scoreValue = scoreValue;
        //    this.nick = nickname;
        //}
    }
}
