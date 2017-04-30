namespace osuShareCmd.Models
{
    public class DownloadCompleted
    {
        public byte[] Bytes { get; set; }
        public string Filename { get; set; }

        public DownloadCompleted(byte[] Bytes, string Filename)
        {
            this.Bytes = Bytes;
            this.Filename = Filename;
        }
    }
}
