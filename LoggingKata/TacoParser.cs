using System.Data.Common;

namespace LoggingKata
{
    /// <summary>
    /// Parses a POI file to locate all the Taco Bells
    /// </summary>
    public class TacoParser
    {
        readonly ILog logger = new TacoLogger();
        
        public ITrackable Parse(string line)
        {
            logger.LogInfo("Begin parsing");

            var cells = line.Split(',');

            if (cells.Length < 3)
            {
                logger.LogInfo("Data entry is too short, may be incorrect");
                return null; 
            }

            var latitude = double.Parse(cells[0]);

            var longitude = double.Parse(cells[1]);

            var name = cells[2];
 
            Point latAndLong = new Point() { Latitude = latitude, Longitude = longitude };

            TacoBell tacoBell = new TacoBell() { Name = name, Location = latAndLong };

            return tacoBell;
        }
    }
}
