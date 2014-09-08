using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dariyal.MMO.Core.Services
{
    public interface IDataService
    {
        object GetData(string name);
        void SetData(string name, object data);
    }
}
