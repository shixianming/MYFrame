using System;
using System.Collections.Generic;

namespace Game.Main
{
    public sealed class Pools
    {
        private List<IPool> _poolList = new List<IPool>();

        private Type _poolType;

        public Pools(Type type)
        {
            _poolType = type;
        }

        public IPool Get()
        {
            if (_poolList.Count > 0)
            {
                IPool pool = _poolList[_poolList.Count - 1];
                _poolList.RemoveAt(_poolList.Count - 1);
                return pool;
            }
            return Activator.CreateInstance(_poolType) as IPool;
        }

        public void Reset(IPool pool)
        {
            pool.Reset();
            _poolList.Add(pool);
        }

        public void Dispose(IPool pool)
        {
            pool.Dispose();
        }
    }
}