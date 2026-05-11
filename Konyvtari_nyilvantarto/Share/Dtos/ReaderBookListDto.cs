using System.ComponentModel.DataAnnotations;
using Share.Validations;

namespace Share.Dtos
{
    public class ReaderBookListDto
    {
        public string Title{get; set;} = string.Empty;
        public string Author{get; set;} = string.Empty;
        public string? Publisher{get; set;}
        public int? PublicationYear {get; set;}
    }
}