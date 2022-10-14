using AIBrains.SoldierBrain;
using Data.ValueObject;
using Datas.UnityObject;
using Enums;
using Interfaces;
using Managers;
using Signals;
using UnityEngine;

namespace Controllers
{
    public class BulletPhysicsController : MonoBehaviour,IDamager
    {
        #region Self Variables

        #region Public Variables

        #endregion

        #region Serialized Variables

        [SerializeField]
        private BulletManager bulletManager;

        #endregion

        #region Private Variables

        private int _damage;

        int IDamager._damage { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        #endregion

        #endregion

        public void GetData(WeaponData data)
        {
            _damage = data.Damage;
        }
        public int Damage()
        {
            return _damage;
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IDamagable idDamagable))
            {
                bulletManager.SetBulletToPool();
            }
        }

        public int GetDamage()
        {
            throw new System.NotImplementedException();
        }
    }
}

