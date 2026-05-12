using Share.Dtos;
using Xunit;

namespace Konyvtari_nyilvantarto.Tests;

public class ReaderLoanListDtoTests
{
    [Fact]
    public void GivenLateReturn_WhenLateFeeCalculated_ThenReturnsCorrectFee()
    {
        // Arrange
        var loan = new ReaderLoanListDto
        {
            LoanDate = new DateOnly(2025, 5, 1),
            DueDate = new DateOnly(2025, 5, 10),
            ReturnDate = new DateOnly(2025, 5, 15)
        };

        // Act
        var result = loan.LateFee;

        // Assert
        Assert.Equal(500, result);
    }

    [Fact]
    public void GivenNoReturnYetButOverdue_WhenLateFeeCalculated_ThenReturnsFee()
    {
        // Arrange
        var today = DateOnly.FromDateTime(DateTime.Now);

        var loan = new ReaderLoanListDto
        {
            LoanDate = today.AddDays(-20),
            DueDate = today.AddDays(-10),
            ReturnDate = null
        };

        // Act
        var result = loan.LateFee;

        // Assert
        Assert.True(result > 0);
    }

    [Fact]
    public void GivenNotOverdue_WhenLateFeeCalculated_ThenReturnsZero()
    {
        // Arrange
        var loan = new ReaderLoanListDto
        {
            LoanDate = DateOnly.FromDateTime(DateTime.Now),
            DueDate = DateOnly.FromDateTime(DateTime.Now.AddDays(5)),
            ReturnDate = null
        };

        // Act
        var result = loan.LateFee;

        // Assert
        Assert.Equal(0, result);
    }
}