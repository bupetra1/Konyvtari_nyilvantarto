using Microsoft.EntityFrameworkCore;
using Xunit;
using Konyvtari_nyilvantarto;

namespace Konyvtari_nyilvantarto.Tests;

public class AppDbContextTests
{
    [Fact]
    public void GivenInMemoryDatabase_WhenContextCreated_ThenCanAddAndRetrieveBooks()
    {
        // Arrange
        var context = DbTestHelper.CreateDb();

        var book = new Book
        {
            Title = "Test Book",
            Author = "Test Author"
        };

        // Act
        context.Books.Add(book);
        context.SaveChanges();

        var retrievedBook = context.Books.FirstOrDefault(b => b.Title == "Test Book");

        // Assert
        Assert.NotNull(retrievedBook);
        Assert.Equal("Test Author", retrievedBook!.Author);
    }
}