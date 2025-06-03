using System;
using System.Collections.Generic;

namespace Game.Main
{
    /// <summary>
    /// 对象池管理器
    /// 引用对象池
    /// 资源对象池
    /// </summary>
    public class PoolManager : Singleton<PoolManager>
    {
        private readonly Dictionary<int, Pools> _poolDict = new Dictionary<int, Pools>();

        public static T Get<T>() where T : class, IPool
        {
            Type type = typeof(T);
            int id = type.GetHashCode();
            if (Instance._poolDict.TryGetValue(id, out Pools pools) == false)
            {
                pools = new Pools(type);
                Instance._poolDict.Add(id, pools);
            }
            return (T)pools.Get();
        }

        public static void Reset(IPool pool)
        {
            int id = pool.GetHashCode();
            if (Instance._poolDict.TryGetValue(id,out Pools pools))
            {
                pools.Reset(pool);
            }
        }

        public static void Dispose(IPool pool)
        {
            
        }
    }
}