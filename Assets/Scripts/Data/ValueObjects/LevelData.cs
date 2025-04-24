using System;
using System.Collections.Generic;

namespace Scripts.Data.ValueObjects
{
    [Serializable]
    public struct LevelData
    {
        public List<PoolData> Pools;
    }
}