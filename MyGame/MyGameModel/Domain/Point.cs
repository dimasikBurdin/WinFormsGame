using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGameModel.Domain
{
    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }
        public static Point Empty = new Point() { X = 0, Y = 0 };
        public Point() { }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static Point operator +(Point firstPoint, Point secondPoint)
        => firstPoint == null || secondPoint == null ? Point.Empty : new Point(firstPoint.X + secondPoint.X, firstPoint.Y + secondPoint.Y);

        public static Point operator -(Point firstPoint, Point secondPoint)
        => firstPoint == null || secondPoint == null ? Point.Empty : new Point(firstPoint.X - secondPoint.X, firstPoint.Y - secondPoint.Y);

        public static bool operator ==(Point firstPoint, Point secondPoint)
        => firstPoint?.X == secondPoint?.X && firstPoint?.Y == secondPoint?.Y;
        
        public static bool operator !=(Point firstPoint, Point secondPoint)
        => firstPoint?.X != secondPoint?.X || firstPoint?.Y != secondPoint?.Y;

        public override bool Equals(object obj)
        {
            return this == (Point)obj;
        }
    }
}
