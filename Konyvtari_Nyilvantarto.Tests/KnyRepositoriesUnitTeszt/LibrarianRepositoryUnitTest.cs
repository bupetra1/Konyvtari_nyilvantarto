using Konyvtari_nyilvantarto.Repositories;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using Xunit;

namespace Konyvtari_nyilvantarto.Tests;

public class LibrarianRepositoryTests
{
    [Fact]
    public async Task GivenBookWithoutActiveLoan_WhenCheckingAvailability_ThenReturnsTrue()
    {
        // Arrange
        var dbContextMock = Substitute.For<AppDbContext>();

        var loans = new List<Loan>().AsQueryable();

        var loanDbSet = Substitute.For<DbSet<Loan>, IQueryable<Loan>>();
        ((IQueryable<Loan>)loanDbSet).Provider.Returns(loans.Provider);
        ((IQueryable<Loan>)loanDbSet).Expression.Returns(loans.Expression);
        ((IQueryable<Loan>)loanDbSet).ElementType.Returns(loans.ElementType);
        ((IQueryable<Loan>)loanDbSet).GetEnumerator().Returns(loans.GetEnumerator());

        dbContextMock.Loans.Returns(loanDbSet);

        var repository = new LibrarianRepository(dbContextMock);

        // Act
        var result = await repository.IsBookAvailableAsync(1);

        // Assert
        Assert.True(result);
    }
}