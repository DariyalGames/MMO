using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Dariyal.PathFind;
using ModestTree;

namespace Dariyal.MMO.Battle.HexGrid
{
    public class Hex : GridObject, IDisposable, IHasNeighbours<Hex>
    {
        //Member variables.
        private HexHooks _hooks;
        private Settings _settings;

        private bool _hasDisposed;
        private bool _isPassable;
        private bool _isSelected;
        private bool _isInPath;

        public bool ISPassable
        {
            get { return _isPassable; }
            set { _isPassable = value; }
        }

        public bool Selected
        {
            get { return _isSelected; }
            set 
            { 
                _isSelected = value; 
            }
        }

        public Hex(HexHooks hooks, Settings settings)
            : base(0, 0)
        {
            ISPassable = true;
            _hooks = hooks;
            _settings = settings;
            _hasDisposed = false;
        }

        public IEnumerable<Hex> AllNeighbours { get; set; }
        public IEnumerable<Hex> Neighbours
        {
            get { return AllNeighbours.Where(o => o.ISPassable); }
        }

        public Vector3 WorldPosition
        {
            get { return _hooks.transform.localPosition; }
            set { _hooks.transform.localPosition = value; }
        }

        public Transform Parent
        {
            get { return _hooks.transform.parent; }
            set { _hooks.transform.parent = value; }
        }

        public static List<Point> NeighbourShift
        {
            get
            {
                return new List<Point>
                {
                    new Point(0, 1),
                    new Point(1, 0),
                    new Point(1, -1),
                    new Point(0, -1),
                    new Point(-1, 0),
                    new Point(-1, 1),
                };
            }
        }

		public bool IsClicked(Camera camera, Vector3 clickLocation)
		{
            bool result = _hooks.IsClicked(camera, clickLocation);
            //if (result)
            //{
            //    if (!Selected)
            //        Selected = true;
            //    else
            //        Selected = false;
            //}

            return result;
		}

        public void FindNeighbours(Dictionary<Point, Hex> Board, Vector2 BoardSize, bool EqualLineLengths)
        {
            List<Hex> neighbours = new List<Hex>();

            foreach (Point point in NeighbourShift)
            {
                int neighbourX = X + point.X;
                int neighbourY = Y + point.Y;
                //x coordinate offset specific to straight axis coordinates
                int xOffset = neighbourY / 2;

                //If every second hexagon row has less hexagons than the first one, just skip the last one when we come to it
                if (neighbourY % 2 != 0 && !EqualLineLengths &&
                    neighbourX + xOffset == BoardSize.x - 1)
                    continue;
                //Check to determine if currently processed coordinate is still inside the board limits
                if (neighbourX >= 0 - xOffset &&
                    neighbourX < (int)BoardSize.x - xOffset &&
                    neighbourY >= 0 && neighbourY < (int)BoardSize.y)
                    neighbours.Add(Board[new Point(neighbourX, neighbourY)]);
            }

            AllNeighbours = neighbours;
        }

        public void Update()
        {
            //TODO: Add update logic.
        }

        public void Dispose()
        {
            Assert.That(!_hasDisposed);
            _hasDisposed = true;
            GameObject.Destroy(_hooks);
            _hooks = null;
        }

        [Serializable]
        public class Settings
        {
            public Color selectedColor;
            public Color movableColor;
            public Color defaultColor;
            public Color impassableColor;
        }
    }
}