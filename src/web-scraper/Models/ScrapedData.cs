using CsvHelper.Configuration.Attributes;

namespace WebScraper.Models;

// Represents one scraped product row — used for both in-memory data and CSV export.
public class ScrapedData
{
    // [Name] sets the CSV column header. Without it, CsvHelper would use the property name as-is.
    [Name("Title")]
    public string Title { get; set; } = string.Empty;

    [Name("Description")]
    public string Description { get; set; } = string.Empty;

    [Name("Price")]
    public string Price { get; set; } = string.Empty;

    // "Image URL" in the CSV instead of "ImageUrl" for a more readable header.
    [Name("Image URL")]
    public string ImageUrl { get; set; } = string.Empty;
}
