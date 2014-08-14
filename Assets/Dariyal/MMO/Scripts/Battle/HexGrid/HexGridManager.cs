using UnityEngine;
using System.Collections.Generic;
using ModestTree.Zenject;


namespace Dariyal.MMO.Battle.HexGrid
{
    public class HexGridManager : IInitializable, ITickable
    {
        IHexGridGenerator _generator;
        Dictionary<Point, Hex> _hexGrid;

        public HexGridManager(IHexGridGenerator generator)
        {
            _generator = generator;
        }

        public void Initialize()
        {
            _hexGrid = _generator.GenerateGrid();
        }

        public void Tick()
        {
            //Update all hexes.
            foreach (Hex hex in _hexGrid.Values)
            {
                hex.Update();
            }
        }

        static double distance(Hex tile1, Hex tile2)
        {
            return 1;
        }
        static double estimate(Hex tile, Hex destTile)
        {
            float dx = Mathf.Abs(destTile.X - tile.X);
            float dy = Mathf.Abs(destTile.Y - tile.Y);
            int z1 = -(tile.X + tile.Y);
            int z2 = -(destTile.X + destTile.Y);
            float dz = Mathf.Abs(z2 - z1);
            return Mathf.Max(dx, dy, dz);
        }
    }
}
