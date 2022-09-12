namespace TriangleTestLib.Exceptions
{
    public class TestArgsParseException : ApplicationException
    {
        public TestArgsParseException() : base()
        {
        }

        public TestArgsParseException( string message ) : base( message )
        {
        }
    }
}
