using System;
using System.Linq;
using System.IO;
using GeoCoordinatePortable;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;

namespace LoggingKata
{
    class Program
    {
        static readonly ILog logger = new TacoLogger();
        const string csvPath = "TacoBell-US-AL.csv";

        static void Main(string[] args)
        {

            logger.LogInfo("Log initialized");

            var allLocations = File.ReadAllLines(csvPath);
            if (allLocations.Length <= 1)
            {
                logger.LogInfo("Error: only read 0 to 1 lines from file.");
            }


            logger.LogInfo($"Lines: {allLocations[0]}");

            var parser = new TacoParser();

            var locations = allLocations.Select(parser.Parse).ToArray();

            ITrackable tacoBellFar1 = null;
            ITrackable tacoBellFar2 = null;

            double distance = 0;

            
            for (int i = 0; i < locations.Length; i++)
            {
                var locA = locations[i];
                GeoCoordinate corA = new GeoCoordinate(locA.Location.Latitude, locA.Location.Longitude);
                for (int j = 0; j < locations.Length; j++)
                {
                    var locB = locations[j];
                    GeoCoordinate corB = new GeoCoordinate(locB.Location.Latitude, locB.Location.Longitude);
                    if (distance < corA.GetDistanceTo(corB))
                    {
                        distance = corA.GetDistanceTo(corB);
                        tacoBellFar1 = locA;
                        tacoBellFar2 = locB;
                    }
                }
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine($"The two TacoBells furthest from each other are:\n" +
                $"{tacoBellFar1.Name}\n" +
                $"{tacoBellFar2.Name}");
        }
    }
}
