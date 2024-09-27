using FileProcessingAPI.Interfaces;
using FileProcessingAPI.Models;

namespace FileProcessingAPI.Services {
    public class FileLogger : IFileLogger {
        private readonly IFileLogRepository _logRepository;

        public FileLogger(IFileLogRepository logRepository) {
            _logRepository = logRepository;
        }

        public void LogFileProcessing(string fileName, string fileType, string result) {
            _logRepository.AddLog(new FileProcessingLog {
                FileName = fileName,
                FileType = fileType,
                Result = result,
                ProcessedAt = DateTime.Now
            });
        }

        public IEnumerable<FileProcessingLog> GetProcessingLogs() {
            return _logRepository.GetAllLogs();
        }
    }
}
