using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dariyal.MMO.Battle.HexGrid
{
    public abstract class GridObject
    {
        public Point GridPosition;
        public int X { get { return GridPosition.X; } }
        public int Y { get { return GridPosition.Y; } }

        public GridObject(Point gridPosition)
        {
            GridPosition = gridPosition;
        }

        public GridObject(int x, int y) : this(new Point(x, y)) { }

        public override string ToString()
        {
            return string.Format("({0}, {1})", X, Y);
        }
    }
}
