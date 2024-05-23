module Utils.FSharp.Tests.DateTime

open System
open NUnit.Framework
open Utils.FSharp.DateTime

[<TestFixture>]
type DateTimeTests () =
    [<Test>]
    [<Category("DateTime")>]
    member this.GetCompleteDaysBetweenTwoDates_Start1stJan2000_End1stJan2001 () =
        let d1 = DateTime (2000, 1, 1)
        let d2 = DateTime (2001, 1, 1)

        let result = getCompleteDaysBetweenTwoDates d1 d2

        Assert.That (result, Is.EqualTo 366)

    [<Test>]
    [<Category("DateTime")>]
    member this.GetCompleteDaysBetweenTwoDates_Start1stJan2000_End1stFeb2000 () =
        let d1 = DateTime (2000, 2, 1)
        let d2 = DateTime (2000, 1, 1)

        let result = getCompleteDaysBetweenTwoDates d1 d2

        Assert.That (result, Is.EqualTo 31)

    [<Test>]
    [<Category("DateTime")>]
    member this.GetCompleteDaysBetweenTwoDates_Start1stJan2000_End2ndJan2000 () =
        let d1 = DateTime (2000, 1, 1)
        let d2 = DateTime (2000, 1, 2)

        let result = getCompleteDaysBetweenTwoDates d1 d2

        Assert.That (result, Is.EqualTo 1)


    [<Test>]
    [<Category("DateTime")>]
    member this.GetCompleteDaysBetweenTwoDates_Start1stJan2000_End1stJan2000 () =
        let d1 = DateTime (2000, 1, 1)
        let d2 = DateTime (2000, 1, 1)

        let result = getCompleteDaysBetweenTwoDates d1 d2

        Assert.That (result, Is.EqualTo 0)

    [<Test>]
    [<Category("DateTime")>]
    member this.GetCompleteDaysBetweenTwoDates_Start1stJan2000_End1stJan3000 () =
        let d1 = DateTime (2000, 1, 1)
        let d2 = DateTime (3000, 1, 1)

        let result = getCompleteDaysBetweenTwoDates d1 d2

        Assert.That (result, Is.EqualTo 365243)

    [<Test>]
    [<Category("DateTime")>]
    member this.GetCompleteYearsBetweenTwoDates_Start1stJan2000_End1stFeb2000 () =
        let d1 = DateTime (2000, 2, 1)
        let d2 = DateTime (2000, 1, 1)

        let result = getCompleteYearsBetweenTwoDates d1 d2

        Assert.That (result, Is.EqualTo 0)

    [<Test>]
    [<Category("DateTime")>]
    member this.GetCompleteYearsBetweenTwoDates_Start1stJan2000_End1stJan2001 () =
        let d1 = DateTime (2000, 1, 1)
        let d2 = DateTime (2001, 1, 1)

        let result = getCompleteYearsBetweenTwoDates d1 d2

        Assert.That (result, Is.EqualTo 1)

    [<Test>]
    [<Category("DateTime")>]
    member this.GetCompleteYearsBetweenTwoDates_Start31stMay2000_End31stMay2015 () =
        let d1 = DateTime (2000, 5, 31)
        let d2 = DateTime (2015, 5, 31)

        let result = getCompleteYearsBetweenTwoDates d1 d2

        Assert.That (result, Is.EqualTo 15)

    [<Test>]
    [<Category("DateTime")>]
    member this.GetCompleteYearsBetweenTwoDates_Start7thJan2000_End6thJan2003 () =
        let d1 = DateTime (2000, 1, 7)
        let d2 = DateTime (2003, 1, 6)

        let result = getCompleteYearsBetweenTwoDates d1 d2

        Assert.That (result, Is.EqualTo 2)

    [<Test>]
    [<Category("DateTime")>]
    member this.GetCompleteYearsBetweenTwoDates_Start7thJan2000_End8thJan2003 () =
        let d1 = DateTime (2000, 1, 7)
        let d2 = DateTime (2003, 1, 8)

        let result = getCompleteYearsBetweenTwoDates d1 d2

        Assert.That (result, Is.EqualTo 3)

    [<Test>]
    [<Category("DateTime")>]
    member this.GetCompleteYearsBetweenTwoDates_Start1stJan2000_End1stJan3000 () =
        let d1 = DateTime (2000, 1, 1)
        let d2 = DateTime (3000, 1, 1)

        let result = getCompleteYearsBetweenTwoDates d1 d2

        Assert.That (result, Is.EqualTo 1000)

    [<Test>]
    [<Category("DateTime")>]
    member this.GetCompleteMonthsBetweenTwoDates_Start1stJan2000_End1stJan2001 () =
        let d1 = DateTime (2000, 1, 1)
        let d2 = DateTime (2001, 1, 1)

        let result = getCompleteMonthsBetweenTwoDates d1 d2

        Assert.That (result, Is.EqualTo 12)

    [<Test>]
    [<Category("DateTime")>]
    member this.GetCompleteMonthsBetweenTwoDates_Start1stJan2000_End1stFeb2000 () =
        let d1 = DateTime (2000, 1, 1)
        let d2 = DateTime (2000, 2, 1)

        let result = getCompleteMonthsBetweenTwoDates d1 d2

        Assert.That (result, Is.EqualTo 1)

    [<Test>]
    [<Category("DateTime")>]
    member this.GetCompleteMonthsBetweenTwoDates_Start1stJan2000_End31stJan2000 () =
        let d1 = DateTime (2000, 1, 1)
        let d2 = DateTime (2000, 1, 31)

        let result = getCompleteMonthsBetweenTwoDates d1 d2

        Assert.That (result, Is.EqualTo 0)

    [<Test>]
    [<Category("DateTime")>]
    member this.GetCompleteMonthsBetweenTwoDates_Start1stJan2000_End1stJan3000 () =
        let d1 = DateTime (2000, 1, 1)
        let d2 = DateTime (3000, 1, 1)

        let result = getCompleteMonthsBetweenTwoDates d1 d2

        Assert.That (result, Is.EqualTo 12000)

    [<Test>]
    [<Category("DateTime")>]
    member this.GetCompleteMonthsBetweenTwoDates_Start1stJan2000_End28thFeb2015 () =
        let d1 = DateTime (2000, 1, 1)
        let d2 = DateTime (2015, 2, 28)

        let result = getCompleteMonthsBetweenTwoDates d1 d2

        Assert.That (result, Is.EqualTo 181)

    [<Test>]
    [<Category("DateTime")>]
    member this.GetCompleteMonthsBetweenTwoDates_Start1stJan2000_End1stMar2015 () =
        let d1 = DateTime (2000, 1, 1)
        let d2 = DateTime (2015, 3, 1)

        let result = getCompleteMonthsBetweenTwoDates d1 d2

        Assert.That (result, Is.EqualTo 182)

    [<Test>]
    [<Category("DateTime")>]
    member this.GetCompleteMonthsBetweenTwoDates_Start1stJan2000_End2ndMar2015 () =
        let d1 = DateTime (2000, 1, 1)
        let d2 = DateTime (2015, 3, 2)

        let result = getCompleteMonthsBetweenTwoDates d1 d2

        Assert.That (result, Is.EqualTo 182)

    [<Test>]
    [<Category("DateTime")>]
    member this.GetCompleteMonthsBetweenTwoDates_Start1stJan2000_End1stApr2015 () =
        let d1 = DateTime (2000, 1, 1)
        let d2 = DateTime (2015, 4, 1)

        let result = getCompleteMonthsBetweenTwoDates d1 d2

        Assert.That (result, Is.EqualTo 183)

    [<Test>]
    [<Category("DateTime")>]
    member this.GetCompleteRelativeYearsBetweenTwoDates_TaxYears_Start1stJan2000_End1stJan2001 () =
        let d1 = DateTime (2000, 1, 1)
        let d2 = DateTime (2001, 1, 1)

        let result = getCompleteRelativeYearsBetweenTwoDates d1 d2 6 4

        Assert.That (result, Is.EqualTo 0)

    [<Test>]
    [<Category("DateTime")>]
    member this.GetCompleteRelativeYearsBetweenTwoDates_TaxYears_Start1stJan2000_End1stFeb2001 () =
        let d1 = DateTime (2000, 1, 1)
        let d2 = DateTime (2001, 2, 1)

        let result = getCompleteRelativeYearsBetweenTwoDates d1 d2 6 4

        Assert.That (result, Is.EqualTo 0)

    [<Test>]
    [<Category("DateTime")>]
    member this.GetCompleteRelativeYearsBetweenTwoDates_TaxYears_Start1stJan2000_End6thApr2001 () =
        let d1 = DateTime (2000, 1, 1)
        let d2 = DateTime (2001, 4, 6)

        let result = getCompleteRelativeYearsBetweenTwoDates d1 d2 6 4

        Assert.That (result, Is.EqualTo 1)

    [<Test>]
    [<Category("DateTime")>]
    member this.GetCompleteRelativeYearsBetweenTwoDates_TaxYears_Start6thApr2000_End6thApr2001 () =
        let d1 = DateTime (2000, 4, 6)
        let d2 = DateTime (2001, 4, 6)

        let result = getCompleteRelativeYearsBetweenTwoDates d1 d2 6 4

        Assert.That (result, Is.EqualTo 1)

    [<Test>]
    [<Category("DateTime")>]
    member this.GetCompleteRelativeYearsBetweenTwoDates_TaxYears_Start6thApr2000_End1stJan2001 () =
        let d1 = DateTime (2000, 4, 6)
        let d2 = DateTime (2001, 1, 1)

        let result = getCompleteRelativeYearsBetweenTwoDates d1 d2 6 4

        Assert.That (result, Is.EqualTo 0)

    [<Test>]
    [<Category("DateTime")>]
    member this.GetCompleteRelativeYearsBetweenTwoDates_TaxYears_Start5thApr2000_End7thApr2001 () =
        let d1 = DateTime (2000, 4, 5)
        let d2 = DateTime (2001, 4, 7)

        let result = getCompleteRelativeYearsBetweenTwoDates d1 d2 6 4

        Assert.That (result, Is.EqualTo 1)

    [<Test>]
    [<Category("DateTime")>]
    member this.GetCompleteTaxYearsBetweenTwoDates_TaxYears_Start1stJan2000_End31stDec2015 () =
        let d1 = DateTime (2000, 1, 1)
        let d2 = DateTime (2015, 12, 31)

        let result = getCompleteTaxYearsBetweenTwoDates d1 d2

        Assert.That (result, Is.EqualTo 15)

    [<Test>]
    [<Category("DateTime")>]
    member this.GetCompleteTaxYearsBetweenTwoDates_TaxYears_Start7thApr2000_End5thApr2001 () =
        let d1 = DateTime (2000, 4, 7)
        let d2 = DateTime (2001, 4, 5)

        let result = getCompleteTaxYearsBetweenTwoDates d1 d2

        Assert.That (result, Is.EqualTo 0)

    [<Test>]
    [<Category("DateTime")>]
    member this.GetCompleteWeeksBetweenTwoDates_Start1stJan2000_End1stFeb2000 () =
        let d1 = DateTime (2000, 1, 1)
        let d2 = DateTime (2000, 2, 1)

        let result = getCompleteWeeksBetweenTwoDates d1 d2

        Assert.That (result, Is.EqualTo 4)

    [<Test>]
    [<Category("DateTime")>]
    member this.GetCompleteWeeksBetweenTwoDates_Start1stJan2000_End1stJan2010 () =
        let d1 = DateTime (2000, 1, 1)
        let d2 = DateTime (2010, 1, 1)

        let result = getCompleteWeeksBetweenTwoDates d1 d2

        Assert.That (result, Is.EqualTo 521)
