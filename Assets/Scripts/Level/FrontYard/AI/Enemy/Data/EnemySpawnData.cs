using System;
using System.Collections;
using UnityEngine;

namespace Data.ValueObject
{
    [Serializable]
    public class EnemySpawnData
    {
        public int NumberOfEnemiesToSpawn;
        public float SpawnDelay;
        public int RandomMaxRange;
        public int MinPossibilityToSpawnEnemy;

    }
}