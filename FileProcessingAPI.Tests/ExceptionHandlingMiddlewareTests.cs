using FileProcessingAPI.Middleware;

using Microsoft.AspNetCore.Http;

using Microsoft.Extensions.Logging;

using Moq;

namespace FileProcessingAPI.Tests {
    public class ExceptionHandlingMiddlewareTests {
        [Fact]
        public async Task InvokeAsync_ExceptionThrown_ReturnsInternalServerError() {
            var loggerMock = new Mock<ILogger<ExceptionHandlingMiddleware>>();
            var middleware = new ExceptionHandlingMiddleware(async (innerHttpContext) => {
                throw new Exception("Test exception");
            }, loggerMock.Object);

            var context = new DefaultHttpContext();
            var memoryStream = new MemoryStream();
            context.Response.Body = memoryStream;

            await middleware.InvokeAsync(context);

            Assert.Equal(StatusCodes.Status500InternalServerError, context.Response.StatusCode);

            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var reader = new StreamReader(context.Response.Body);
            var responseBody = await reader.ReadToEndAsync();

            Assert.Equal("{\"error\":\"An error occurred while processing your request.\"}", responseBody);
        }
    }
}
