using arcade_v2.Data.Common;

namespace arcade_v2.Data.Models
{
    public class Score
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public required GameID Game {  get; set; }
        public required ApplicationUser User { get; set; }
        public ulong Value { get; set; } = 0;
        public ulong TurnCount { get; set; } = 0;
        public required DateTime SessionStart { get; set; }
        public required DateTime SessionEnd { get; set; }

    }

}
