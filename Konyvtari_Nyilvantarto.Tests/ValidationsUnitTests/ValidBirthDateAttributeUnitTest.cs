using System.ComponentModel.DataAnnotations;
using System.Data;
using Konyvtari_nyilvantarto.Validations;
using System.Reflection;
using Xunit;

namespace Konyvtari_nyilvantarto.Tests;

public class ValidBirthDateAttributeTests
{
    [Fact]
    public void ValidDate_ValidationOccurs_ReturnsSuccess()
    {
        //Arrange
        var attribute = new ValidBirthDateAttribute();
        var validDate = new DateOnly(2000, 5, 10);
        var context = new ValidationContext(new object());
        //Act
        var result = attribute.GetValidationResult(validDate, context);
        //Assert
        Assert.Equal(ValidationResult.Success, result);
    }

    [Fact]
    public void YearBefore1900_ValidationOccurs_ReturnsValidationError()
    {
        //Arrange
        var attribute = new ValidBirthDateAttribute();
        var invalidDate = new DateOnly(1800, 1, 1);
        var context = new ValidationContext(new object());
        //Act
        var result = attribute.GetValidationResult(invalidDate, context);
        //Assert
        Assert.NotNull(result);
        Assert.Equal("1900 utáni évet kell megadni!", result.ErrorMessage);
    }

    [Fact]
    public void FutureDate_ValidationOccurs_ReturnsValidationError()
    {
        //Arrange
        var attribute = new ValidBirthDateAttribute();
        var futureDate = DateOnly.FromDateTime(DateTime.Today);
        var context = new ValidationContext(new object());
        //Act
        var result = attribute.GetValidationResult(futureDate, context);
        //Assert
        Assert.NotNull(result);
        Assert.Equal("A születésnapnak a mai nap előtt kell lennie!", result.ErrorMessage);
    }
}
