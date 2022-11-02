using Controllers.AIControllers;
using Enums;
using Interfaces;
using Signals;
using System.Threading.Tasks;
using UnityEngine;

namespace Controllers
{
    public class RockedPyhsicsController : MonoBehaviour, IAttacker
    {
        [SerializeField]
        private int damage;

        public int Damage()
        {
            return damage;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out EnemyPhysicsController enemyPhysicsController))
            {
                PoolSignals.Instance.onReleaseObjectFromPool(PoolType.TurretRocket, transform.parent.gameObject);
            }

            if (other.TryGetComponent(out IDamagable damagable))
            {
                if (damagable.IsDead)
                {
                    Debug.Log("EnemyDeadForTurret");
                }
            }
        }

        private void OnEnable()
        {
            ReturnTOPool();
        }

        private async void ReturnTOPool()
        {
            while (gameObject.activeInHierarchy)
            {
                await Task.Delay(1500);
                PoolSignals.Instance.onReleaseObjectFromPool(PoolType.TurretRocket, transform.parent.gameObject);
            }
        }
    }
}