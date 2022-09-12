namespace TriangleLib.Exceptions
{
    public class ArgsParseException : ApplicationException
    {
        public ArgsParseException() : base()
        {
        }

        public ArgsParseException( string message ) : base( message )
        {
        }
    }
}
