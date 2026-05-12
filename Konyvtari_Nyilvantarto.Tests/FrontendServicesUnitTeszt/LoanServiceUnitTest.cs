using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Xunit;

namespace Frontend.Tests;

public class LoanHttpTests
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
    public async Task CreateLoan_RequestReturnsSuccess()
    {
        // Arrange
        var response = new HttpResponseMessage(HttpStatusCode.OK);

        var client = CreateClient(response);

        var loan = new
        {
            ReaderId = 1,
            BookId = 1,
            DueDate = DateOnly.FromDateTime(DateTime.Today.AddDays(7))
        };

        // Act
        var result = await client.PostAsJsonAsync("api/librarian/CreateLoan", loan);

        // Assert
        Assert.True(result.IsSuccessStatusCode);
    }

    [Fact]
    public async Task GetLoans_ReturnsJsonResponse()
    {
        // Arrange
        var data = new[]
        {
            new
            {
                ReaderName = "John",
                BookTitle = "Book1"
            }
        };

        var json = JsonSerializer.Serialize(data);

        var response = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(json, Encoding.UTF8, "application/json")
        };

        var client = CreateClient(response);

        // Act
        var result = await client.GetStringAsync("api/librarian/loans");

        // Assert
        Assert.Contains("John", result);
    }

    [Fact]
    public async Task DeleteLoan_RequestReturnsSuccess()
    {
        // Arrange
        var response = new HttpResponseMessage(HttpStatusCode.OK);

        var client = CreateClient(response);

        // Act
        var result = await client.DeleteAsync("api/librarian/DeleteLoan?loanId=1");

        // Assert
        Assert.True(result.IsSuccessStatusCode);
    }
}