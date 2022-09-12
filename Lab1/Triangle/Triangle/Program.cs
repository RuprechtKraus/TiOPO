using TriangleLib;

try
{
    List<float> sides = Utilities.ParseSides( args ).ToList();
    Triangle triangle = new( sides[0], sides[1], sides[2] );
    Console.WriteLine( triangle.GetTriangleType().ToTriangleTypeString() );
}
catch ( ApplicationException e )
{
    Console.WriteLine( e.Message );
}
catch ( Exception )
{
    Console.WriteLine( "Неизвестная ошибка" );
}
