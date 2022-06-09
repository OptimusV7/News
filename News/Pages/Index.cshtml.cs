using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using News.Models;
using News.Services;

namespace News.Pages
{
    public class IndexModel : PageModel
    {
        public NewsFinance news;
        private readonly ILogger<IndexModel> _logger;
        private readonly INewsService _newsService;

        public IndexModel(ILogger<IndexModel> logger, INewsService newsService)
        {
            _logger = logger;
            _newsService = newsService;
        }

        public void OnGet()
        {
            news = _newsService.GetFinanceNews();
        }
    }
}