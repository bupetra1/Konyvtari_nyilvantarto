namespace Share.Logic
{
    /// <summary>
    /// Provides a method for calculating late fee.
    /// </summary>
    public static class LateFeeCalculator
    {
        /// <summary>
        /// A base fee used for calculations.
        /// </summary>
        private const int baseFee = 100;

        /// <summary>
        /// Calculates the late fee based on the number of overdue days.
        /// The calculation follows a progressive penalty structure.
        /// </summary>
        /// <param name="dueDate">The deadline for the loan.</param>
        /// <param name="returnDate">The date the book was returned, or null if it is still with the reader.</param>
        /// <returns>An integer containing the total fee.</returns>
        public static int CalculateLateFee(DateOnly dueDate, DateOnly? returnDate)
        {
                int daysLate = 0;
                DateOnly dateToday = DateOnly.FromDateTime(DateTime.Now);
                if(returnDate is null && dueDate < dateToday)
                {
                    daysLate = dateToday.DayNumber - dueDate.DayNumber;
                }
                if(returnDate is not null && dueDate < returnDate)
                {
                    daysLate = returnDate.Value.DayNumber - dueDate.DayNumber;
                }
                return daysLate switch
                {
                    >=1 and <11 => baseFee*daysLate,
                    >=11 and <16 => baseFee*daysLate*2,
                    >=16 => baseFee*daysLate*3,
                    _ => 0

                };
        }
    }
}