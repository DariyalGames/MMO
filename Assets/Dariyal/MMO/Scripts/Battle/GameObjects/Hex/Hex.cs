using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ModestTree;
using UnityEngine;

namespace Dariyal.MMO.Battle.HexGrid
{
    public interface IHex : IDisposable
    {
        void Update();

        Vector3 Position
        {
            get;
            set;
        }

        Transform Parent
        {
            get;
            set;
        }
    }

	public class Hex : MonoBehaviour, IHex
	{
        
        bool _hasDisposed;

        public Hex()
        {
            
        }

        public void Dispose()
        {
            Assert.That(!_hasDisposed);
            _hasDisposed = true;
            GameObject.Destroy(gameObject);
        }

        public void Update()
        {
        }

        public Vector3 Position
        {
            get
            {
                return transform.position;
            }
            set
            {
                transform.position = value;
            }
        }

        public Transform Parent
        {
            get
            {
                return transform.parent;
            }
            set
            {
                transform.parent = value;
            }
        }
    }
}
