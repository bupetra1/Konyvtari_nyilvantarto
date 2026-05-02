using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Konyvtari_nyilvantarto.Dtos;

namespace Konyvtari_nyilvantarto.Repositories
{
    public class LibrarianRepository : ILibrarianRepository
    {
        private readonly AppDbContext _dbContext;

        public LibrarianRepository(AppDbContext appDbContext)
        {
            _dbContext = appDbContext;
        }
        public async Task CreateBookAsync(BookDto bookDto)
        {
            var book = new Book
            {
                Title = bookDto.Title,
                Author = bookDto.Author,
                Publisher = bookDto.Publisher,
                PublicationYear = bookDto.PublicationYear
            };
            await _dbContext.Books.AddAsync(book);
            await _dbContext.SaveChangesAsync();
        }

        public async Task CreateLoanAsync(CreateLoanDto createLoanDto)
        {
            var readerTask = _dbContext.Readers.FindAsync(createLoanDto.ReaderId).AsTask();
            var bookTask = _dbContext.Books.FindAsync(createLoanDto.BookId).AsTask();

            await Task.WhenAll(readerTask,bookTask);

            var reader = readerTask.Result;
            var book = bookTask .Result;
            if(reader is not null && book is not null)
            {
                var loan = new Loan
                {
                    Reader = reader,
                    Book = book,
                    LoanDate = createLoanDto.LoanDate,
                    DueDate = createLoanDto.DueDate,
                };
                await _dbContext.Loans.AddAsync(loan);
                await _dbContext.SaveChangesAsync();
            }

        }

        public async Task CreateReaderAsync(ReaderDto readerDto)
        {
            var reader = new Reader
            {
                Name = readerDto.Name,
                Address = readerDto.Address,
                BirthDate = readerDto.BirthDate
            };
            await _dbContext.Readers.AddAsync(reader);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteBookAsync(int bookId)
        {
            var book = await _dbContext.Books.FindAsync(bookId);
            if(book is not null)
            {
                _dbContext.Books.Remove(book);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task DeleteLoanAsync(int loanId)
        {
            var loan = await _dbContext.Loans.FindAsync(loanId);
            if(loan is not null)
            {
                _dbContext.Loans.Remove(loan);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task DeleteReaderAsync(int readerId)
        {
            var reader = await _dbContext.Readers.FindAsync(readerId);
            if(reader is not null)
            {
                _dbContext.Readers.Remove(reader);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Book>> GetBooksAsync()
        {
            return await _dbContext.Books
                        .ToListAsync();
        }

        public async Task<IEnumerable<LoanDto>> GetLoansAsync()
        {
            return await _dbContext.Loans
                        .Include(l => l.Reader)
                        .Include(l => l.Book)
                        .Select(l => new LoanDto
                        {
                            ReaderId = l.ReaderId,
                            ReaderName = l.Reader.Name,
                            BookId = l.BookId,
                            BookTitle = l.Book.Title,
                            BookAuthor = l.Book.Author,
                            LoanDate = l.LoanDate,
                            DueDate = l.DueDate,
                            ReturnDate = l.ReturnDate
                        })
                        .ToListAsync();                        
        }

        public async Task<IEnumerable<ReaderDto>> GetReadersAsync()
        {
            return await _dbContext.Readers
                        .Select(r => new ReaderDto
                        {
                            Name = r.Name,
                            Address = r.Address,
                            BirthDate = r.BirthDate
                        })
                        .ToListAsync();
        }


        public async Task UpdateBookAsync(int bookId, BookDto bookDto)
        {
            var book = await _dbContext.Books.FindAsync(bookId);
            if(book is not null)
            {
                book.Title = bookDto.Title;
                book.Author = bookDto.Author;
                book.Publisher = bookDto.Publisher;
                book.PublicationYear = bookDto.PublicationYear;
                
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task UpdateLoanAsync(int loanId, LoanDto loanDto)
        {
            var loan = await _dbContext.Loans.FindAsync(loanId);
            if(loan is not null)
            {
                loan.ReaderId = loanDto.ReaderId;
                loan.BookId = loanDto.BookId;
                loan.LoanDate = loanDto.LoanDate;
                loan.DueDate = loanDto.DueDate;
                loan.ReturnDate = loanDto.ReturnDate;

                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task UpdateReaderAsync(int readerId, ReaderDto readerDto)
        {
            var reader = await _dbContext.Readers.FindAsync(readerId);
            if(reader is not null)
            {
                reader.Name = readerDto.Name;
                reader.Address = readerDto.Address;
                reader.BirthDate = readerDto.BirthDate;
                
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<bool> IsBookAvailableAsync(int bookId)
        {
            var book = _dbContext.Loans.Where(l => l.BookId == bookId && l.ReturnDate == null);
            if(book is not null)
            {
                return false;
            }
            return true;
        }
    }
}