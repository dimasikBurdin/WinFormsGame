using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class PointExtensions
{
    public static Point Add(this Point first, Point second)
        => new Point(first.X + second.X, first.Y + second.Y);

    public static Point SubStract(this Point first, Point second)
        => new Point(first.X - second.X, first.Y - second.Y);
}