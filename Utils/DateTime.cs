namespace Utils.CSharp;

public static class DateTimeExtensions
{
    public static double GetDaysBetweenTwoDates(this DateTime d1, DateTime d2)
    {
        return FSharp.DateTime.getDaysBetweenTwoDates(d1, d2);
    }

    public static int GetCompleteDaysBetweenTwoDates(this DateTime d1, DateTime d2)
    {
        return FSharp.DateTime.getCompleteDaysBetweenTwoDates(d1, d2);
    }

    public static double GetYearsBetweenTwoDates(this DateTime d1, DateTime d2)
    {
        return FSharp.DateTime.getYearsBetweenTwoDates(d1, d2);
    }

    public static int GetCompleteYearsBetweenTwoDates(this DateTime d1, DateTime d2)
    {
        return FSharp.DateTime.getCompleteYearsBetweenTwoDates(d1, d2);
    }

    public static int GetCompleteRelativeYearsBetweenTwoDates(this DateTime d1, DateTime d2, int relativeDay, int relativeMonth)
    {
        return FSharp.DateTime.getCompleteRelativeYearsBetweenTwoDates(d1, d2, relativeDay, relativeMonth);
    }

    public static int GetCompleteTaxYearsBetweenTwoDates(this DateTime d1, DateTime d2)
    {
        return FSharp.DateTime.getCompleteTaxYearsBetweenTwoDates(d1, d2);
    }

    public static double GetMonthsBetweenTwoDates(this DateTime d1, DateTime d2)
    {
        return FSharp.DateTime.getMonthsBetweenTwoDates(d1, d2);
    }

    public static int GetCompleteMonthsBetweenTwoDates(this DateTime d1, DateTime d2)
    {
        return FSharp.DateTime.getCompleteMonthsBetweenTwoDates(d1, d2);
    }

    public static double GetWeeksBetweenTwoDates(this DateTime d1, DateTime d2)
    {
        return FSharp.DateTime.getWeeksBetweenTwoDates(d1, d2);
    }

    public static int GetCompleteWeeksBetweenTwoDates(this DateTime d1, DateTime d2)
    {
        return FSharp.DateTime.getCompleteWeeksBetweenTwoDates(d1, d2);
    }
}
