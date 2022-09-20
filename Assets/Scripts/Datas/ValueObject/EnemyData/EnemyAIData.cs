using Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data.ValueObject
{
    [Serializable]
    public class EnemyAIData
    {
        public List<Transform> TurretTargetList;

        public Transform SpawnPosition;

        public int Healt;
        public int Damage;
        public float AttackRange;
        public float MoveSpeed;
        public EnemyType EnemyType;
        public float NavMeshRadius;
        public float _enemyDamage;
        public Color Color;
        public float ChaseSpeed;
        public Vector3 ScaleSize;

    }
}
