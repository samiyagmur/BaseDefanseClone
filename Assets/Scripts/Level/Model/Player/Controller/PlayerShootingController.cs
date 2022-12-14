using Enums;
using Keys;
using Managers;
using Signals;
using System.Collections;
using UnityEngine;

namespace Controllers
{
    public class PlayerShootingController : MonoBehaviour
    {
        [SerializeField]
        private PlayerManager manager;

        [SerializeField]
        private Transform weaponHolder;

        private BulletFireController _fireController;
        private Vector2 _bulletPosRange;
        private const float _fireRate = 0.3f;

        private void Awake()
        {
            _fireController = new BulletFireController(manager.WeaponType);
        }

        public void SetEnemyTargetTransform()
        {
            manager.EnemyTarget = manager.EnemyList[0].GetTransform();
            manager.HasEnemyTarget = true;
            Shoot();
        }

        private void EnemyTargetStatus()
        {
            if (manager.EnemyList.Count != 0)
            {
                SetEnemyTargetTransform();
            }
            else
            {
                manager.HasEnemyTarget = false;
            }
        }

        private void RemoveTarget()
        {
            if (manager.EnemyList.Count == 0) return;
            manager.EnemyList.RemoveAt(0);
            manager.EnemyList.TrimExcess();
            manager.EnemyTarget = null;
            EnemyTargetStatus();
        }

        private void Shoot()
        {
            if (!manager.EnemyTarget || manager.CurrentAreaType == AreaType.BaseDefense)
                return;
            if (manager.EnemyList[0].IsDead)
            {
                RemoveTarget();
            }
            else
            {
                StartCoroutine(FireBullets());
            }
        }

        private IEnumerator FireBullets()
        {
            yield return new WaitForSeconds(_fireRate);

            _fireController.FireBullets(weaponHolder, new Vector3(_bulletPosRange.x, 0, _bulletPosRange.y));

            PlayerSignal.Instance.onSetWeaponTransform?.Invoke(weaponHolder);

            Shoot();
        }

        internal void ArrangeBulletpos(HorizontalInputParams inputParams)
        {
            _bulletPosRange = inputParams.MovementVector;
        }
    }
}