using WebScraper.Services;

const string defaultUrl = "https://books.toscrape.com/";
const string outputPath = "Outputs/results.csv";

var url = args.Length > 0 ? args[0] : defaultUrl;

var scraper = new ScraperService();
var exporter = new CsvExportService();

Console.WriteLine($"Scraping {url}...");

var results = await scraper.ScrapeDataAsync(url);

await exporter.ExportAsync(results, outputPath);

Console.WriteLine($"Saved {results.Count} records to {outputPath}");
