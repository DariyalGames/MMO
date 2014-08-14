using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dariyal.MMO.Battle.HexGrid
{
    public interface IHexGridGenerator
    {
        Dictionary<Point, Hex> GenerateGrid();
    }
}
