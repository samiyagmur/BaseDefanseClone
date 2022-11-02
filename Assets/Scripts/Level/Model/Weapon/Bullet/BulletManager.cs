using Controllers;
using Data.UnityObject;
using Data.ValueObject;
using Enums;
using Interfaces;
using Signals;
using UnityEngine;

namespace Managers
{
    public class BulletManager : MonoBehaviour, IReleasePoolObject
    {
        #region Self Variables



        #region Serialized Variables

        [SerializeField]
        private WeaponTypes weaponType;

        [SerializeField]
        private BulletMovementController bulletMovementController;

        [SerializeField]
        private BulletPhysicsController physicsController;

        #endregion Serialized Variables

        #region Private Variables

        private WeaponData _data;

        #endregion Private Variables

        #endregion Self Variables

        private void Awake()
        {
            _data = GetBulletData();
            SetDataToControllers();
        }

        private void OnEnable()
        {
            Invoke(nameof(SetBulletToPool), 1f);
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            PlayerSignal.Instance.onSetWeaponTransform += OnSetWeaponTransform;
        }

        private void UnsubscribeEvents()
        {
            PlayerSignal.Instance.onSetWeaponTransform -= OnSetWeaponTransform;
        }

        private void OnDisable() => UnsubscribeEvents();

        private void OnSetWeaponTransform(Transform playerTransform)
        {
            Debug.Log(playerTransform);
            bulletMovementController.SetPlayerTransform(playerTransform);
        }

        private WeaponData GetBulletData() => Resources.Load<CD_Weapon>("Data/CD_Weapon").WeaponData[(int)weaponType];

        private void SetDataToControllers() => physicsController.GetData(_data);

        public void ReleaseObject(GameObject obj, PoolType poolName) => PoolSignals.Instance.onReleaseObjectFromPool.Invoke(poolName, obj);

        public void SetBulletToPool()
        {
            var poolName = (PoolType)System.Enum.Parse(typeof(PoolType), weaponType.ToString());
            ReleaseObject(gameObject, poolName);
        }
    }
}