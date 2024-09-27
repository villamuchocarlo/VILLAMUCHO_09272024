using FileProcessingAPI.Models;

namespace FileProcessingAPI.Interfaces {
    public interface IFileLogRepository {
        void AddLog(FileProcessingLog log);
        IEnumerable<FileProcessingLog> GetAllLogs();
    }
}
