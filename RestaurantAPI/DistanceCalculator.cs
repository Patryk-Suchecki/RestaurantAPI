using Newtonsoft.Json;
using System.Net;

namespace RestaurantAPI
{
    public class DistanceCalculator
    {
        private string apiKey;

        public DistanceCalculator(string apiKey)
        {
            this.apiKey = apiKey;
        }

        public int CalculateDistance(string origin, string destination)
        {
            string url = $"https://maps.googleapis.com/maps/api/distancematrix/json?origins={origin}&destinations={destination}&key={apiKey}";

            using (WebClient client = new WebClient())
            {
                string json = client.DownloadString(url);
                dynamic result = JsonConvert.DeserializeObject(json);

                int distance = result.rows[0].elements[0].distance.value;

                return distance;
            }
        }
    }
}
