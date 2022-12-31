using Newtonsoft.Json;
using CryptoPricePrediction.Models;

namespace CryptoPricePrediction.Utils
{
    public class Util
    {
        /// <summary>
        /// Utility class that contains the following methods: UnitTimeToDateTime and LoadJson.
        /// </summary>
        public DateTime UnixTimeToDateTime(long unixtime)
        {
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddMilliseconds(unixtime).ToLocalTime();
            return dtDateTime;
        }

        public Root? LoadJson()
        {
            string filePath = AppDomain.CurrentDomain.BaseDirectory + "config.json";

            using (StreamReader r = new StreamReader(filePath))
            {
                string json = r.ReadToEnd();
                return JsonConvert.DeserializeObject<Root>(json);
            }
        }
    }
}
