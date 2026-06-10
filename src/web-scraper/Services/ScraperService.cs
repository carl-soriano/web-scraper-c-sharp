using HtmlAgilityPack;
using WebScraper.Models;

namespace WebScraper.Services;

// Handles fetching a web page and extracting product data from its HTML.
public class ScraperService
{
    // Reused across requests instead of creating a new client every scrape.
    private readonly HttpClient _httpClient;

    // Optional HttpClient lets tests inject a mock; otherwise we create a real one.
    public ScraperService(HttpClient? httpClient = null)
    {
        _httpClient = httpClient ?? new HttpClient();
    }

    public async Task<List<ScrapedData>> ScrapeDataAsync(string url)
    {
        // Step 1: Download the page HTML as a string.
        var html = await _httpClient.GetStringAsync(url);

        // Step 2: Parse the raw HTML into a navigable document tree.
        var document = new HtmlDocument();
        document.LoadHtml(html);

        var scrapedData = new List<ScrapedData>();

        // Step 3: Find every product card on the page.
        // XPath: "//article[@class='product_pod']" means "all <article> tags with class product_pod".
        var products = document.DocumentNode.SelectNodes("//article[@class='product_pod']");

        // No matching elements means nothing to scrape — return an empty list.
        if (products is null)
        {
            return scrapedData;
        }

        // Used to turn relative image paths (e.g. "media/cache/...") into full URLs.
        var baseUri = new Uri(url);

        // Step 4: Loop through each product card and pull out the fields we care about.
        foreach (var product in products)
        {
            // ".//img" searches inside the current product node only (not the whole page).
            var imageSrc = product.SelectSingleNode(".//img")?.GetAttributeValue("src", string.Empty) ?? string.Empty;
            var imageUrl = string.Empty;

            // Combine the page URL with the relative image path to get a complete URL.
            if (!string.IsNullOrEmpty(imageSrc) && Uri.TryCreate(baseUri, imageSrc, out var resolvedImageUri))
            {
                imageUrl = resolvedImageUri.ToString();
            }

            scrapedData.Add(new ScrapedData
            {
                // Prefer the title attribute on the link; fall back to visible link text.
                Title = product.SelectSingleNode(".//h3/a")?.GetAttributeValue("title", string.Empty)
                    ?? product.SelectSingleNode(".//h3/a")?.InnerText.Trim()
                    ?? string.Empty,
                // Listing page has no description text, so we use the image alt text instead.
                Description = product.SelectSingleNode(".//img")?.GetAttributeValue("alt", string.Empty) ?? string.Empty,
                Price = product.SelectSingleNode(".//p[@class='price_color']")?.InnerText.Trim() ?? string.Empty,
                ImageUrl = imageUrl,
            });
        }

        return scrapedData;
    }
}
