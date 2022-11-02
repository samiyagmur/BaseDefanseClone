using System;
using System.Collections.Generic;

namespace Data.ValueObject
{
    [Serializable]
    public class EnemyAIData
    {
        public List<EnemyData> EnemyDatas = new List<EnemyData>();
        public EnemySpawnData enemySpawnData;
    }
}