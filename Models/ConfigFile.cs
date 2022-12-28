
namespace CryptoPricePrediction.Models
{
    public class Authentication
    {
        public string Key { get; set; } = string.Empty;
        public string Secret { get; set; } = string.Empty;
    }

    public class Root
    {
        public Authentication Authentication { get; set; } = new();
        public Settings Settings { get; set; } = new();
    }

    public class Settings
    {
        public int BuyAmount { get; set; }
        public bool TestMode { get; set; }
    }


}
