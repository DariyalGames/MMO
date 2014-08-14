using UnityEngine;
using System;
using System.Collections.Generic;
using ModestTree.Zenject;

namespace Dariyal.MMO.Battle.HexGrid
{
    public class RegularHeagonalGenerator : IHexGridGenerator
    {
        Settings _settings;
        IFactory<Hex> _factory;
        float _hexWidth;
        float _hexHeight;
        Vector3 _firstHexPositionInWorldSpace;

        public RegularHeagonalGenerator(Settings settings, IFactory<Hex> factory)
        {
            _settings = settings;
            _factory = factory;

            _hexWidth = _settings.HexPrefab.renderer.bounds.extents.x*2;
            _hexHeight = _settings.HexPrefab.renderer.bounds.extents.z;
            
            _firstHexPositionInWorldSpace = new Vector3((-1 * _hexWidth/2 * (_settings.Sides - 1)), 0, (3 / 2 * _hexHeight * (_settings.Sides - 1)));
            Debug.Log("First Hex Position : " + _firstHexPositionInWorldSpace.ToString());
        }

        public Dictionary<Point, Hex> GenerateGrid()
        {
            Dictionary<Point, Hex> hexgrid = new Dictionary<Point, Hex>();

            int rows = _settings.Sides*2-1;
            Debug.Log("rows:" + rows.ToString());
            for (int i = 0; i < rows; i++)
            {
                int columns = (i < _settings.Sides) ? (_settings.Sides + i) : (3 * _settings.Sides - 2 - i);
                Debug.Log("column("+i.ToString()+"):" + columns.ToString());
                for (int j = 0; j < columns; j++)
                {
                    Hex hex = _factory.Create();
                    hex.GridPosition = new Point(i, j);
                    hex.WorldPosition = GridToWorldCoordinates(hex.GridPosition);
                    hex.Parent = _settings.HexGrid;
                    hexgrid.Add(hex.GridPosition, hex);
                }
            }

            return hexgrid;
        }

        public Vector3 GridToWorldCoordinates(Point gridPos)
        {
            float dx = (gridPos.X < _settings.Sides) ? (-1 * _hexWidth * gridPos.X / 2) : (-1 * _hexWidth * (2 * (_settings.Sides - 1) - gridPos.X) / 2);
            dx += _hexWidth * gridPos.Y;
            Debug.Log("x(" + gridPos.X.ToString() + "," + gridPos.Y.ToString() + "):" + dx.ToString());
            float dz = (-1 * _hexHeight * 3 / 2 * gridPos.X);
            return _firstHexPositionInWorldSpace + new Vector3(dx, 0, dz);
        }

        [Serializable]
        public class Settings
        {
            public GameObject HexPrefab;
            public int Sides;
            public Transform HexGrid;
        }
    }
}
