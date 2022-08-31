using System.ComponentModel.DataAnnotations;

namespace gamesPlatform.Shared
{
    public class Score
    {
        public int id { get; set; } = -1;

        public int appID { get; set; }

        [Range(0, long.MaxValue)]
        public long scoreValue { get; set; }

        public DateTime runStart { get; set; }

        public TimeSpan runLength { get; set; }

        [StringLength(12, ErrorMessage = "The nickname can only be up to 8 characters long.")]
        public string nickname { get; set; }

        public void setAppID(int appID)
        {
            this.appID = appID;
        }
    }
}
