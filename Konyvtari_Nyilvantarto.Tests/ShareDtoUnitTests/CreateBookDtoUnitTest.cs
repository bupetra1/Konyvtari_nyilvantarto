using System.ComponentModel.DataAnnotations;
using Share.Dtos;
using Xunit;

namespace Konyvtari_nyilvantarto.Tests;

public class CreateBookDtoTests
{
    [Fact]
    public void GivenEmptyTitle_WhenValidationOccurs_ThenReturnsValidationError()
    {
        // Arrange
        var book = new CreateBookDto
        {
            Title = "",
            Author = "Test Author",
            PublicationYear = 2020
        };

        var context = new ValidationContext(book);
        var results = new List<ValidationResult>();

        // Act
        var isValid = Validator.TryValidateObject(
            book,
            context,
            results,
            true);

        // Assert
        Assert.False(isValid);

        Assert.Contains(results,
            r => r.ErrorMessage == "Book title is required!");
    }

    [Fact]
    public void GivenEmptyAuthor_WhenValidationOccurs_ThenReturnsValidationError()
    {
        // Arrange
        var book = new CreateBookDto
        {
            Title = "Test Book",
            Author = "",
            PublicationYear = 2020
        };

        var context = new ValidationContext(book);
        var results = new List<ValidationResult>();

        // Act
        var isValid = Validator.TryValidateObject(
            book,
            context,
            results,
            true);

        // Assert
        Assert.False(isValid);

        Assert.Contains(results,
            r => r.ErrorMessage == "Book author is required!");
    }

    [Fact]
    public void GivenNegativePublicationYear_WhenValidationOccurs_ThenReturnsValidationError()
    {
        // Arrange
        var book = new CreateBookDto
        {
            Title = "Test Book",
            Author = "Test Author",
            PublicationYear = -1
        };

        var context = new ValidationContext(book);
        var results = new List<ValidationResult>();

        // Act
        var isValid = Validator.TryValidateObject(
            book,
            context,
            results,
            true);

        // Assert
        Assert.False(isValid);

        Assert.Contains(results,
            r => r.ErrorMessage == "Year cannot be negative!");
    }

    [Fact]
    public void GivenValidBook_WhenValidationOccurs_ThenReturnsSuccess()
    {
        // Arrange
        var book = new CreateBookDto
        {
            Title = "Test Book",
            Author = "Test Author",
            PublicationYear = 2020
        };

        var context = new ValidationContext(book);
        var results = new List<ValidationResult>();

        // Act
        var isValid = Validator.TryValidateObject(
            book,
            context,
            results,
            true);

        // Assert
        Assert.True(isValid);
        Assert.Empty(results);
    }
}