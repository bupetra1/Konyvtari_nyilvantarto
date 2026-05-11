namespace Share.Dtos
{
    /// <summary>
    /// Data transfer object for displaying a simplified list of books associated with a reader.
    /// </summary>
    public class ReaderBookListDto
    {
        /// <summary>
        /// The title of the book.
        /// </summary>  
        public string Title{get; set;} = string.Empty;

        /// <summary>
        /// The author of the book.
        /// </summary>
        public string Author{get; set;} = string.Empty;

        /// <summary>
        /// The publisher of the book.
        /// </summary>
        public string? Publisher{get; set;}
        public int? PublicationYear {get; set;}
    }
}