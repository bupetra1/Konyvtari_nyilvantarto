namespace Konyvtari_nyilvantarto.Repositories
{
    public class LibrarianRepository : ILibrarianRepository
    {
        private readonly AppDbContext _dbContext;

        public LibrarianRepository(AppDbContext appDbContext)
        {
            _dbContext = appDbContext;
        }
        public void CreateBook(BookDto bookDto)
        {
            var book = new Book
            {
                Title = bookDto.Title,
                Author = bookDto.Author,
                Publisher = bookDto.Publisher,
                PublicationYear = bookDto.PublicationYear
            };
            _dbContext.Books.Add(book);
            _dbContext.SaveChanges();
        }

        public void CreateLoan(LoanDto loanDto)
        {
            var loan = new Loan
            {
                LoanDate = loanDto.LoanDate,
                DueDate = loanDto.DueDate,
                LateFee = loanDto.LateFee,
                ReturnDate = loanDto.ReturnDate
            };
            _dbContext.Loans.Add(loan);
            _dbContext.SaveChanges();
        }

        public void CreateReader(ReaderDto readerDto)
        {
            var reader = new Reader
            {
                Name = readerDto.Name,
                Address = readerDto.Address,
                BirthDate = readerDto.BirthDate
            };
            _dbContext.Readers.Add(reader);
            _dbContext.SaveChanges();
        }

        public void DeleteBook()
        {
            throw new NotImplementedException();
        }

        public void DeleteLoan()
        {
            throw new NotImplementedException();
        }

        public void DeleteReader()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BookDto> GetBooks()
        {
            return _dbContext.Books
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

        public IEnumerable<LoanDto> GetLoans()
        {
            return _dbContext.Loans
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

        public IEnumerable<ReaderDto> GetReaders()
        {
            return _dbContext.Readers
                        .Select(r => new ReaderDto
                        {
                            Id = r.Id,
                            Name = r.Name,
                            Address = r.Address,
                            BirthDate = r.BirthDate
                        })
                        .ToList();
        }

        public void UpdateBook()
        {
            throw new NotImplementedException();
        }

        public void UpdateLoan()
        {
            throw new NotImplementedException();
        }

        public void UpdateReader()
        {
            throw new NotImplementedException();
        }
    }
}