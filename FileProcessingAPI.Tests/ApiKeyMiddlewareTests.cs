using FileProcessingAPI.Middleware;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

using Moq;

public class ApiKeyMiddlewareTests
{
    private readonly Mock<RequestDelegate> _nextMock;

    public ApiKeyMiddlewareTests()
    {
        _nextMock = new Mock<RequestDelegate>();
    }

    [Fact]
    public async Task Middleware_ValidApiKey_ShouldProceed()
    {
        var apiKey = "12345-abcde-67890-fghij-12345";
        var configMock = new Mock<IConfiguration>();

        configMock.Setup(x => x["ApiKey"]).Returns(apiKey);

        var httpContext = new DefaultHttpContext();
        httpContext.Request.Headers["X-API-KEY"] = apiKey;

        var middleware = new ApiKeyMiddleware(_nextMock.Object, configMock.Object);

        await middleware.InvokeAsync(httpContext);

        _nextMock.Verify(x => x(httpContext), Times.Once);
    }

    [Fact]
    public async Task Middleware_InvalidApiKey_ShouldReturnUnauthorized()
    {
        var apiKey = "12345-abcde-67890-fghij-12345";
        var configMock = new Mock<IConfiguration>();

        configMock.Setup(x => x["ApiKey"]).Returns(apiKey);

        var httpContext = new DefaultHttpContext();
        httpContext.Request.Headers["X-API-KEY"] = "invalid-key"; // Incorrect API Key

        var middleware = new ApiKeyMiddleware(_nextMock.Object, configMock.Object);

        await middleware.InvokeAsync(httpContext);

        Assert.Equal(StatusCodes.Status401Unauthorized, httpContext.Response.StatusCode);
        _nextMock.Verify(x => x(httpContext), Times.Never);
    }
}
