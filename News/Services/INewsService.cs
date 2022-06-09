using News.Models;
using Newtonsoft.Json;

namespace News.Services
{
    public interface INewsService
    {
        NewsFinance GetFinanceNews();
    }

    public class NewsService : INewsService
    {
        private readonly IConfiguration _configuration;
        public NewsService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public NewsFinance GetFinanceNews()
        {
            string apiKey = _configuration.GetValue<string>("API_KEY");
            string baseUrl = _configuration.GetValue<string>("API_URL");

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                HttpResponseMessage response = client.GetAsync("?apikey=" + apiKey).Result;
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<NewsFinance>(result);
                }
                else
                {
                    return new NewsFinance()
                    {
                        Data = new List<NewsArticle>(),
                        Pagination = new Pagination()
                    };
                }
            }
        }
    }
}
