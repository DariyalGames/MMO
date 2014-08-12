using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ModestTree.Zenject;
using UnityEngine;
using ModestTree;

namespace Dariyal.MMO.Battle.HexGrid
{
	public class HexGridManager : ITickable
	{
        Settings _settings;
        bool _started;
        IFactory<IHex> _hexFactory;
        List<Hex> _hexes = new List<Hex>();

        //Hexagon tile width and height in game world
        private float hexWidth;
        private float hexHeight;

        public HexGridManager(Settings settings, IFactory<IHex> hexFactory)
        {
            _settings = settings;
            _hexFactory = hexFactory;
        }

        public void Start()
        {
            Assert.That(!_started);

            setSizes();
            CreateGrid();

            _started = true;
        }

        public void Stop()
        {
            Assert.That(_started);
            _started = false;
        }

        //Method to initialise Hexagon width and height
        void setSizes()
        {
            //renderer component attached to the Hex prefab is used to get the current width and height
            hexWidth = _settings.hexPrefab.renderer.bounds.size.x;
            hexHeight = _settings.hexPrefab.renderer.bounds.size.z;
        }

        //Method to calculate the position of the first hexagon tile
        //The center of the hex grid is (0,0,0)
        Vector3 calcInitPos()
        {
            Vector3 initPos;
            //the initial position will be in the left upper corner
            initPos = new Vector3(-hexWidth * _settings.gridWidthInHexes / 2f + hexWidth / 2, 0,
                _settings.gridHeightInHexes / 2f * hexHeight - hexHeight / 2);

            return initPos;
        }

        //method used to convert hex grid coordinates to game world coordinates
        public Vector3 calcWorldCoord(Vector2 gridPos)
        {
            //Position of the first hex tile
            Vector3 initPos = calcInitPos();
            //Every second row is offset by half of the tile width
            float offset = 0;
            if (gridPos.y % 2 != 0)
                offset = hexWidth / 2;

            float x = initPos.x + offset + gridPos.x * hexWidth;
            //Every new line is offset in z direction by 3/4 of the hexagon height
            float z = initPos.z - gridPos.y * hexHeight * 0.75f;
            return new Vector3(x, 0, z);
        }

        private void CreateGrid()
        {
            for (float y = 0; y < _settings.gridHeightInHexes; y++)
            {
                for (float x = 0; x < _settings.gridWidthInHexes; x++)
                {
                    var hex = _hexFactory.Create();
                    Vector2 gridPos = new Vector2(x, y);
                    hex.Position = calcWorldCoord(gridPos);
                    hex.Parent = _settings.parentTransform;
                }
            }
        }

        void ResetAll()
        {
            foreach (Hex hex in _hexes)
            {
                hex.Dispose();
            }

            _hexes.Clear();
        }

        public void Tick()
        {
            if (!_started)
                return;

            foreach (Hex hex in _hexes)
            {
                hex.Update();
            }
        }

        [Serializable]
        public class Settings
        {
            public GameObject hexPrefab;
            public Transform parentTransform;
            public int gridWidthInHexes;
            public int gridHeightInHexes;
        }
    }
}
