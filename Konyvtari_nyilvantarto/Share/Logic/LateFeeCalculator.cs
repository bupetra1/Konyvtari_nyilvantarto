namespace Share.Logic
{
    public static class LateFeeCalculator
    {
        public static int CalculateLateFee(DateOnly dueDate, DateOnly? returnDate)
        {
                int daysLate = 0;
                if(returnDate is null && dueDate < DateOnly.FromDateTime(DateTime.Now))
                {
                    daysLate = DateOnly.FromDateTime(DateTime.Now).DayNumber - dueDate.DayNumber;
                }
                if(returnDate is not null && dueDate < returnDate)
                {
                    daysLate = returnDate.Value.DayNumber - dueDate.DayNumber;
                }
                return daysLate switch
                {
                    >=1 and <11 => 100*daysLate,
                    >=11 and <16 => 100*daysLate*2,
                    >=16 => 100*daysLate*3,
                    _ => 0

                };
        }
    }
}