module Utils.Test.Math

open NUnit.Framework
open Utils.Math

[<TestFixture>]
type MathTests () =
    [<Test>]
    [<Category("Math")>]
    member this.SafeDivide_Integers_NonZeroDenominator () =
        let numerator = 10
        let denominator = 5

        let result = numerator /*/ denominator

        Assert.That (result, Is.EqualTo (Some 2))

    [<Test>]
    [<Category("Math")>]
    member this.SafeDivide_Floats_NonZeroDenominator () =
        let numerator = 5.0
        let denominator = 10.0

        let result = numerator /*/ denominator

        Assert.That (result, Is.EqualTo (Some 0.5))

    [<Test>]
    [<Category("Math")>]
    member this.SafeDivide_Integers_ZeroDenominator () =
        let numerator = 10
        let denominator = 0

        let result = numerator /*/ denominator

        Assert.That (result, Is.EqualTo None)

    [<Test>]
    [<Category("Math")>]
    member this.SafeDivide_Floats_ZeroDenominator () =
        let numerator = 5.0
        let denominator = 0.0

        let result = numerator /*/ denominator

        Assert.That (result, Is.EqualTo None)

    [<Test>]
    [<Category("Math")>]
    member this.Round_TwoDecimalPlaces_GreaterThan () =
        let number = 43.7991

        let result = round 2 number

        Assert.That (result, Is.EqualTo 43.80)

    [<Test>]
    [<Category("Math")>]
    member this.Round_TwoDecimalPlaces_EqualTo () =
        let number = 43.7951

        let result = round 2 number

        Assert.That (result, Is.EqualTo 43.80)

    [<Test>]
    [<Category("Math")>]
    member this.Round_TwoDecimalPlaces_LessThan () =
        let number = 43.7931

        let result = round 2 number

        Assert.That (result, Is.EqualTo 43.79)

    [<Test>]
    [<Category("Math")>]
    member this.RoundUp_TwoDecimalPlaces_GreaterThan () =
        let number = 43.7991

        let result = roundUp 2 number

        Assert.That (result, Is.EqualTo 43.80)

    [<Test>]
    [<Category("Math")>]
    member this.RoundUp_TwoDecimalPlaces_EqualTo () =
        let number = 43.7951

        let result = roundUp 2 number

        Assert.That (result, Is.EqualTo 43.80)

    [<Test>]
    [<Category("Math")>]
    member this.RoundUp_TwoDecimalPlaces_LessThan () =
        let number = 43.7931

        let result = roundUp 2 number

        Assert.That (result, Is.EqualTo 43.80)

    [<Test>]
    [<Category("Math")>]
    member this.RoundDown_TwoDecimalPlaces_GreaterThan () =
        let number = 43.7991

        let result = roundDown 2 number

        Assert.That (result, Is.EqualTo 43.79)

    [<Test>]
    [<Category("Math")>]
    member this.RoundDown_TwoDecimalPlaces_EqualTo () =
        let number = 43.7951

        let result = roundDown 2 number

        Assert.That (result, Is.EqualTo 43.79)

    [<Test>]
    [<Category("Math")>]
    member this.RoundDown_TwoDecimalPlaces_LessThan () =
        let number = 43.7931

        let result = roundDown 2 number

        Assert.That (result, Is.EqualTo 43.79)
