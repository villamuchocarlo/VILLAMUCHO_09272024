using FileProcessingAPI.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace FileProcessingAPI.Controllers {

    [Route("api/[controller]")]
    [ApiController]
    public class FileUploadController : ControllerBase {
        private readonly IFileProcessingService _fileProcessingService;
        private readonly ILogger<FileUploadController> _logger;

        public FileUploadController(IFileProcessingService fileProcessingService, ILogger<FileUploadController> logger) {
            _fileProcessingService = fileProcessingService;
            _logger = logger;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile(IFormFile file) {
            if (file == null || file.Length == 0) {
                _logger.LogWarning("No file uploaded.");
                return BadRequest(new { status = 400, error = "Bad Request", message = "No file uploaded." });
            }

            try {
                var (result, processingTime, filesProcessed) = await _fileProcessingService.ProcessFileAsync(file);

                _logger.LogInformation("File processed: {fileName} in {processingTime} ms. Total files processed: {filesProcessed}", file.FileName, processingTime, filesProcessed);

                return Ok(new { status = 200, message = "File processed successfully.", data = result, timeInMs = processingTime, filesProcessed });
            }
            catch (NotSupportedException ex) {
                _logger.LogWarning(ex.Message);
                return StatusCode(StatusCodes.Status415UnsupportedMediaType, new { status = 415, error = "Unsupported Media Type", message = ex.Message });
            }
            catch (FileNotFoundException ex) {
                _logger.LogError(ex, "File not found: {fileName}", file.FileName);
                return NotFound(new { status = 404, error = "File Not Found", message = ex.Message });
            }
            catch (Exception ex) {
                _logger.LogError(ex, "An unexpected error occurred while processing file {fileName}", file.FileName);
                throw;
            }
        }

        [HttpGet("report")]
        public IActionResult GetProcessingReport() {
            var totalFilesProcessed = _fileProcessingService.GetTotalFilesProcessed();
            var logs = _fileProcessingService.GetProcessingLogs();

            return Ok(new { totalFilesProcessed, logs });
        }
    }
}
