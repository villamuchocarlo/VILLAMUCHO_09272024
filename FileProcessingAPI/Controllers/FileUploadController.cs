using FileProcessingAPI.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace FileProcessingAPI.Controllers {

    [Route("api/[controller]")]
    [ApiController]
    public class FileUploadController : ControllerBase {
        private readonly IFileProcessorFactory _fileProcessorFactory;
        private readonly IFileLogger _fileLogger;

        public FileUploadController(IFileProcessorFactory fileProcessorFactory, IFileLogger fileLogger) {
            _fileProcessorFactory = fileProcessorFactory;
            _fileLogger = fileLogger;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile(IFormFile file) {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            var extension = Path.GetExtension(file.FileName).ToLower();

            try {
                var processor = _fileProcessorFactory.CreateProcessor(extension);
                return await processor.ProcessFile(file);
            }
            catch (NotSupportedException) {
                return BadRequest("Unsupported file type.");
            }
        }

        [HttpGet("report")]
        public IActionResult GetProcessingReport() {
            var logs = _fileLogger.GetProcessingLogs();
            return Ok(logs);
        }
    }
}
