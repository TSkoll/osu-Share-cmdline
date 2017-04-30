using System.Collections.Generic;
using System.IO;

using osuShareCmd.Models;

namespace osuShareCmd.Parser
{
    public static class OsuSongsParser
    {
        public static int[] GetBeatmapSetIds()
        {
            List<int> Ids = new List<int>();

            var folders = Directory.GetDirectories(Path.Combine(Program.osuFolderLocation, "Songs"));
            foreach (var folder in folders)
            {
                var id = folder.Split(' ')[0];
                if (int.TryParse(id, out int idInt))
                    Ids.Add(idInt);
            }

            return Ids.ToArray();
        }

        public static Beatmap[] GetBeatmaps()
        {
            List<Beatmap> Beatmaps = new List<Beatmap>();

            var folders = Directory.GetDirectories(Path.Combine(Program.osuFolderLocation, "Songs"));
            foreach (var folder in folders)
            {
                var id = folder.Split(' ')[0];
                if (int.TryParse(id, out int idInt))
                    Beatmaps.Add(new Beatmap(idInt));
            }

            return Beatmaps.ToArray();
        }
    }
}
