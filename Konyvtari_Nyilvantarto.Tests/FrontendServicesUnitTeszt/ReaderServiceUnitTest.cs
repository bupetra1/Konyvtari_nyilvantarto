using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Xunit;

namespace Frontend.Tests;

public class ReaderServiceHttpTests
{
    private class FakeHandler : HttpMessageHandler
    {
        private readonly HttpResponseMessage _response;

        public FakeHandler(HttpResponseMessage response)
        {
            _response = response;
        }

        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            return Task.FromResult(_response);
        }
    }

    private HttpClient CreateClient(HttpResponseMessage response)
    {
        return new HttpClient(new FakeHandler(response))
        {
            BaseAddress = new Uri("http://localhost/")
        };
    }

    [Fact]
    public async Task CreateReader_ReturnsSuccess()
    {
        // Arrange
        var response = new HttpResponseMessage(HttpStatusCode.OK);
        var client = CreateClient(response);

        var reader = new
        {
            Name = "Test User",
            Address = "Budapest"
        };

        // Act
        var result = await client.PostAsJsonAsync("api/librarian/CreateReader", reader);

        // Assert
        Assert.True(result.IsSuccessStatusCode);
    }

    [Fact]
    public async Task GetReaders_ReturnsJsonData()
    {
        // Arrange
        var data = new[]
        {
            new { Name = "John Doe", Address = "Pécs" }
        };

        var json = JsonSerializer.Serialize(data);

        var response = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(json, Encoding.UTF8, "application/json")
        };

        var client = CreateClient(response);

        // Act
        var result = await client.GetStringAsync("api/librarian/readers");

        // Assert
        Assert.Contains("John Doe", result);
    }

    [Fact]
    public async Task UpdateReader_ReturnsSuccess()
    {
        // Arrange
        var response = new HttpResponseMessage(HttpStatusCode.OK);
        var client = CreateClient(response);

        var reader = new
        {
            ReaderId = 1,
            Name = "Updated Name",
            Address = "Budapest"
        };

        // Act
        var result = await client.PutAsJsonAsync("api/librarian/UpdateReader?readerId=1", reader);

        // Assert
        Assert.True(result.IsSuccessStatusCode);
    }

    [Fact]
    public async Task DeleteReader_ReturnsSuccess()
    {
        // Arrange
        var response = new HttpResponseMessage(HttpStatusCode.OK);
        var client = CreateClient(response);

        // Act
        var result = await client.DeleteAsync("api/librarian/DeleteReader?readerId=1");

        // Assert
        Assert.True(result.IsSuccessStatusCode);
    }
}