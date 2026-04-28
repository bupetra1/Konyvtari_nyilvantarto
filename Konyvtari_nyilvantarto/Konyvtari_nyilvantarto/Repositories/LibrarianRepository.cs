using Microsoft.EntityFrameworkCore;

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
            var reader = _dbContext.Readers.Find(loanDto.ReaderId);
            var book = _dbContext.Books.Find(loanDto.BookId);
            if(reader is not null && book is not null)
            {
                var loan = new Loan
                {
                    ReaderId = loanDto.ReaderId,
                    BookId = loanDto.BookId,
                    LoanDate = loanDto.LoanDate,
                    DueDate = loanDto.DueDate,
                    LateFee = loanDto.LateFee,
                    ReturnDate = loanDto.ReturnDate
                };
                _dbContext.Loans.Add(loan);
                _dbContext.SaveChanges();
            }

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

        public void DeleteBook(int bookId)
        {
            var book = _dbContext.Books.Find(bookId);
            if(book is not null)
            {
                _dbContext.Books.Remove(book);
                _dbContext.SaveChanges();
            }
        }

        public void DeleteLoan(int loanId)
        {
            var loan = _dbContext.Loans.Find(loanId);
            if(loan is not null)
            {
                _dbContext.Loans.Remove(loan);
                _dbContext.SaveChanges();
            }
        }

        public void DeleteReader(int readerId)
        {
            var reader = _dbContext.Readers.Find(readerId);
            if(reader is not null)
            {
                _dbContext.Readers.Remove(reader);
                _dbContext.SaveChanges();
            }
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


        public void UpdateBook(int bookId, BookDto bookDto)
        {
            var book = _dbContext.Books.Find(bookId);
            if(book is not null)
            {
                book.Title = bookDto.Title;
                book.Author = bookDto.Author;
                book.Publisher = book.Publisher;
                book.PublicationYear = book.PublicationYear;
                
                _dbContext.SaveChanges();
            }
        }

        public void UpdateLoan(int loanId, LoanDto loanDto)
        {
            var loan = _dbContext.Loans.Find(loanId);
            if(loan is not null)
            {
                loan.ReaderId = loanDto.ReaderId;
                loan.BookId = loanDto.BookId;
                loan.LoanDate = loanDto.LoanDate;
                loan.DueDate = loanDto.DueDate;
                loan.ReturnDate = loanDto.ReturnDate;

                _dbContext.SaveChanges();
            }
        }

        public void UpdateReader(int readerId, ReaderDto readerDto)
        {
            var reader = _dbContext.Readers.Find(readerId);
            if(reader is not null)
            {
                reader.Name = readerDto.Name;
                reader.Address = readerDto.Address;
                reader.BirthDate = readerDto.BirthDate;
                
                _dbContext.SaveChanges();
            }
        }
    }
}