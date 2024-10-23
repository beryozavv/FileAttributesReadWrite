using System;
using System.Runtime.InteropServices;

namespace FileAttributesReadWrite
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IFileAttributesService? fileAttributesService;

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                fileAttributesService = new LinuxFileAttributesService();
                Console.WriteLine("Linux platform");
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                fileAttributesService = new NtfsFileAttributesService();
                Console.WriteLine("Windows platform");
            }
            else
            {
                Console.WriteLine("platform is not supported");
                return;
            }

            var arg = args[0];

            var filePath = args[1];
            Console.WriteLine($"Current path to file {filePath}");

            var key = "DssMarker";

            switch (arg)
            {
                case "w":
                {
                    var markerGuid = Guid.NewGuid();
                    Console.WriteLine("Test guid for write {0}", markerGuid);

                    fileAttributesService.Write(filePath,
                        key, $"My test data {markerGuid}");
                    break;
                }
                case "r":
                {
                    var result = fileAttributesService.Read(filePath, key);

                    Console.WriteLine("Read: {0}", result);
                    break;
                }
                default:
                    Console.WriteLine("Invalid argument. Set r to read data or w to write data");
                    break;
            }
        }
    }
}