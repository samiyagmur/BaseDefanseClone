using Enums;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Datas.UnityObject
{
    [Serializable]
    public class EnemyTypeData
    {
        public List<Transform> TurretList = new List<Transform>();

        public Transform SpawnPosition;

        public EnemyType EnemyType;
        public Vector3 scaleSize;
        public Color color;
        public float NavMeshRadius;
        public float NavMeshHeigh;
        public int Healt;
        public int Damage;
        public float AttackRange;
        public float AttackSpeed;
        public float MoveSpeed;
        public float ChaseSpeed;
    }
}