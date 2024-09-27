using FileProcessingAPI.Interfaces;
using FileProcessingAPI.Middleware;
using FileProcessingAPI.Processors;
using FileProcessingAPI.Repositories;
using FileProcessingAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the DI container

// Register services for file processing, logging, etc.
builder.Services.AddScoped<IFileProcessorFactory, FileProcessorFactory>();
builder.Services.AddScoped<CsvFileProcessor>();
builder.Services.AddScoped<JsonFileProcessor>();
builder.Services.AddScoped<IFileLogger, FileLogger>();
builder.Services.AddScoped<IFileLogRepository, InMemoryFileLogRepository>();

// Add controllers and Swagger with API Key support
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("ApiKey", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Description = "API Key needed to access the endpoints. Example: \"X-API-KEY: {api_key}\"",
        Name = "X-API-KEY",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Scheme = "ApiKeyScheme"
    });

    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "ApiKey"
                },
                Scheme = "ApiKeyScheme",
                Name = "X-API-KEY",
                In = Microsoft.OpenApi.Models.ParameterLocation.Header
            },
            new List<string>()
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseMiddleware<ApiKeyMiddleware>();  // Custom API key middleware
app.UseAuthorization();
app.MapControllers();

app.Run();
