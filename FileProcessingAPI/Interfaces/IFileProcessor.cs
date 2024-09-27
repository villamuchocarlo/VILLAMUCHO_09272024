using Microsoft.AspNetCore.Mvc;

namespace FileProcessingAPI.Interfaces {
    public interface IFileProcessor {
        Task<IActionResult> ProcessFile(IFormFile file);
    }
}
