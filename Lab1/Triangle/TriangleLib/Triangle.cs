using TriangleLib.Exceptions;

namespace TriangleLib
{
    public enum TriangleType
    {
        Isosceles,
        Equilateral,
        Ordinary
    }

    public class Triangle
    {
        public float sideA { get; }
        public float sideB { get; }
        public float sideC { get; }

        public Triangle( float sideA, float sideB, float sideC )
        {
            if ( !IsValidTriangle( sideA, sideB, sideC ) )
            {
                throw new TriangleException( "Не треугольник" );
            }

            this.sideA = sideA;
            this.sideB = sideB;
            this.sideC = sideC;
        }

        public TriangleType GetTriangleType()
        {
            if ( sideA == sideB && sideB == sideC )
            {
                return TriangleType.Equilateral;
            }

            if ( sideA == sideB || sideA == sideC || sideB == sideC )
            {
                return TriangleType.Isosceles;
            }

            return TriangleType.Ordinary;
        }

        private static bool IsValidTriangle( float a, float b, float c )
        {
            if ( a + b <= c || a + c <= b || b + c <= a )
            {
                return false;
            }

            return true;
        }
    }
}
