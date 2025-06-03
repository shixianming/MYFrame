using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Main
{
    public abstract class Pool : IPool
    {
        public virtual void Reset()
        {

        }

        public virtual void Dispose()
        {
        }
    }
}