using System.Diagnostics;

using FileProcessingAPI.Interfaces;
using FileProcessingAPI.Models;

namespace FileProcessingAPI.Services {
    public class FileProcessingService : IFileProcessingService {
        private readonly IFileProcessorFactory _fileProcessorFactory;
        private readonly IFileLogger _fileLogger;
        private static int _fileProcessedCounter = 0;

        public FileProcessingService(IFileProcessorFactory fileProcessorFactory, IFileLogger fileLogger) {
            _fileProcessorFactory = fileProcessorFactory;
            _fileLogger = fileLogger;
        }

        public async Task<(string result, long processingTime, int filesProcessed)> ProcessFileAsync(IFormFile file) {
            var extension = Path.GetExtension(file.FileName).ToLower();
            var supportedTypes = new[] { ".csv", ".json" };

            if (!supportedTypes.Contains(extension)) {
                throw new NotSupportedException($"File type {extension} is not supported.");
            }

            var processor = _fileProcessorFactory.CreateProcessor(extension);

            var stopwatch = Stopwatch.StartNew();
            var result = await processor.ProcessFileAsync(file);
            stopwatch.Stop();

            var processingTime = stopwatch.ElapsedMilliseconds;
            _fileProcessedCounter++;

            _fileLogger.LogFileProcessing(file.FileName, extension, "Processing completed");

            return (result, processingTime, _fileProcessedCounter);
        }

        public int GetTotalFilesProcessed() {
            return _fileProcessedCounter;
        }

        public IEnumerable<FileProcessingLog> GetProcessingLogs() {
            return _fileLogger.GetProcessingLogs();
        }
    }
}