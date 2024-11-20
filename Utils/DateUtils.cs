public class DateUtils()
{
    public static dynamic GetDaylightSavingTimeDates(string timezone)
    {
        TimeZoneInfo timeZone = TimeZoneInfo.FindSystemTimeZoneById(timezone);

        int currentYear = DateTime.Now.Year;

        DateTime startDate = DateTime.MinValue;
        DateTime endDate = DateTime.MinValue;

        bool isDst = false;
        for (
            DateTime date = new DateTime(currentYear, 1, 1);
            date.Year == currentYear;
            date = date.AddDays(1)
        )
        {
            bool isCurrentDst = timeZone.IsDaylightSavingTime(date);

            if (!isDst && isCurrentDst)
            {
                startDate = date;
                isDst = true;
            }
            else if (isDst && !isCurrentDst)
            {
                endDate = date.AddDays(-1);
                isDst = false;
            }
        }

        return new { startDate, endDate };
    }
}
