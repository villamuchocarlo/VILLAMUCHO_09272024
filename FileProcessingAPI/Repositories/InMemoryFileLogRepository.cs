using FileProcessingAPI.Interfaces;
using FileProcessingAPI.Models;

namespace FileProcessingAPI.Repositories {
    public class InMemoryFileLogRepository : IFileLogRepository {
        private static readonly List<FileProcessingLog> _logs = new List<FileProcessingLog>();

        public void AddLog(FileProcessingLog log) {
            _logs.Add(log);
        }

        public IEnumerable<FileProcessingLog> GetAllLogs() {
            return _logs;
        }
    }
}
