using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;


namespace Konyvtari_nyilvantarto
{
    /// <summary>
    /// The primary database context for the Library Management system, 
    /// managing connections and entity mappings for books, readers, and loans.
    /// </summary>
    public class AppDbContext : DbContext
    {
        /// <summary>
        /// Gets or sets the collection of books in the library.
        /// </summary>
        public DbSet<Book> Books {get; set;}

        /// <summary>
        /// Gets or sets the collection of registered library readers.
        /// </summary>
        public DbSet<Reader> Readers {get; set;}

        /// <summary>
        /// Gets or sets the collection of registered loans.
        /// </summary>
        public DbSet<Loan> Loans {get; set;}

        /// <summary>
        /// Initializes a new instance of the <see cref="AppDbContext"/> class with the specified options.
        /// </summary>
        /// <param name="options">The options to be used by the database context.</param>
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}

        /// <summary>
        /// Configures the database schema and entity relationships using the Fluent API.
        /// </summary>
        /// <param name="modelBuilder">The builder being used to construct the model for this context.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().HasKey(b => b.Id);
            modelBuilder.Entity<Reader>().HasKey(r => r.Id);
            modelBuilder.Entity<Loan>().HasKey(l => l.Id);

            modelBuilder.Entity<Loan>()
                .HasOne(x => x.Book)
                .WithMany(x => x.Loans)
                .HasForeignKey(x => x.BookId);

            modelBuilder.Entity<Loan>()
                .HasOne(x => x.Reader)
                .WithMany(x => x.Loans)
                .HasForeignKey(x => x.ReaderId);

        }
    }

    /// <summary>
    /// Represents a book entity in the library database.
    /// </summary>
    public class Book
    {
        /// <summary>
        /// The unique identifier for the book.
        /// </summary>
        public int Id{get; set;}

        /// <summary>
        /// The title of the book.
        /// </summary>
        [Required]
        public string Title{get; set;} = string.Empty;

        /// <summary>
        /// The author of the book.
        /// </summary>
        [Required]
        public string Author{get; set;} = string.Empty;

        /// <summary>
        /// The publisher of the book.
        /// </summary>
        public string? Publisher{get; set;}

        /// <summary>
        /// The year the book was published.
        /// </summary>
        public int? PublicationYear {get; set;}

        /// <summary>
        /// The collection of loans associated with this book.
        /// </summary>
        public ICollection<Loan> Loans {get; set;} = new List<Loan>();
    }

    /// <summary>
    /// Represents a registered reader in the library database.
    /// </summary>
    public class Reader
    {
        /// <summary>
        /// The unique identifier for the reader.
        /// </summary>
        public int Id {get; set;}

        /// <summary>
        /// The full name of the reader.
        /// </summary>
        [Required]
        public string Name {get; set;} = string.Empty;

        /// <summary>
        /// The address of the reader.
        /// </summary>
        [Required]
        public string Address {get; set;} = string.Empty;

        /// <summary>
        /// The date of birth of the reader.
        /// </summary>
        [Required]
        public DateOnly BirthDate {get; set;}

        /// <summary>
        /// The collection of loans associated with this reader.
        /// </summary>
        public ICollection<Loan> Loans {get; set;} = new List<Loan>();
    }

    /// <summary>
    /// Represents a book loan transaction in the library database.
    /// </summary>
    public class Loan
    {

        /// <summary>
        /// The unique identifier for the loan.
        /// </summary>
        public int Id {get; set;}

        /// <summary>
        /// The foreign key referencing the reader who borrowed the book.
        /// </summary>
        public int ReaderId {get; set;}

        /// <summary>
        /// The foreign key referencing the borrowed book.
        /// </summary>
        public int BookId {get; set;}

        /// <summary>
        /// The date when the book was borrowed.
        /// </summary>
        [Required]
        public DateOnly LoanDate {get; set;}

        /// <summary>
        /// The deadline date by which the book must be returned.
        /// </summary>
        [Required]
        public DateOnly DueDate {get; set;}

        /// <summary>
        /// The dynamically calculated late fee for an overdue book.
        /// </summary>
        [NotMapped]
        public int LateFee {get; set;}

        /// <summary>
        /// The date the book was returned.
        /// </summary>
        public DateOnly? ReturnDate {get; set;}

        /// <summary>
        /// The navigation property representing the reader associated with this loan.
        /// </summary>
        public Reader Reader {get; set;} = new Reader();

        /// <summary>
        /// The navigation property representing the borrowed book.
        /// </summary>
        public Book Book {get; set;} = new Book();
    }
}