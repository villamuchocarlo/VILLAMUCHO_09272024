using FileProcessingAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FileProcessingAPI.Processors {
    public class CsvFileProcessor : IFileProcessor {
        private readonly IFileLogger _fileLogger;

        public CsvFileProcessor(IFileLogger fileLogger) {
            _fileLogger = fileLogger;
        }

        public async Task<string> ProcessFileAsync(IFormFile file) {
            using (var reader = new StreamReader(file.OpenReadStream())) {
                var total = 0.0;
                var count = 0;
                bool isHeader = true;
                while (!reader.EndOfStream) {
                    var line = await reader.ReadLineAsync();

                    if (string.IsNullOrEmpty(line)) {
                        continue;
                    }

                    var values = line.Split(",");

                    if (isHeader) {
                        isHeader = false;
                        continue;
                    }

                    if (double.TryParse(values[1], out double numericValue)) {
                        total += numericValue;
                        count++;
                    }
                }
                var average = total / count;
                _fileLogger.LogFileProcessing(file.FileName, "CSV", average.ToString());
                return $"CSV file processed. Average: {average}";
            }
        }
    }
}
