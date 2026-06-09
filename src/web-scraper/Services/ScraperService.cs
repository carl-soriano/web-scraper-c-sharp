

using HtmlAgilityPack;
using WebScraper.Models;

namespace WebScraper.Services;

public class ScraperService
{
    public async Task<List<ScrapedData>> ScrapeDataAsync(string url)
    {
        var scrapedData = new List<ScrapedData>();
    }
}
