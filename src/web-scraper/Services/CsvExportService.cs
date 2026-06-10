using System.Globalization;
using CsvHelper;
using WebScraper.Models;

namespace WebScraper.Services;

public class CsvExportService
{
    public async Task ExportAsync(IEnumerable<ScrapedData> data, string filePath)
    {
        var directory = Path.GetDirectoryName(filePath);
        if (!string.IsNullOrEmpty(directory))
        {
            Directory.CreateDirectory(directory);
        }

        await using var writer = new StreamWriter(filePath);
        await using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
        await csv.WriteRecordsAsync(data);
    }
}
