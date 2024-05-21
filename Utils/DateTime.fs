module Utils.DateTime

open System
open Math

let private determineEarliestDate (d1: DateTime) (d2: DateTime) =
    match d1 <= d2 with
    | true  -> d1, d2
    | false -> d2, d1

let getDaysBetweenTwoDates (d1: DateTime) (d2: DateTime) =
    abs (d1 - d2).TotalDays

let private getCompleteXBetweenTwoDates (d1: DateTime) (d2: DateTime) (func: DateTime -> DateTime -> float) =
    func d1 d2 |> truncate |> int

let getCompleteDaysBetweenTwoDates (d1: DateTime) (d2: DateTime) =
    getDaysBetweenTwoDates |> getCompleteXBetweenTwoDates d1 d2

let private determineYearsBetweenTwoDates (d1: DateTime) (d2: DateTime) =
    let earlierDate, _ = determineEarliestDate d1 d2
    Array.init (d1.Year - d2.Year |> abs) (fun index ->
        earlierDate.Year + index
    )

let private determineLeapYearsBetweenTwoDates (d1: DateTime) (d2: DateTime) =
    let earlierDate, laterDate = determineEarliestDate d1 d2
    determineYearsBetweenTwoDates d1 d2
    |> Array.filter (fun year ->
        let isLastYearAndOnFeb29 = year = laterDate.Year && laterDate.Month = 2 && laterDate.Day = 29
        let isLastYearAndAfterFeb = year = laterDate.Year && laterDate.Month > 2
        let isBetweenEarlierAndLaterYear = year > earlierDate.Year && year < laterDate.Year
        let isFirstYearAndBeforeFeb = year = earlierDate.Year && earlierDate.Month < 2
        let isFirstYearAndBeforeFeb29 = year = earlierDate.Year && earlierDate.Month = 2 && earlierDate.Day < 29
        DateTime.IsLeapYear year &&
        (isFirstYearAndBeforeFeb29 ||
        isFirstYearAndBeforeFeb ||
        isBetweenEarlierAndLaterYear ||
        isLastYearAndAfterFeb ||
        isLastYearAndOnFeb29)
    )

// TODO: Verify this function (not entirely convinced it is truly accurate)
let getYearsBetweenTwoDates (d1: DateTime) (d2: DateTime) =
    let leapYears = determineLeapYearsBetweenTwoDates d1 d2
    let difference = d1.Year - d2.Year |> abs
    let adj = float leapYears.Length /*/ float difference |> Option.setDefault leapYears.Length
    getDaysBetweenTwoDates d1 d2 / (365.0 + adj)

let getCompleteYearsBetweenTwoDates (d1: DateTime) (d2: DateTime) =
    getYearsBetweenTwoDates |> getCompleteXBetweenTwoDates d1 d2

let getMonthsBetweenTwoDates (d1: DateTime) (d2: DateTime) =
    let _, laterDate = determineEarliestDate d1 d2
    let monthsFromCompleteYears = getCompleteYearsBetweenTwoDates d1 d2 |> (*) 12

    let remainingDays = getDaysBetweenTwoDates (DateTime (laterDate.Year, 1, 1)) laterDate
    let remainingMonths =
        Array.init (laterDate.Month - 1) (fun index ->
           1 + index
        )

    let monthsFromIncompleteYear, remainingDays =
        remainingMonths |> ((0, remainingDays) |> Array.fold (fun (months, remainingDays) month ->
            months + 1, remainingDays - float (DateTime.DaysInMonth (laterDate.Year, month))
        ))

    float monthsFromCompleteYears + float monthsFromIncompleteYear + remainingDays / float (DateTime.DaysInMonth (laterDate.Year, laterDate.Month))

let getCompleteMonthsBetweenTwoDates (d1: DateTime) (d2: DateTime) =
    getMonthsBetweenTwoDates |> getCompleteXBetweenTwoDates d1 d2
