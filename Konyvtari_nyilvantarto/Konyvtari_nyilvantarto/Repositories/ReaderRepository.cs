using Microsoft.EntityFrameworkCore;
using Share.Dtos;

namespace Konyvtari_nyilvantarto.Repositories
{
    /// <summary>
    /// Provides implementation for querying reader-specific library data.
    /// </summary>
    public class ReaderRepository : IReaderRepository
    {
        /// <summary>
        /// The database context used for data access operations.
        /// </summary>
        private readonly AppDbContext _dbContext;

        /// <summary>
        /// Constructor for the <see cref="ReaderRepository"/> class.
        /// </summary>
        /// <param name="appDbContext">The database context used to perform data operations.</param>
        public ReaderRepository(AppDbContext appDbContext)
        {
            _dbContext = appDbContext;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<ReaderBookListDto>> GetAvailableBooksAsync()
        {
            return await _dbContext.Books
                        .Where(b => b.Loans.All(l => l.ReturnDate != null))
                        .Select(b => new ReaderBookListDto
                        {
                            Title = b.Title,
                            Author = b.Author,
                            Publisher = b.Publisher,
                            PublicationYear = b.PublicationYear
                        })
                        .ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<ReaderLoanListDto>>? GetLoansByReaderIdAsync(int readerId)
        {
            bool readerExists = await _dbContext.Readers.AnyAsync(r => r.Id == readerId);

            if(readerExists){
                return await _dbContext.Loans
                            .Where(l => l.ReaderId == readerId)
                            .Select(l => new ReaderLoanListDto
                            {
                                ReaderName = l.Reader.Name,
                                BookTitle = l.Book.Title,
                                BookAuthor = l.Book.Author,
                                LoanDate = l.LoanDate,
                                DueDate = l.DueDate,
                                ReturnDate = l.ReturnDate
                            })
                            .ToListAsync();
            }
            return null;
        }
    }
}