using Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data.ValueObject
{
    public class EnemyData : MonoBehaviour
    {

        public List<Transform> TurretTargetList;

        public Transform SpawnPosition;

        public int Healt;
        public int Damage;
        public float AttackRange;
        public float MoveSpeed;
        public EnemyType EnemyType;
        public float NavMeshRadius;
        public float EnemyDamage;
        public Color Color;
        public float ChaseSpeed;
        public Vector3 ScaleSize;

    }
}