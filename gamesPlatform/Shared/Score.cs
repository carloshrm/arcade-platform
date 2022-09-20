using System.ComponentModel.DataAnnotations;

namespace cmArcade.Shared
{
    public class Score
    {
        public int id { get; set; }
        public int appID { get; set; }
        [Range(0, long.MaxValue)]
        public long scoreValue { get; set; }
        public DateTime runStart { get; set; }
        public TimeSpan runLength { get; set; }
        [StringLength(12, ErrorMessage = "The nickname can only be up to 8 characters long.")]
        public string nickname { get; set; }
        public int turn { get; set; }

        public Score()
        {
            id = -1;
            scoreValue = 0;
            runStart = DateTime.Now;
            runLength = TimeSpan.Zero;
            nickname = string.Empty;
            turn = 0;
        }

        public Score(AppID appID)
        {
            this.appID = (int)appID;
        }
    }
}
