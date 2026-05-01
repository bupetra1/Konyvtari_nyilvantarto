using Microsoft.EntityFrameworkCore;

namespace Konyvtari_nyilvantarto.Repositories
{
    public class ReaderRepository : IReaderRepository
    {
        private readonly AppDbContext _dbContext;

        public ReaderRepository(AppDbContext appDbContext)
        {
            _dbContext = appDbContext;
        }
        public async Task<IEnumerable<BookDto>> GetAvailableBooksAsync()
        {
            return await _dbContext.Books
                        .Where(b => b.Loans.All(l => l.ReturnDate != null))
                        .Select(b => new BookDto
                        {
                            Title = b.Title,
                            Author = b.Author,
                            Publisher = b.Publisher,
                            PublicationYear = b.PublicationYear
                        })
                        .ToListAsync();
        }

        public async Task<IEnumerable<LoanDto>>? GetLoansByReaderIdAsync(int readerId)
        {
            bool readerExists = await _dbContext.Readers.AnyAsync(r => r.Id == readerId);

            if(readerExists){
                return await _dbContext.Loans
                            .Where(l => l.ReaderId == readerId)
                            .Select(l => new LoanDto
                            {
                                ReaderName = l.Reader.Name,
                                BookTitle = l.Book.Title,
                                BookAuthor = l.Book.Author,
                                LoanDate = l.LoanDate,
                                DueDate = l.DueDate,
                                ReturnDate = l.ReturnDate,
                            })
                            .ToListAsync();
            }
            return null;
        }
    }
}