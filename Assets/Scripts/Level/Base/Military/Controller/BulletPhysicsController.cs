using AIBrains.SoldierBrain;
using Data.ValueObject;
using Datas.UnityObject;
using Enums;
using Interfaces;
using Managers;
using Signals;
using UnityEngine;
using Controllers.AIControllers;

namespace Controllers
{
    public class BulletPhysicsController : MonoBehaviour,IAttacker
    {
        #region Self Variables

        #region Public Variables

        #endregion

        #region Serialized Variables

        

        #endregion

        #region Private Variables

        private int _damage;

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



    }
}

