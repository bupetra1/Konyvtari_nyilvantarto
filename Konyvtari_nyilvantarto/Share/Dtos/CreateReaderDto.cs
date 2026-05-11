using System.ComponentModel.DataAnnotations;
using Share.Validations;
using Share.Logic;

namespace Share.Dtos
{

    /// <summary>
    /// Data transfer object containing the necessary information to register a new library reader.
    /// </summary>
    public class CreateReaderDto
    {

        /// <summary>
        /// The full name of the new reader.
        /// </summary>
        [Required(ErrorMessage ="Name is required!")]
        public string Name {get; set;} = string.Empty;

        /// <summary>
        /// The address of the new reader.
        /// </summary>
        [Required(ErrorMessage ="Address is required!")]
        public string Address {get; set;} = string.Empty;

        /// <summary>
        /// The date of birth of the new reader. Must be a valid date in the past.
        /// </summary>
        [Required(ErrorMessage ="BirthDate is required!")]
        [ValidBirthDate]
        public DateOnly BirthDate {get; set;}
    }
}