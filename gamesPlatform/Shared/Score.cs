namespace gamesPlatform.Shared
{
    public class Score
    {
        public int id { get; set; }
        public int appID { get; set; }
        public long scoreValue { get; set; }
        public DateTime runStart { get; set; }
        public TimeSpan runLength { get; set; }
        public string nickname { get; set; }
    }
}
