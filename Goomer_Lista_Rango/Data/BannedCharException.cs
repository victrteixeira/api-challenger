namespace Goomer_Lista_Rango.Data;

public class BannedCharException : Exception
{
    public BannedCharException()
    {
    }
    
    public BannedCharException(string? message) : base(message)
    {
    }
    
    public override string Message { get => "Impossible to input special char to this field"; }
}