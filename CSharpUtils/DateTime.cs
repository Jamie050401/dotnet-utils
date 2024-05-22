namespace Utils.CSharp;

public static class DateTimeExtensions
{
    public static double GetDaysBetweenTwoDates(DateTime d1, DateTime d2)
    {
        return FSharp.DateTime.getDaysBetweenTwoDates(d1, d2);
    }

    public static double GetDaysToTargetDate(this DateTime startDate, DateTime targetDate)
    {
        return GetDaysBetweenTwoDates(startDate, targetDate);
    }

    public static int GetCompleteDaysBetweenTwoDates(DateTime d1, DateTime d2)
    {
        return FSharp.DateTime.getCompleteDaysBetweenTwoDates(d1, d2);
    }

    public static int GetCompleteDaysToTargetDate(this DateTime startDate, DateTime targetDate)
    {
        return GetCompleteDaysBetweenTwoDates(startDate, targetDate);
    }

    public static double GetYearsBetweenTwoDates(DateTime d1, DateTime d2)
    {
        return FSharp.DateTime.getYearsBetweenTwoDates(d1, d2);
    }

    public static double GetYearsToTargetDate(this DateTime startDate, DateTime targetDate)
    {
        return GetYearsBetweenTwoDates(startDate, targetDate);
    }

    public static int GetCompleteYearsBetweenTwoDates(DateTime d1, DateTime d2)
    {
        return FSharp.DateTime.getCompleteYearsBetweenTwoDates(d1, d2);
    }

    public static int GetCompleteYearsToTargetDate(this DateTime startDate, DateTime targetDate)
    {
        return GetCompleteYearsBetweenTwoDates(startDate, targetDate);
    }

    public static double GetRelativeYearsBetweenTwoDates(DateTime d1, DateTime d2, int relativeDay, int relativeMonth)
    {
        return FSharp.DateTime.getRelativeYearsBetweenTwoDates(relativeDay, relativeMonth, d1, d2);
    }

    public static double GetRelativeYearsToTargetDate(this DateTime startDate, DateTime targetDate, int relativeDay, int relativeMonth)
    {
        return GetRelativeYearsBetweenTwoDates(startDate, targetDate, relativeDay, relativeMonth);
    }

    public static int GetCompleteRelativeYearsBetweenTwoDates(DateTime d1, DateTime d2, int relativeDay, int relativeMonth)
    {
        return FSharp.DateTime.getCompleteRelativeYearsBetweenTwoDates(relativeDay, relativeMonth, d1, d2);
    }

    public static int GetCompleteRelativeYearsToTargetDate(this DateTime startDate, DateTime targetDate, int relativeDay, int relativeMonth)
    {
        return GetCompleteRelativeYearsBetweenTwoDates(startDate, targetDate, relativeDay, relativeMonth);
    }

    public static double GetTaxYearsBetweenTwoDates(DateTime d1, DateTime d2)
    {
        return FSharp.DateTime.getTaxYearsBetweenTwoDates(d1, d2);
    }

    public static double GetTaxYearsToTargetDate(this DateTime startDate, DateTime targetDate)
    {
        return GetTaxYearsBetweenTwoDates(startDate, targetDate);
    }

    public static int GetCompleteTaxYearsBetweenTwoDates(DateTime d1, DateTime d2)
    {
        return FSharp.DateTime.getCompleteTaxYearsBetweenTwoDates(d1, d2);
    }

    public static int GetCompleteTaxYearsToTargetDate(this DateTime startDate, DateTime targetDate)
    {
        return GetCompleteTaxYearsBetweenTwoDates(startDate, targetDate);
    }

    public static double GetMonthsBetweenTwoDates(DateTime d1, DateTime d2)
    {
        return FSharp.DateTime.getMonthsBetweenTwoDates(d1, d2);
    }

    public static double GetMonthsToTargetDate(this DateTime startDate, DateTime targetDate)
    {
        return GetMonthsBetweenTwoDates(startDate, targetDate);
    }

    public static int GetCompleteMonthsBetweenTwoDates(DateTime d1, DateTime d2)
    {
        return FSharp.DateTime.getCompleteMonthsBetweenTwoDates(d1, d2);
    }

    public static int GetCompleteMonthsToTargetDate(this DateTime startDate, DateTime targetDate)
    {
        return GetCompleteMonthsBetweenTwoDates(startDate, targetDate);
    }

    public static double GetWeeksBetweenTwoDates(DateTime d1, DateTime d2)
    {
        return FSharp.DateTime.getWeeksBetweenTwoDates(d1, d2);
    }

    public static double GetWeeksToTargetDate(this DateTime startDate, DateTime targetDate)
    {
        return GetWeeksBetweenTwoDates(startDate, targetDate);
    }

    public static int GetCompleteWeeksBetweenTwoDates(DateTime d1, DateTime d2)
    {
        return FSharp.DateTime.getCompleteWeeksBetweenTwoDates(d1, d2);
    }

    public static int GetCompleteWeeksToTargetDate(this DateTime startDate, DateTime targetDate)
    {
        return GetCompleteWeeksBetweenTwoDates(startDate, targetDate);
    }
}
