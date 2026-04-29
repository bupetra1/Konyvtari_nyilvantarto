using Konyvtari_nyilvantarto.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Share
{
    public class ReaderDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required!")]
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "Address is required!")]
        public string Address { get; set; } = string.Empty;

        [Required(ErrorMessage = "BirthDate is required!")]
        [ValidBirthDate]
        public DateTime BirthDate { get; set; }
    }
}
