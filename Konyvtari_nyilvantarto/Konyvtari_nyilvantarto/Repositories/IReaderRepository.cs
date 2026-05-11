using Share.Dtos;

namespace Konyvtari_nyilvantarto.Repositories
{
    /// <summary>
    /// Defines the contract for querying reader-specific library data.
    /// </summary>
    public interface IReaderRepository
    {
        /// <summary>
        /// Lists the available books that the reader can borrow.
        /// </summary>
        /// <returns>A collection of <see cref="ReaderBookListDto"/> objects representing books that are available for borrowing.</returns>
        Task<IEnumerable<ReaderBookListDto>> GetAvailableBooksAsync();

        /// <summary>
        /// Lists loans for the specified reader.
        /// </summary>
        /// <param name="readerId">The unique identifier of the reader whose loans are to be queried.</param>
        /// <returns>If the specified reader exists, then a collection of <see cref="ReaderLoanListDto"/> objects representing
        ///  the reader's loan data, else <see langword="null"/>.</returns>
        Task<IEnumerable<ReaderLoanListDto>>? GetLoansByReaderIdAsync(int readerId);
    }
}