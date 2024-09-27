using FileProcessingAPI.Interfaces;
using FileProcessingAPI.Processors;

namespace FileProcessingAPI.Services {
    public class FileProcessorFactory : IFileProcessorFactory {
        private readonly IServiceProvider _serviceProvider;

        public FileProcessorFactory(IServiceProvider serviceProvider) {
            _serviceProvider = serviceProvider;
        }

        public IFileProcessor CreateProcessor(string fileExtension) {
            return fileExtension.ToLower() switch {
                ".csv" => _serviceProvider.GetRequiredService<CsvFileProcessor>(),
                ".json" => _serviceProvider.GetRequiredService<JsonFileProcessor>(),
                _ => throw new NotSupportedException("File type not supported")
            };
        }
    }
}
