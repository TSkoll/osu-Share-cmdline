using System;
using System.Threading.Tasks;
using System.IO;

using osuShareCmd.DL;
using osuShareCmd.Interfaces;

namespace osuShareCmd.Models
{
    public class DownloadLineContainer : ILineWritable
    {
        private Download Download { get; set; }

        public Beatmap Beatmap { get; private set; }

        public int ProgressPercentage { get; private set; }
        public long MaxBytes { get; private set; }
        public long CurrentBytes { get; private set; }

        public DownloadLineContainer(Beatmap Beatmap)
        {
            this.Beatmap = Beatmap;

            IProgress<DownloadProgress> Progress = new Progress<DownloadProgress>(e =>
            {
                ProgressPercentage = e.DownloadPercentage;
                MaxBytes = e.TotalBytesToReceive;
                CurrentBytes = e.CurrentBytesReceived;
            });

            var downloadUrl = $"http://bloodcat.com/osu/s/{Beatmap.BeatmapId}";
            Download = new Download(downloadUrl, Progress);
        }

        public async Task<bool> StartDownload()
        {
            var dl = await Download.DownloadFileAsync().ConfigureAwait(false); // Downloads the file and returns the DownloadCompleted object
            File.WriteAllBytes(Path.Combine(Program.osuFolderLocation, "Songs", dl.Filename), dl.Bytes); // Writes the .osz to osu! songs folder, BLOCKS!!!
            return true;
        }

        public string Write()
        {
            return $"Set {Beatmap.BeatmapId} => {ProgressPercentage}% [{CurrentBytes}/{MaxBytes}]";
        }
    }
}
