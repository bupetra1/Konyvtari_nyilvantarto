using Microsoft.EntityFrameworkCore;
using Konyvtari_nyilvantarto.Dtos;

namespace Konyvtari_nyilvantarto.Repositories
{
    /// <summary>
    /// Provides the implementation for managing reader data in the database.
    /// </summary>
    public class LibrarianRepository : ILibrarianRepository
    {
        /// <summary>
        /// The database context used for data access operations.
        /// </summary>
        private readonly AppDbContext _dbContext;

        /// <summary>
        /// Constructor for the <see cref="LibrarianRepository"/> class.
        /// </summary>
        /// <param name="appDbContext">The database context used to perform data operations.</param>
        public LibrarianRepository(AppDbContext appDbContext)
        {
            _dbContext = appDbContext;
        }

        /// <inheritdoc/>
        public async Task CreateBookAsync(CreateBookDto createBookDto)
        {
            var book = new Book
            {
                Title = createBookDto.Title,
                Author = createBookDto.Author,
                Publisher = createBookDto.Publisher,
                PublicationYear = createBookDto.PublicationYear
            };
            await _dbContext.Books.AddAsync(book);
            await _dbContext.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task CreateLoanAsync(CreateLoanDto createLoanDto)
        {
            var readerTask = _dbContext.Readers.FindAsync(createLoanDto.ReaderId).AsTask();
            var bookTask = _dbContext.Books.FindAsync(createLoanDto.BookId).AsTask();

            await Task.WhenAll(readerTask,bookTask);

            var reader = readerTask.Result;
            var book = bookTask .Result;
            if(reader is not null && book is not null)
            {
                if (await IsBookAvailableAsync(book.Id))
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

        }

        /// <inheritdoc/>
        public async Task CreateReaderAsync(CreateReaderDto createReaderDto)
        {
            var reader = new Reader
            {
                Name = createReaderDto.Name,
                Address = createReaderDto.Address,
                BirthDate = createReaderDto.BirthDate
            };
            await _dbContext.Readers.AddAsync(reader);
            await _dbContext.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task DeleteBookAsync(int bookId)
        {
            var book = await _dbContext.Books.FindAsync(bookId);
            if(book is not null)
            {
                _dbContext.Books.Remove(book);
                await _dbContext.SaveChangesAsync();
            }
        }

        /// <inheritdoc/>
        public async Task DeleteLoanAsync(int loanId)
        {
            var loan = await _dbContext.Loans.FindAsync(loanId);
            if(loan is not null)
            {
                _dbContext.Loans.Remove(loan);
                await _dbContext.SaveChangesAsync();
            }
        }

        /// <inheritdoc/>
        public async Task DeleteReaderAsync(int readerId)
        {
            var reader = await _dbContext.Readers.FindAsync(readerId);
            if(reader is not null)
            {
                _dbContext.Readers.Remove(reader);
                await _dbContext.SaveChangesAsync();
            }
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<BookDto>> GetBooksAsync()
        {
            return await _dbContext.Books
                        .Select(b => new BookDto
                        {
                            BookId = b.Id,
                            Title = b.Title,
                            Author = b.Author,
                            Publisher = b.Publisher,
                            PublicationYear = b.PublicationYear
                        })
                        .ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<LoanDto>> GetLoansAsync()
        {
            return await _dbContext.Loans
                        .Include(l => l.Reader)
                        .Include(l => l.Book)
                        .Select(l => new LoanDto
                        {
                            LoanId = l.Id,
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

        /// <inheritdoc/>
        public async Task<IEnumerable<ReaderDto>> GetReadersAsync()
        {
            return await _dbContext.Readers
                        .Select(r => new ReaderDto
                        {
                            ReaderId = r.Id,
                            Name = r.Name,
                            Address = r.Address,
                            BirthDate = r.BirthDate
                        })
                        .ToListAsync();
        }

        /// <inheritdoc/>
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

        /// <inheritdoc/>
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

        /// <inheritdoc/>
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

        /// <inheritdoc/>
        public async Task<bool> IsBookAvailableAsync(int bookId)
        {
            var book = await _dbContext.Loans.Where(l => l.BookId == bookId && l.ReturnDate == null).FirstOrDefaultAsync();
            if(book is not null)
            {
                return false;
            }
            return true;
        }
    }
}