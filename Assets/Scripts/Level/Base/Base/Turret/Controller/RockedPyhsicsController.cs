using Enums;
using Interfaces;
using Managers;
using System.Collections;
using UnityEngine;

namespace Controllers
{
    public class RockedPyhsicsController : MonoBehaviour, IDamager
    {
        [SerializeField]
        public int Damage { get; set ; }

        public int GetDamage()
        {
            return Damage;
        }
    }
}