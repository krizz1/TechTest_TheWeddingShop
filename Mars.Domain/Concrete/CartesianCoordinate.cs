using System;

namespace Mars.Domain
{
    public class CartesianCoordinate: IComparable<CartesianCoordinate>
    {
        public CartesianCoordinate() { }

        public CartesianCoordinate(int x, int y)
        {
            this.X = x; this.Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }

        public int CompareTo(CartesianCoordinate that)
        {
            if (this.X != that.X || this.Y != that.Y) return -1;
            if (this.X == that.X && this.Y == that.Y) return 0;
            return 1;
        }
    }
}
