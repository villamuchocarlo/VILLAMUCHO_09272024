using FileProcessingAPI.Interfaces;
using FileProcessingAPI.Models;
using Microsoft.AspNetCore.Mvc;

using Newtonsoft.Json;

namespace FileProcessingAPI.Processors {
    public class JsonFileProcessor : IFileProcessor {
        private readonly IFileLogger _fileLogger;

        public JsonFileProcessor(IFileLogger fileLogger) {
            _fileLogger = fileLogger;
        }

        public async Task<IActionResult> ProcessFile(IFormFile file) {
            using (var reader = new StreamReader(file.OpenReadStream())) {
                var jsonData = await reader.ReadToEndAsync();
                var jsonObjects = JsonConvert.DeserializeObject<List<MyJsonObject>>(jsonData);

                if (jsonObjects == null) {
                    return new BadRequestObjectResult("Invalid JSON data or empty file.");
                }

                var filteredData = jsonObjects
                    .Where(obj => obj.City == "New York")
                    .ToList();

                _fileLogger.LogFileProcessing(file.FileName, "JSON", "Data filtered");
                return new OkObjectResult(new { Result = "JSON file processed", Data = filteredData });
            }
        }
    }
}
