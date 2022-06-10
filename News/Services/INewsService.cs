using News.Models;
using Newtonsoft.Json;

namespace News.Services
{
    public interface INewsService
    {
        NewsFinance GetFinanceNews(int offset);
    }

    public class NewsService : INewsService
    {
        private readonly IConfiguration _configuration;
        public NewsService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public NewsFinance GetFinanceNews(int offset)
        {
            string apiKey = _configuration.GetValue<string>("API_KEY");
            string baseUrl = _configuration.GetValue<string>("API_URL");

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                var parameters = string.Format("?apikey={0}&offset={1}&data={2}&sort={3}", apiKey, offset, "today", "desc");
                HttpResponseMessage response = client.GetAsync(parameters).Result;
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
