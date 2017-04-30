namespace osuShareCmd.Models
{
    public class DownloadProgress
    {
        public int DownloadPercentage { get; set; }
        public long TotalBytesToReceive { get; set; }
        public long CurrentBytesReceived { get; set; }

        public DownloadProgress(int downloadPercentage, long totalBytesToReceive, long currentBytesReceived)
        {
            DownloadPercentage = downloadPercentage;
            TotalBytesToReceive = totalBytesToReceive;
            CurrentBytesReceived = currentBytesReceived;
        }
    }
}
