using System.ComponentModel.DataAnnotations;
using Share.Dtos;
using Xunit;

namespace Konyvtari_nyilvantarto.Tests;

public class CreateReaderDtoTests
{
    [Fact]
    public void GivenEmptyName_WhenValidationOccurs_ThenReturnsValidationError()
    {
        // Arrange
        var reader = new CreateReaderDto
        {
            Name = "",
            Address = "Test Address",
            BirthDate = new DateOnly(2000, 1, 1)
        };

        var context = new ValidationContext(reader);
        var results = new List<ValidationResult>();

        // Act
        var isValid = Validator.TryValidateObject(
            reader,
            context,
            results,
            true);

        // Assert
        Assert.False(isValid);

        Assert.Contains(results,
            r => r.ErrorMessage == "Name is required!");
    }

    [Fact]
    public void GivenEmptyAddress_WhenValidationOccurs_ThenReturnsValidationError()
    {
        // Arrange
        var reader = new CreateReaderDto
        {
            Name = "Test Reader",
            Address = "",
            BirthDate = new DateOnly(2000, 1, 1)
        };

        var context = new ValidationContext(reader);
        var results = new List<ValidationResult>();

        // Act
        var isValid = Validator.TryValidateObject(
            reader,
            context,
            results,
            true);

        // Assert
        Assert.False(isValid);

        Assert.Contains(results,
            r => r.ErrorMessage == "Address is required!");
    }

    [Fact]
    public void GivenFutureBirthDate_WhenValidationOccurs_ThenReturnsValidationError()
    {
        // Arrange
        var reader = new CreateReaderDto
        {
            Name = "Test Reader",
            Address = "Test Address",
            BirthDate = DateOnly.FromDateTime(DateTime.Now.AddDays(1))
        };

        var context = new ValidationContext(reader);
        var results = new List<ValidationResult>();

        // Act
        var isValid = Validator.TryValidateObject(
            reader,
            context,
            results,
            true);

        // Assert
        Assert.False(isValid);

        Assert.Contains(results,
            r => r.ErrorMessage == "Birthdate cannot be in the future!");
    }

    [Fact]
    public void GivenValidReader_WhenValidationOccurs_ThenReturnsSuccess()
    {
        // Arrange
        var reader = new CreateReaderDto
        {
            Name = "Test Reader",
            Address = "Test Address",
            BirthDate = new DateOnly(2000, 1, 1)
        };

        var context = new ValidationContext(reader);
        var results = new List<ValidationResult>();

        // Act
        var isValid = Validator.TryValidateObject(
            reader,
            context,
            results,
            true);

        // Assert
        Assert.True(isValid);
        Assert.Empty(results);
    }
}