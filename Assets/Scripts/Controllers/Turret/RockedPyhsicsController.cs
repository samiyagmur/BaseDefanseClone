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
        public int _damage { get; set ; }

        public int GetDamage()
        {
            return _damage;
        }
    }
}