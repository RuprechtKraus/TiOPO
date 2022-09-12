namespace TriangleLib.Exceptions
{
    public class TriangleException : ApplicationException
    {
        public TriangleException() : base()
        {
        }

        public TriangleException( string message ) : base( message )
        {
        }
    }
}
