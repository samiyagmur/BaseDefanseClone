using Controllers.AIControllers;
using Enums;
using Interfaces;
using Managers;
using Signals;
using System;
using System.Collections;
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