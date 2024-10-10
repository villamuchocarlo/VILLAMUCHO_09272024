using FileProcessingAPI.Models;

namespace FileProcessingAPI.Interfaces {
    public interface IFileProcessingService {
        Task<(string result, long processingTime, int filesProcessed)> ProcessFileAsync(IFormFile file);
        int GetTotalFilesProcessed();
        IEnumerable<FileProcessingLog> GetProcessingLogs();
    }
}
