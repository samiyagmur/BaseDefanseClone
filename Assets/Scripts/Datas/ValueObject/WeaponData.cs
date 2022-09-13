using System;
using System.Collections;
using UnityEngine;

namespace Datas.ValueObject
{
    [Serializable]
    public class WeaponData : MonoBehaviour
    {   

        public int Damage;
        public float AttackRate;

        ParticleSystem WeaponParticle;

    }
}