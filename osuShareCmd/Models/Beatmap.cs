using System;

namespace osuShareCmd.Models
{
    public class Beatmap
    {
        public int BeatmapId { get; set; }

        public Beatmap(int BeatmapId)
        {
            this.BeatmapId = BeatmapId;
        }

        public Beatmap(string BeatmapId)
        {
            this.BeatmapId = Convert.ToInt32(BeatmapId);
        }
    }
}
