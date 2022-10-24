using AIBrains.EnemyBrain;
using Enums;
using Interfaces;
using Signals;
using UnityEngine;

namespace Controllers.AIControllers
{
    public class EnemyPhysicsController : MonoBehaviour, IDamagable
    {
        private TurretKey _turretKey;
        private void OnEnable()
        {
            IsDead = false;
        }

        [SerializeField]
        private EnemyAIBrain _enemyAIBrain;
        public bool IsTaken { get; set; }
        public bool IsDead { get; set; }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out TurretDetactController turretDetactController))
            {
                _turretKey = turretDetactController.TurretKey;
            }


            if (!other.TryGetComponent(out IAttacker attacker)) return;

            var damage = attacker.Damage();
            TakeDamage(damage);
        }

        public int TakeDamage(int damage)
        {
            if (_enemyAIBrain.Health <= 0) return 0;

            _enemyAIBrain.Health -= damage;

            if (_enemyAIBrain.Health != 0) return _enemyAIBrain.Health;

            IsDead = true;

            return _enemyAIBrain.Health;

        }
        public Transform GetTransform()
        {
            return transform;
        }
        private void OnDisable()
        {
            IsDead = true;

        }
    }
}