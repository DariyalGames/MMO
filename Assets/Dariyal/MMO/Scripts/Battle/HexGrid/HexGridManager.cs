using UnityEngine;
using System.Collections.Generic;
using ModestTree.Zenject;
using Dariyal.Util.Messenger;
using Dariyal.Util.PathFind;

namespace Dariyal.MMO.Battle.HexGrid
{
    public class HexGridManager : IInitializable, ITickable
    {
        IHexGridGenerator _generator;
        BattleController _battleController;

        Dictionary<Point, Hex> _hexGrid;

        Hex _selectedHex;
        Hex _destinationHex;
        Hex _originHex;

        Path<Hex> _currentPath;

        public HexGridManager(IHexGridGenerator generator, BattleController controller)
        {
            _generator = generator;
            _battleController = controller;
        }

        public void Initialize()
        {
            //generate the hexgrid.
            _hexGrid = _generator.GenerateGrid();

            //add event listeners.
            Messenger.AddListener<Vector3>("input_click", OnClick);
        }

        public void Tick()
        {
            //Update all hexes.
            //foreach (Hex hex in _hexGrid.Values)
            //{
            //    hex.Update();
            //}
        }

        //Event listeners
        void OnClick(Vector3 clickLocation)
        {
            foreach (Hex hex in _hexGrid.Values)
            {
                if (hex.IsClicked(_battleController.ActiveCamera, clickLocation))
                {
                    _selectedHex = hex;
                    _destinationHex = hex;
                    if (_originHex == null)
                        _originHex = hex;
                    else if (hex == _selectedHex)
                    {
                        _selectedHex = null;
                        hex.Selected = false;
                    }
                    if (_destinationHex != _originHex)
                        _currentPath = PathFinder.FindPath<Hex>(_originHex, _destinationHex, distance, estimate);

                    break;
                }
            }
        }

        //Pathfinding related
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
