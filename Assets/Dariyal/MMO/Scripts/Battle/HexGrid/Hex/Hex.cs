using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Dariyal.Util.PathFind;
using ModestTree;

namespace Dariyal.MMO.Battle.HexGrid
{
    public class Hex : GridObject, IDisposable, IHasNeighbours<Hex>
    {
        public bool Passable;

        private HexHooks _hooks;
        private bool _hasDisposed;

        public Hex(HexHooks hooks)
            : base(0, 0)
        {
            Passable = true;
            _hooks = hooks;
            _hasDisposed = false;
        }

        public IEnumerable<Hex> AllNeighbours { get; set; }
        public IEnumerable<Hex> Neighbours
        {
            get { return AllNeighbours.Where(o => o.Passable); }
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
    }
}