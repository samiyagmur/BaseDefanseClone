using Enums;
using System;

namespace Data.ValueObject
{
    [Serializable]
    public class EnemyData
    {
        public int Healt;
        public int Damage;
        public float AttackRange;
        public float MoveSpeed;
        public EnemyType EnemyType;
        public float ChaseSpeed;
    }
}