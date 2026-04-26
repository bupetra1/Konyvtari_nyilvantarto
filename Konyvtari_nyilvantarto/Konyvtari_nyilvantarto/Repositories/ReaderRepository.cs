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
                        .Where(b => b.Loans.All(l => l.ReturnDate != null))
                        .Select(b => new BookDto
                        {
                            Id = b.Id,
                            Title = b.Title,
                            Author = b.Author,
                            Publisher = b.Publisher,
                            PublicationYear = b.PublicationYear
                        })
                        .ToList();
        }

        public IEnumerable<LoanDto> GetLoansByReaderId(int readerId)
        {
            bool readerExists = _dbContext.Readers.Any(r => r.Id == readerId);

            if(readerExists){
                return _dbContext.Loans
                            .Where(l => l.ReaderId == readerId)
                            .Select(l => new LoanDto
                            {
                                ReaderId = l.ReaderId,
                                ReaderName = l.Reader.Name,
                                BookId = l.BookId,
                                BookTitle = l.Book.Title,
                                BookAuthor = l.Book.Author,
                                LoanDate = l.LoanDate,
                                DueDate = l.DueDate,
                                ReturnDate = l.ReturnDate,
                                LateFee = l.LateFee
                            })
                            .ToList();
            }
            return null;
        }
    }
}