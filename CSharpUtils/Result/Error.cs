namespace Utils.CSharp.Result;

public class Error
{
    public Error(string code, string message)
    {
        Code = code;
        Message = message;
    }

    public override string ToString()
    {
        return $"Code: {Code} Message: {Message}";
    }

    public string Code { get; }
    public string Message { get; }
}
