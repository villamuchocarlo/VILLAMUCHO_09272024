using Microsoft.AspNetCore.Mvc;

namespace FileProcessingAPI.Interfaces {
    public interface IFileProcessor {
        Task<string> ProcessFileAsync(IFormFile file);
    }
}
