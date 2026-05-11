using System.ComponentModel.DataAnnotations;
using Share.Validations;

namespace Share.Dtos
{
    /// <summary>
    /// Data transfer object representing the details of a book.
    /// </summary>
    public class BookDto
    {

        /// <summary>
        /// The unique identifier of the book.
        /// </summary>
        public int BookId {get; set;}

        /// <summary>
        /// The title of the book.
        /// </summary>
        [Required(ErrorMessage ="Book title is required!")]
        public string Title{get; set;} = string.Empty;

        /// <summary>
        /// The author of the book.
        /// </summary>
        [Required(ErrorMessage ="Book author is required!")]
        public string Author{get; set;} = string.Empty;

        /// <summary>
        /// The publisher of the book, if available.
        /// </summary>
        public string? Publisher{get; set;}


        /// <summary>
        /// The year the book was published. Must be non-negative and cannot exceed the current year.
        /// </summary>
        [ValidPublicationYear]
        public int? PublicationYear {get; set;}
    }
}