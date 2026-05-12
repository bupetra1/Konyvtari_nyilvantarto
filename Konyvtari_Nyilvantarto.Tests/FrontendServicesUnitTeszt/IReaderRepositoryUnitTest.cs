using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Xunit;

namespace Frontend.Tests;

public class ReaderHttpTests
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
    public async Task GetReaders_ReturnsJson()
    {
        // Arrange
        var readers = new[]
        {
            new { Name = "John Doe", Address = "Budapest" }
        };

        var json = JsonSerializer.Serialize(readers);

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
    public async Task CreateReader_ReturnsSuccess()
    {
        // Arrange
        var client = CreateClient(new HttpResponseMessage(HttpStatusCode.OK));

        var reader = new
        {
            Name = "John Doe",
            Address = "Budapest"
        };

        // Act
        var response = await client.PostAsJsonAsync("api/librarian/CreateReader", reader);

        // Assert
        Assert.True(response.IsSuccessStatusCode);
    }

    [Fact]
    public async Task DeleteReader_ReturnsSuccess()
    {
        // Arrange
        var client = CreateClient(new HttpResponseMessage(HttpStatusCode.OK));

        // Act
        var response = await client.DeleteAsync("api/librarian/DeleteReader?readerId=1");

        // Assert
        Assert.True(response.IsSuccessStatusCode);
    }
}