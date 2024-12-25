module Utils.FSharp.DateTime

open System

let private getCompleteXBetweenTwoDates d1 d2 (func: DateTime -> DateTime -> float) =
    func d1 d2 |> truncate |> int

let private determineEarliestDate (d1: DateTime) (d2: DateTime) =
    match d1 <= d2 with
    | true  -> d1, d2
    | false -> d2, d1

let private (|BeforeRelativeMonth|OnRelativeMonth|AfterRelativeMonth|) (date: DateTime, relativeMonth) =
    match date.Month < relativeMonth with
    | true ->
        BeforeRelativeMonth
    | false ->
        match date.Month > relativeMonth with
        | true  -> AfterRelativeMonth
        | false -> OnRelativeMonth

let private (|BeforeRelativeDay|OnRelativeDay|AfterRelativeDay|) (date: DateTime, relativeDay) =
    match date.Day < relativeDay with
    | true ->
        BeforeRelativeDay
    | false ->
        match date.Day > relativeDay with
        | true  -> AfterRelativeDay
        | false -> OnRelativeDay

let private getNextRelativeYearStart date relativeMonth relativeDay =
    match (date, relativeMonth), (date, relativeDay) with
    | BeforeRelativeMonth, _
    | OnRelativeMonth, BeforeRelativeDay
    | OnRelativeMonth, OnRelativeDay     -> DateTime (date.Year, relativeMonth, relativeDay)
    | _                                  -> DateTime (date.Year + 1, relativeMonth, relativeDay)

let private getPrevRelativeYearStart date relativeMonth relativeDay =
    match (date, relativeMonth), (date, relativeDay) with
    | BeforeRelativeMonth, _
    | OnRelativeMonth, BeforeRelativeDay -> DateTime (date.Year - 1, relativeMonth, relativeDay)
    | _                                  -> DateTime (date.Year, relativeMonth, relativeDay)

let getDaysBetweenTwoDates (d1: DateTime) (d2: DateTime) =
    abs (d1 - d2).TotalDays

let getCompleteDaysBetweenTwoDates d1 d2 =
    getDaysBetweenTwoDates |> getCompleteXBetweenTwoDates d1 d2

let getMonthsBetweenTwoDates d1 d2 =
    raise (NotImplementedException())

let getCompleteMonthsBetweenTwoDates d1 d2 =
    getMonthsBetweenTwoDates |> getCompleteXBetweenTwoDates d1 d2

let getYearsBetweenTwoDates d1 d2 =
    getMonthsBetweenTwoDates d1 d2 / 12.0

let getCompleteYearsBetweenTwoDates d1 d2 =
    getYearsBetweenTwoDates |> getCompleteXBetweenTwoDates d1 d2

let getCompleteRelativeYearsBetweenTwoDates d1 d2 relativeDay relativeMonth =
    let earlierDate, laterDate = determineEarliestDate d1 d2
    let relativeEarlierDate = getNextRelativeYearStart earlierDate relativeDay relativeMonth
    let relativeLaterDate = getPrevRelativeYearStart laterDate relativeDay relativeMonth

    match relativeLaterDate <= relativeEarlierDate with
    | true  -> 0
    | false -> getCompleteYearsBetweenTwoDates relativeEarlierDate relativeLaterDate

let getCompleteTaxYearsBetweenTwoDates d1 d2 =
    getCompleteRelativeYearsBetweenTwoDates d1 d2 6 4

let getWeeksBetweenTwoDates d1 d2 =
    (getDaysBetweenTwoDates d1 d2) / 7.0

let getCompleteWeeksBetweenTwoDates d1 d2 =
    getWeeksBetweenTwoDates |> getCompleteXBetweenTwoDates d1 d2
