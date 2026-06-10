using WebScraper.Services;

// Default site to scrape if no URL is passed on the command line.
const string defaultUrl = "https://books.toscrape.com/";

// Where the CSV file will be written (relative to the project folder when using dotnet run).
const string outputPath = "Outputs/results.csv";

// args[0] lets you override the URL: dotnet run -- https://example.com
var url = args.Length > 0 ? args[0] : defaultUrl;

// Create the two services that handle scraping and file export.
var scraper = new ScraperService();
var exporter = new CsvExportService();

Console.WriteLine($"Scraping {url}...");

// Fetch the page and extract product data into a list of ScrapedData objects.
var results = await scraper.ScrapeDataAsync(url);

// Write that list to a CSV file using the column headers defined on ScrapedData.
await exporter.ExportAsync(results, outputPath);

Console.WriteLine($"Saved {results.Count} records to {outputPath}");
