module Utils.FSharp.DateTime

open System

let private determineEarliestDate (d1: DateTime) (d2: DateTime) =
    match d1 <= d2 with
    | true  -> d1, d2
    | false -> d2, d1

let private getCompleteXBetweenTwoDates (d1: DateTime) (d2: DateTime) (func: DateTime -> DateTime -> float) =
    func d1 d2 |> truncate |> int

let getDaysBetweenTwoDates (d1: DateTime) (d2: DateTime) =
    abs (d1 - d2).TotalDays

let getCompleteDaysBetweenTwoDates (d1: DateTime) (d2: DateTime) =
    getDaysBetweenTwoDates |> getCompleteXBetweenTwoDates d1 d2

let getYearsBetweenTwoDates (d1: DateTime) (d2: DateTime) =
    let earlierDate, laterDate = determineEarliestDate d1 d2
    let yearsFromYears = float (laterDate.Year - earlierDate.Year)
    let yearsFromMonths = float (laterDate.Month - earlierDate.Month) / 12.0
    let yearsFromDays = (float (laterDate.Day - earlierDate.Day) / float (DateTime.DaysInMonth (laterDate.Year, laterDate.Month))) / 12.0

    yearsFromYears + yearsFromMonths + yearsFromDays

let getCompleteYearsBetweenTwoDates (d1: DateTime) (d2: DateTime) =
    getYearsBetweenTwoDates |> getCompleteXBetweenTwoDates d1 d2

let getRelativeYearsBetweenTwoDates (d1: DateTime) (d2: DateTime) (relativeDay: int) (relativeMonth: int) =
    let earlierDate, laterDate = determineEarliestDate d1 d2
    let relativeEarlierDate = DateTime (earlierDate.Year, relativeMonth, relativeDay)
    let relativeLaterDate = DateTime (laterDate.Year, relativeMonth, relativeDay)

    let relativeYears = getYearsBetweenTwoDates relativeEarlierDate relativeLaterDate
    let earlierAdj = getYearsBetweenTwoDates earlierDate relativeEarlierDate * match earlierDate < relativeEarlierDate with | true -> 1.0 | false -> -1.0
    let laterAdj = getYearsBetweenTwoDates laterDate relativeLaterDate * match laterDate < relativeLaterDate with | true -> -1.0 | false -> 1.0

    relativeYears + earlierAdj + laterAdj + match laterDate >= relativeLaterDate with | true -> 0.0 | false -> -1.0

let getCompleteRelativeYearsBetweenTwoDates (d1: DateTime) (d2: DateTime) (relativeDay: int) (relativeMonth: int) =
    // TODO: Understand why this first statement does not work
    //(relativeDay |> (relativeMonth |> getCustomYearsBetweenTwoDates)) |> getCompleteXBetweenTwoDates d1 d2
    getRelativeYearsBetweenTwoDates d1 d2 relativeDay relativeMonth |> truncate |> int

let getMonthsBetweenTwoDates (d1: DateTime) (d2: DateTime) =
    getYearsBetweenTwoDates d1 d2 |> (*) 12.0

let getCompleteMonthsBetweenTwoDates (d1: DateTime) (d2: DateTime) =
    getMonthsBetweenTwoDates |> getCompleteXBetweenTwoDates d1 d2

let getWeeksBetweenTwoDates (d1: DateTime) (d2: DateTime) =
    (getDaysBetweenTwoDates d1 d2) / 7.0

// TODO: Needs unit tests
let getCompleteWeeksBetweenTwoDates (d1: DateTime) (d2: DateTime) =
    getWeeksBetweenTwoDates |> getCompleteXBetweenTwoDates d1 d2
