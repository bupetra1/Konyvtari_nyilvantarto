using System.ComponentModel.DataAnnotations;
using System.Data;
using Konyvtari_nyilvantarto.Validations;
using System.Reflection;
using Xunit;

namespace Konyvtari_nyilvantarto.Tests;

public class ValidPublicationYearAttributeUnitTest
{
    [Fact]
    public void ValidYear_ValidationOccurs_ReturnsSuccess()
    {
        // Arrange
        var attribute = new ValidPublicationYearAttribute();
        var year = 2000;
        var context = new ValidationContext(new object());
        // Act
        var result = attribute.GetValidationResult(year, context);
        // Assert
        Assert.Equal(ValidationResult.Success, result);
    }

    [Fact]
    public void ZeroYear_ValidationOccurs_ReturnsError()
    {
        // Arrange
        var attribute = new ValidPublicationYearAttribute();
        var year = 0;
        var context = new ValidationContext(new object());
        // Act
        var result = attribute.GetValidationResult(year, context);
        // Assert
        Assert.Equal(ValidationResult.Success, result);
    }

    [Fact]
    public void NegativeYear_ValidationOccurs_ReturnsError()
    {
        // Arrange
        var attribute = new ValidPublicationYearAttribute();
        var year = -1;
        var context = new ValidationContext(new object());
        // Act
        var result = attribute.GetValidationResult(year, context);
        // Assert
        Assert.NotEqual(ValidationResult.Success, result);
    }
}
