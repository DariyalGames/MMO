using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dariyal.PathFind
{
    public interface IHasNeighbours<N>
    {
        IEnumerable<N> AllNeighbours { get; set; }
        IEnumerable<N> Neighbours { get; }
    }
}