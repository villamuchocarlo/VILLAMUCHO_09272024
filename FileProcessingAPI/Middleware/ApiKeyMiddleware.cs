namespace FileProcessingAPI.Middleware {
    public class ApiKeyMiddleware {
        private readonly RequestDelegate _next;
        private const string APIKEYNAME = "X-API-KEY";
        private readonly string _apiKey;

        public ApiKeyMiddleware(RequestDelegate next, IConfiguration configuration) {
            _next = next;
            _apiKey = configuration["ApiKey"] ?? throw new ArgumentNullException(nameof(_apiKey));
        }

        public async Task InvokeAsync(HttpContext context) {
            if (!context.Request.Headers.TryGetValue(APIKEYNAME, out var extractedApiKey)) {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("API Key was not provided.");
                return;
            }

            if (!_apiKey.Equals(extractedApiKey)) {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Unauthorized client.");
                return;
            }

            await _next(context);
        }
    }
}
