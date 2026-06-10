using System.Globalization;
using CsvHelper;
using WebScraper.Models;

namespace WebScraper.Services;

// Writes a collection of ScrapedData objects to a CSV file on disk.
public class CsvExportService
{
    public async Task ExportAsync(IEnumerable<ScrapedData> data, string filePath)
    {
        // Ensure the output folder exists (e.g. Outputs/) before writing the file.
        var directory = Path.GetDirectoryName(filePath);
        if (!string.IsNullOrEmpty(directory))
        {
            Directory.CreateDirectory(directory);
        }

        // StreamWriter opens (or creates) the CSV file for writing.
        await using var writer = new StreamWriter(filePath);

        // CsvWriter serializes objects to CSV rows using the [Name] attributes on ScrapedData.
        // InvariantCulture keeps number/date formatting consistent regardless of system locale.
        await using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);

        // Writes a header row first, then one row per ScrapedData item.
        await csv.WriteRecordsAsync(data);
    }
}
