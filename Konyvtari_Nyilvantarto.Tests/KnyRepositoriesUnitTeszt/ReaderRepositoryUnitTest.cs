using Konyvtari_nyilvantarto.Repositories;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using Share.Dtos;
using Xunit;

namespace Konyvtari_nyilvantarto.Tests;

public class ReaderRepositoryUnitTests
{
    [Fact]
    public async Task GivenExistingReader_WhenGettingLoans_ThenReturnsLoanList()
    {
        // Arrange
        var dbContextMock = Substitute.For<AppDbContext>();

        var reader = new Reader
        {
            Id = 1,
            Name = "Test Reader"
        };

        var book = new Book
        {
            Id = 1,
            Title = "Test Book",
            Author = "Test Author"
        };

        var loans = new List<Loan>
        {
            new Loan
            {
                Id = 1,
                ReaderId = 1,
                Reader = reader,
                Book = book,
                LoanDate = DateOnly.FromDateTime(DateTime.Today),
                DueDate = DateOnly.FromDateTime(DateTime.Today.AddDays(7))
            }
        }.AsQueryable();

        var loanDbSet = Substitute.For<DbSet<Loan>, IQueryable<Loan>>();

        ((IQueryable<Loan>)loanDbSet).Provider.Returns(loans.Provider);
        ((IQueryable<Loan>)loanDbSet).Expression.Returns(loans.Expression);
        ((IQueryable<Loan>)loanDbSet).ElementType.Returns(loans.ElementType);
        ((IQueryable<Loan>)loanDbSet).GetEnumerator().Returns(loans.GetEnumerator());

        dbContextMock.Loans.Returns(loanDbSet);

        var readers = new List<Reader>
        {
            reader
        }.AsQueryable();

        var readerDbSet = Substitute.For<DbSet<Reader>, IQueryable<Reader>>();

        ((IQueryable<Reader>)readerDbSet).Provider.Returns(readers.Provider);
        ((IQueryable<Reader>)readerDbSet).Expression.Returns(readers.Expression);
        ((IQueryable<Reader>)readerDbSet).ElementType.Returns(readers.ElementType);
        ((IQueryable<Reader>)readerDbSet).GetEnumerator().Returns(readers.GetEnumerator());

        dbContextMock.Readers.Returns(readerDbSet);

        var repository = new ReaderRepository(dbContextMock);

        // Act
        var result = await repository.GetLoansByReaderIdAsync(1);

        // Assert
        Assert.NotNull(result);
    }
}