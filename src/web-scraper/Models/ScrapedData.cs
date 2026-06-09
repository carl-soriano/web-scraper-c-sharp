using CsvHelper.Configuration.Attributes;

namespace WebScraper.Models;

public class ScrapedData
{
    [Name("Title")]
    public string Title { get; set; } = string.Empty;

    [Name("Description")]
    public string Description { get; set; } = string.Empty;

    [Name("Price")]
    public string Price { get; set; } = string.Empty;

    [Name("Image URL")]
    public string ImageUrl { get; set; } = string.Empty;
}

