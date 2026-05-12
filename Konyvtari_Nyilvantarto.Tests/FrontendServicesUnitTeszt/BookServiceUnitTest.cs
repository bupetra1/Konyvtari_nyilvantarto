using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Xunit;

namespace Frontend.Tests;

public class HttpClientTests
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
    public async Task CreateBook_RequestReturnsSuccess()
    {
        // Arrange
        var response = new HttpResponseMessage(HttpStatusCode.OK);

        var client = CreateClient(response);

        var book = new
        {
            Title = "Test",
            Author = "Author"
        };

        // Act
        var result = await client.PostAsJsonAsync("api/librarian/CreateBook", book);

        // Assert
        Assert.True(result.IsSuccessStatusCode);
    }

    [Fact]
    public async Task GetBooks_ReturnsJsonResponse()
    {
        // Arrange
        var data = new[]
        {
            new { Title = "Book1", Author = "A1" }
        };

        var json = JsonSerializer.Serialize(data);

        var response = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(json, Encoding.UTF8, "application/json")
        };

        var client = CreateClient(response);

        // Act
        var result = await client.GetStringAsync("api/librarian/books");

        // Assert
        Assert.Contains("Book1", result);
    }
}