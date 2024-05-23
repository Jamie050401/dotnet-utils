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

let getCompleteRelativeYearsBetweenTwoDates (d1: DateTime) (d2: DateTime) (relativeDay: int) (relativeMonth: int) =
    let (|BeforeRelativeMonth|OnRelativeMonth|AfterRelativeMonth|) (date: DateTime) =
        match date.Month < relativeMonth with
        | true ->
            BeforeRelativeMonth
        | false ->
            match date.Month > relativeMonth with
            | true  -> AfterRelativeMonth
            | false -> OnRelativeMonth

    let (|BeforeRelativeDay|OnRelativeDay|AfterRelativeDay|) (date: DateTime) =
        match date.Day < relativeDay with
        | true ->
            BeforeRelativeDay
        | false ->
            match date.Day > relativeDay with
            | true  -> AfterRelativeDay
            | false -> OnRelativeDay

    let getNextRelativeYearStart (date: DateTime) =
        match date, date with
        | BeforeRelativeMonth, _
        | OnRelativeMonth, BeforeRelativeDay
        | OnRelativeMonth, OnRelativeDay     -> DateTime (date.Year, relativeMonth, relativeDay)
        | _                                  -> DateTime (date.Year + 1, relativeMonth, relativeDay)

    let getPrevRelativeYearStart (date: DateTime) =
        match date, date with
        | BeforeRelativeMonth, _
        | OnRelativeMonth, BeforeRelativeDay -> DateTime (date.Year - 1, relativeMonth, relativeDay)
        | _                                  -> DateTime (date.Year, relativeMonth, relativeDay)


    let earlierDate, laterDate = determineEarliestDate d1 d2
    let relativeEarlierDate = getNextRelativeYearStart earlierDate
    let relativeLaterDate = getPrevRelativeYearStart laterDate

    match relativeLaterDate <= relativeEarlierDate with
    | true  -> 0
    | false -> getCompleteYearsBetweenTwoDates relativeEarlierDate relativeLaterDate

let getCompleteTaxYearsBetweenTwoDates (d1: DateTime) (d2: DateTime) =
    getCompleteRelativeYearsBetweenTwoDates d1 d2 6 4

let getMonthsBetweenTwoDates (d1: DateTime) (d2: DateTime) =
    getYearsBetweenTwoDates d1 d2 |> (*) 12.0

let getCompleteMonthsBetweenTwoDates (d1: DateTime) (d2: DateTime) =
    getMonthsBetweenTwoDates |> getCompleteXBetweenTwoDates d1 d2

let getWeeksBetweenTwoDates (d1: DateTime) (d2: DateTime) =
    (getDaysBetweenTwoDates d1 d2) / 7.0

let getCompleteWeeksBetweenTwoDates (d1: DateTime) (d2: DateTime) =
    getWeeksBetweenTwoDates |> getCompleteXBetweenTwoDates d1 d2
