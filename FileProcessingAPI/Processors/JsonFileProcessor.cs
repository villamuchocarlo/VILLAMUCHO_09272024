using FileProcessingAPI.Interfaces;
using FileProcessingAPI.Models;
using Newtonsoft.Json;

namespace FileProcessingAPI.Processors
{
    public class JsonFileProcessor : IFileProcessor
    {
        private readonly IFileLogger _fileLogger;

        public JsonFileProcessor(IFileLogger fileLogger)
        {
            _fileLogger = fileLogger;
        }

        public async Task<string> ProcessFileAsync(IFormFile file)
        {
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                var jsonData = await reader.ReadToEndAsync();
                var jsonObjects = JsonConvert.DeserializeObject<List<MyJsonObject>>(jsonData);

                if (jsonObjects == null || !jsonObjects.Any())
                {
                    _fileLogger.LogFileProcessing(file.FileName, "JSON", "Invalid or empty JSON data");
                    return "Invalid JSON data or empty file.";
                }

                var filteredData = jsonObjects
                    .Where(obj => obj.City == "New York")
                    .ToList();

                _fileLogger.LogFileProcessing(file.FileName, "JSON", "Data filtered");

                return $"JSON file processed, filtered data count = {filteredData.Count}";
            }
        }
    }
}
