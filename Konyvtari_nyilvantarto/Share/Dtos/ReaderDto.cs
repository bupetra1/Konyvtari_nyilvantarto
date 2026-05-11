using System.ComponentModel.DataAnnotations;
using Share.Validations;

namespace Share.Dtos
{
    /// <summary>
    /// Data transfer object representing the detailed information of a library reader.
    /// </summary>
    public class ReaderDto
    {
        /// <summary>
        /// The unique identifier for the reader.
        /// </summary>
        public int ReaderId {get; set;}

        /// <summary>
        /// The full name of the reader.
        /// </summary>
        [Required(ErrorMessage ="Name is required!")]
        public string Name {get; set;} = string.Empty;

        /// <summary>
        /// The address of the reader.
        /// </summary>
        [Required(ErrorMessage ="Address is required!")]
        public string Address {get; set;} = string.Empty;

        /// <summary>
        /// The date of birth of the reader. Must be a valid date in the past.
        /// </summary>
        [Required(ErrorMessage ="BirthDate is required!")]
        [ValidBirthDate]
        public DateOnly BirthDate {get; set;}
    }
}