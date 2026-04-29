using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Konyvtari_nyilvantarto.Validations;

namespace Share
{
    public class BookDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Book title is required!")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Book author is required!")]
        public string Author { get; set; } = string.Empty;
        public string? Publisher { get; set; }

        [ValidPublicationYear]
        public int? PublicationYear { get; set; }
    }
}
