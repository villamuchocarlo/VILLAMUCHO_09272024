namespace FileProcessingAPI.Models {
    public class FileProcessingLog {
        public string FileName { get; set; } = string.Empty;
        public string FileType { get; set; } = string.Empty;
        public string Result { get; set; } = string.Empty;
        public DateTime ProcessedAt { get; set; }
    }
}
