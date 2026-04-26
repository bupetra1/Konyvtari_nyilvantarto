using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;


namespace Konyvtari_nyilvantarto
{
    public class AppDbContext : DbContext
    {
        public DbSet<Book> Books {get; set;}
        public DbSet<Reader> Readers {get; set;}
        public DbSet<Loan> Loans {get; set;}

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().HasKey(x => x.Id);
            modelBuilder.Entity<Reader>().HasKey(x => x.Id);
            modelBuilder.Entity<Loan>().HasKey(x => new {x.BookId ,x.ReaderId});

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

    public class Book
    {
        public int Id{get; set;}
        [Required]
        public string Title{get; set;} = string.Empty;
        [Required]
        public string Author{get; set;} = string.Empty;
        public string? Publisher{get; set;}
        public int? PublicationYear {get; set;}
        public ICollection<Loan> Loans {get; set;} = new List<Loan>();
    }
    public class Reader
    {
        public int Id {get; set;}
        [Required]
        public string Name {get; set;} = string.Empty;
        [Required]
        public string Address {get; set;} = string.Empty;
        [Required]
        public DateTime BirthDate {get; set;}

        public ICollection<Loan> Loans {get; set;} = new List<Loan>();
    }
    public class Loan
    {
        public int ReaderId {get; set;}
        public int BookId {get; set;}
        [Required]
        public DateTime LoanDate {get; set;}
        [Required]
        public DateTime DueDate {get; set;}
        public int? LateFee {get; set;}
        public DateTime? ReturnDate {get; set;}
        public Reader Reader {get; set;} = new Reader();
        public Book Book {get; set;} = new Book();
    }
}