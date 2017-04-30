using System;
using System.Threading.Tasks;
using System.Net;

using osuShareCmd.Models;

namespace osuShareCmd.DL
{
    public class Download
    {
        private string _URL { get; set; }
        private IProgress<DownloadProgress> _Progress { get; set; }

        public Download(string URL, IProgress<DownloadProgress> Progress)
        {
            _URL = URL;
            _Progress = Progress;
        }

        public async Task<DownloadCompleted> DownloadFileAsync()
        {
            using (var client = new WebClient())
            {
                client.DownloadProgressChanged += (s, e) =>
                {
                    _Progress.Report(new DownloadProgress(e.ProgressPercentage, e.TotalBytesToReceive, e.BytesReceived)); // Reports download progress to host
                };

                var bytes = await client.DownloadDataTaskAsync(_URL).ConfigureAwait(false); // Download file as byte array

                // Header handling
                var contentHeader = client.ResponseHeaders["Content-Disposition"];
                var header = contentHeader.Split(';')[1].Trim(); // Get filename header

                var firstIndex = header.IndexOf('"');
                var lastIndex = header.LastIndexOf('"');
                var filename = header.Substring(firstIndex + 1, (lastIndex - 1) - (firstIndex + 1)); // Gets the filename inside the quotes

                return new DownloadCompleted(bytes, filename);
            }
        }
    }
}
