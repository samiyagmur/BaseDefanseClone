using AIBrains.EnemyBrain;
using Controllers.SoldierPhysicsControllers;
using Interfaces;
using UnityEngine;

namespace Controllers.AIControllers
{
    public class EnemyDetectionController : MonoBehaviour
    {
        #region Self Variables



        #region Serialized Variables

        [SerializeField]
        private EnemyAIBrain enemyAIBrain;

        #endregion Serialized Variables

        #region Private Variables

        private Transform _detectedMine;

        #endregion Private Variables

        #endregion Self Variables

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerPhysicsController physicsController))
            {
                PickOneTarget(other);
                enemyAIBrain.CachePlayer(physicsController);
                enemyAIBrain.CacheSoldier(null);
            }

            if (other.TryGetComponent(out SoldierHealthController soldierHealthController))
            {
                enemyAIBrain.CachePlayer(null);
                PickOneTarget(other);
                enemyAIBrain.CacheSoldier(soldierHealthController);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out PlayerPhysicsController physicsController))
            {
                enemyAIBrain.SetTarget(null);
                enemyAIBrain.CachePlayer(null);
            }

            if (!other.TryGetComponent(out IDamagable damageable)) return;
            enemyAIBrain.SetTarget(null);
            enemyAIBrain.CacheSoldier(null);
        }

        private void PickOneTarget(Collider other)
        {
            if (enemyAIBrain.CurrentTarget == enemyAIBrain.TurretTarget)
            {
                enemyAIBrain.SetTarget(other.transform);
            }
        }
    }
}