namespace FileProcessingAPI.Interfaces {
    public interface IFileProcessorFactory {
        IFileProcessor CreateProcessor(string fileExtension);
    }
}
