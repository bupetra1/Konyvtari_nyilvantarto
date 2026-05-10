using System.ComponentModel.DataAnnotations;
using Konyvtari_nyilvantarto.Validations;

namespace Share.Dtos
{
    /// <summary>
    /// Data transfer object containing the necessary information to register a new book.
    /// </summary>
    public class CreateBookDto
    {
        /// <summary>
        /// The title of the new book.
        /// </summary>
        [Required(ErrorMessage ="Book title is required!")]
        public string Title{get; set;} = string.Empty;

        /// <summary>
        /// The author of the new book.
        /// </summary>
        [Required(ErrorMessage ="Book author is required!")]
        public string Author{get; set;} = string.Empty;

        /// <summary>
        /// The publisher of the new book, if available.
        /// </summary>
        public string? Publisher{get; set;}

        /// <summary>
        /// The year the new book was published, if available. Must be non-negative and cannot exceed the current year.
        /// </summary>
        [ValidPublicationYear]
        public int? PublicationYear {get; set;}
    }
}