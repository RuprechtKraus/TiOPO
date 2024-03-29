using System.ComponentModel;
using TriangleLib.Exceptions;

namespace TriangleLib
{
    public static class Utilities
    {
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgsParseException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static IEnumerable<float> ParseSides( string[] args )
        {
            if ( args == null )
            {
                throw new ArgumentNullException( nameof( args ) );
            }

            if ( args.Length != 3 )
            {
                throw new ArgsParseException( "Не треугольник" );
            }

            IEnumerable<float> sides = args.Select( x =>
            {
                if ( !float.TryParse( x, out float result ) )
                {
                    throw new ArgsParseException( "Неизвестная ошибка" );
                }
                return result;
            } );

            foreach ( float side in sides )
            {
                if ( side < 0 || side > float.MaxValue )
                {
                    throw new ArgumentOutOfRangeException( nameof( args ) );
                }
            }

            return sides;
        }

        /// <exception cref="InvalidEnumArgumentException"></exception>
        public static string ToTriangleTypeString( this TriangleType triangleType )
        {
            return triangleType switch
            {
                TriangleType.Isosceles => "Равнобедренный",
                TriangleType.Equilateral => "Равносторонний",
                TriangleType.Ordinary => "Обычный",
                _ => throw new InvalidEnumArgumentException( "Неизвестный тип треугольника" ),
            };
        }
    }
}
