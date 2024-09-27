using FileProcessingAPI.Interfaces;
using FileProcessingAPI.Models;
using FileProcessingAPI.Processors;

using Microsoft.AspNetCore.Http;

using Moq;

using Newtonsoft.Json;

public class JsonFileProcessorTests {
    private readonly Mock<IFileLogger> _fileLoggerMock;
    private readonly JsonFileProcessor _jsonProcessor;

    public JsonFileProcessorTests() {
        _fileLoggerMock = new Mock<IFileLogger>();
        _jsonProcessor = new JsonFileProcessor(_fileLoggerMock.Object);
    }

    [Fact]
    public async Task ProcessFile_ShouldReturnFilteredData() {
        var jsonContent = JsonConvert.SerializeObject(new List<MyJsonObject>
        {
            new MyJsonObject { ID = 1, Name = "John", Age = 28, City = "New York" },
            new MyJsonObject { ID = 2, Name = "Jane", Age = 32, City = "Los Angeles" },
            new MyJsonObject { ID = 3, Name = "Sara", Age = 25, City = "New York" }
        });

        var fileMock = new Mock<IFormFile>();
        var ms = new MemoryStream();
        var writer = new StreamWriter(ms);
        await writer.WriteAsync(jsonContent);
        await writer.FlushAsync();
        ms.Position = 0;
        fileMock.Setup(f => f.OpenReadStream()).Returns(ms);
        fileMock.Setup(f => f.FileName).Returns("test.json");

        var result = await _jsonProcessor.ProcessFile(fileMock.Object);

        _fileLoggerMock.Verify(x => x.LogFileProcessing("test.json", "JSON", "Data filtered"), Times.Once);
        Assert.NotNull(result);
    }
}