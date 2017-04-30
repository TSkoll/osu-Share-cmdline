using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using Microsoft.Win32;

using osuShareCmd.Interfaces;

namespace osuShareCmd
{
    public class Program
    {
        public static string osuFolderLocation { get; private set; }
        public static List<ILineWritable> Writables = new List<ILineWritable>();

        public static void Main(string[] args)
        {
            // Get osu folder location from Registry
            osuFolderLocation = GetOsuRegistryFolderPath();
            if (osuFolderLocation == null) // If registry key does not exist
            {
                Console.WriteLine("I wasn't able to find your osu! installation location!\nPlease enter the base folder location.");
                osuFolderLocation = Console.ReadLine();
            }

            var updateLoop = new Timer(200)
            {
                AutoReset = true,
                Enabled = true
            };
            updateLoop.Elapsed += UpdateLoop_Elapsed;
        }

        private static void UpdateLoop_Elapsed(object sender, ElapsedEventArgs e)
        {
            Console.Clear();
            StringBuilder sB = new StringBuilder();
            sB.AppendLine("Downloading...");
            foreach (var writable in Writables)
                sB.AppendLine(writable.Write());
            Console.WriteLine(sB.ToString());
        }

        public static string GetOsuRegistryFolderPath()
        {
            using (RegistryKey req = Registry.ClassesRoot.OpenSubKey("osu\\DefaultIcon"))
            {
                if (req != null)
                {
                    string osuKey = req.GetValue(null).ToString();
                    string path;
                    path = osuKey.Remove(0, 1);
                    path = path.Remove(path.Length - 11);
                    return path;
                }
            }
            return null;
        }
    }
}
