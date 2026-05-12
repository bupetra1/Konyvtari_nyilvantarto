using Share.Dtos;
using Xunit;

namespace Konyvtari_nyilvantarto.Tests;

public class ReaderBookListDtoTests
{
    [Fact]
    public void GivenBookData_WhenPropertiesAreAccessed_ThenValuesAreCorrect()
    {
        // Arrange
        var book = new ReaderBookListDto
        {
            Title = "Test Title",
            Author = "Test Author",
            Publisher = "Test Publisher",
            PublicationYear = 2020
        };

        // Act
        var title = book.Title;
        var author = book.Author;
        var publisher = book.Publisher;
        var year = book.PublicationYear;

        // Assert
        Assert.Equal("Test Title", title);
        Assert.Equal("Test Author", author);
        Assert.Equal("Test Publisher", publisher);
        Assert.Equal(2020, year);
    }
}