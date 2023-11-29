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


            // Complete the Parse method in TacoParser class first and then START BELOW ----------

            // TODO: Create two `ITrackable` variables with initial values of `null`. 
            // These will be used to store your two Taco Bells that are the farthest from each other.
            ITrackable tacoBellFar1 = null;
            ITrackable tacoBellFar2 = null;

            // TODO: Create a `double` variable to store the distance
            double distance = 0;

            // TODO: Add the Geolocation library to enable location comparisons: using GeoCoordinatePortable;
            // Look up what methods you have access to within this library.

            // NESTED LOOPS SECTION----------------------------
            
            // FIRST FOR LOOP -
            // TODO: Create a loop to go through each item in your collection of locations.
            // This loop will let you select one location at a time to act as the "starting point" or "origin" location.
            // Naming suggestion for variable: `locA`
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

            // TODO: Once you have locA, create a new Coordinate object called `corA` with your locA's latitude and longitude.

            // SECOND FOR LOOP -
            // TODO: Now, Inside the scope of your first loop, create another loop to iterate through locations again.
            // This allows you to pick a "destination" location for each "origin" location from the first loop. 
            // Naming suggestion for variable: `locB`

            // TODO: Once you have locB, create a new Coordinate object called `corB` with your locB's latitude and longitude.

            // TODO: Now, still being inside the scope of the second for loop, compare the two locations using
            // `.GetDistanceTo()` method, which returns a double.
            // If the distance is greater than the currently saved distance,
            // update the distance variable and the two `ITrackable` variables you set above.

            // NESTED LOOPS SECTION COMPLETE ---------------------

            // Once you've looped through everything, you've found the two Taco Bells farthest away from each other.
            // Display these two Taco Bell locations to the console.

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine($"The furthest two TacoBells are:\n" +
                $"{tacoBellFar1.Name}\n" +
                $"{tacoBellFar2.Name}");
        }
    }
}
