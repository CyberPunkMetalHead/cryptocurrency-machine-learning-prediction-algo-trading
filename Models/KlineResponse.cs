
namespace CryptoPricePrediction.Models
{
    public class KlineResponse
    {
        public DateTime OpenTime { get; set; }
        public string OpenPrice { get; set; } = string.Empty;
        public string ClosePrice { get; set; } = string.Empty;
    }
}
