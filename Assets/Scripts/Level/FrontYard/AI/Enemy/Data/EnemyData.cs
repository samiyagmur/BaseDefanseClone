using Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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