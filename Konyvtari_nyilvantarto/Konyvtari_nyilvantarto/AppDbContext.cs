using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        public DateOnly BirthDate {get; set;}

        public ICollection<Loan> Loans {get; set;} = new List<Loan>();
    }
    public class Loan
    {
        public int Id {get; set;}
        public int ReaderId {get; set;}
        public int BookId {get; set;}
        [Required]
        public DateOnly LoanDate {get; set;}
        [Required]
        public DateOnly DueDate {get; set;}
        [NotMapped]
        public int LateFee
        {
            get
            {
                int daysLate = 0;
                if(ReturnDate is null && DueDate < DateOnly.FromDateTime(DateTime.Now))
                {
                    daysLate = DateOnly.FromDateTime(DateTime.Now).DayNumber - DueDate.DayNumber;
                }
                if(ReturnDate is not null && DueDate < ReturnDate)
                {
                    daysLate = ReturnDate.Value.DayNumber - DueDate.DayNumber;
                }
                return daysLate switch
                {
                    >=1 and <11 => 100*daysLate,
                    >=11 and <16 => 100*daysLate*2,
                    >=16 => 100*daysLate*3,
                    _ => 0

                };
                
            }
        }
        public DateOnly? ReturnDate {get; set;}
        public Reader Reader {get; set;} = new Reader();
        public Book Book {get; set;} = new Book();
    }
}