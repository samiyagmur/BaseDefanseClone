using System;
using Enums;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Data.ValueObject
{
    [Serializable]
    public class WeaponData
    {
        public WeaponTypes WeaponTypes;
        public Mesh WeaponMesh;
        public bool HasSideMesh;
        [ShowIf("HasSideMesh")]
        public Mesh SideMesh;
        public int Damage;
        public float AttackRate;
        public int WeaponLevel;
        public GameObject Bullet;
    }
}