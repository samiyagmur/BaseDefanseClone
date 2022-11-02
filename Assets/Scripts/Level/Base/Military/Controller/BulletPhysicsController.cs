using Data.ValueObject;
using Interfaces;
using UnityEngine;

namespace Controllers
{
    public class BulletPhysicsController : MonoBehaviour, IAttacker
    {
        private int _damage;

        public void GetData(WeaponData data)
        {
            _damage = data.Damage;
        }

        public int Damage()
        {
            return _damage;
        }
    }
}