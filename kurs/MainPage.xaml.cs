using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Net;
using System.Text.Json;

namespace kurs
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            DateTime dzis = DateTime.Now;
            date.MaximumDate = dzis;
        }

        private void CurrencyClicked(object sender, EventArgs e)
        {
            string dateSelected = date.Date.ToString("yyyy-MM-dd");
            string currency = currencyValue.Text.ToLower();
            string url = $"https://api.nbp.pl/api/exchangerates/rates/c/{currency}/{dateSelected}/?format=json";
            string json;

            using (var webClient = new WebClient())
            {
                json = webClient.DownloadString(url);
            }

            Currency c = JsonSerializer.Deserialize<Currency>(json);

            string s = $"Nazwa waluty: {c.currency}\n";
            s += $"Kod waluty:  {c.code}\n";
            s += $"Data: {c.rates[0].effectiveDate}\n";
            s += $"Cena skupu: {c.rates[0].bid}\n";
            s += $"Cena sprzedaży: {c.rates[0].ask}\n";
            kurs.Text = s;
        }

    }


    public class Currency
    {
        public string? table { get; set; }
        public string? currency { get; set; }
        public string? code { get; set; }
        public IList<Rate> rates { get; set; }

    }

    public class Rate
    {
        public string? no { get; set; }
        public string? effectiveDate { get; set; }
        public double? bid { get; set; }
        public double? ask { get; set; }
    }
}

