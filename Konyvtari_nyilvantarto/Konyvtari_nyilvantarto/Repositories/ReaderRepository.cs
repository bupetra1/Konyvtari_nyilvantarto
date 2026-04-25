namespace Konyvtari_nyilvantarto.Repositories
{
    public class ReaderRepository : IReaderRepository
    {
        public AppDbContext _dbContext;

        public ReaderRepository(AppDbContext appDbContext)
        {
            _dbContext = appDbContext;
        }
        public IEnumerable<BookDto> GetAvailableBooks()
        {
            return _dbContext.Books
                        .Where(x => x.Loans.All(x => x.ReturnDate == null))
                        .Select(x => new BookDto
                        {
                            Id = x.Id,
                            Title = x.Title,
                            Author = x.Author,
                            Publisher = x.Publisher,
                            PublicationYear = x.PublicationYear
                        })
                        .ToList();
        }

        public IEnumerable<LoanDto> GetLoans(int studentId)
        {
            return _dbContext.Loans
                        .Where(x => x.ReaderId == studentId)
                        .Select(x => new LoanDto
                        {
                            ReaderId = x.ReaderId,
                            ReaderName = x.Reader.Name,
                            BookId = x.BookId,
                            BookTitle = x.Book.Title,
                            BookAuthor = x.Book.Author,
                            LoanDate = x.LoanDate,
                            DueDate = x.DueDate,
                            ReturnDate = x.ReturnDate,
                            LateFee = x.LateFee
                        })
                        .ToList();
        }
    }
}