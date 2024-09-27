using FileProcessingAPI.Models;

namespace FileProcessingAPI.Interfaces {
    public interface IFileLogger {
        void LogFileProcessing(string fileName, string fileType, string result);
        IEnumerable<FileProcessingLog> GetProcessingLogs();
    }
}
