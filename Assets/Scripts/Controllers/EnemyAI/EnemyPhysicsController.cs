using System.Collections;
using UnityEngine;
using Interfaces;
using AIBrain;
using Signals;

namespace Controllers
{
    public class EnemyPhysicsController : MonoBehaviour,IDamagable
    {
        [SerializeField]
        private EnemyBrain enemyAIBrain;

        public bool IsTaken { get; set; }
        public bool IsDead { get; set; }

        public Transform GetTransform()
        {
            
            return this.transform;

        }

        public int TakeDamage(int damage)
        {

            return DeadCondition(damage);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(typeof(IDamager), out Component rocketObject))
            {
                int rockedDamage=(int)rocketObject.GetComponent<RockedPyhsicsController>().GetDamage();

                DeadCondition(rockedDamage);

                if (enemyAIBrain._health<0)
                {   
                    TurretSignals.Instance.onDieEnemy?.Invoke();
                }

            }
        }

        private int DeadCondition(int damage)
        {
            if (enemyAIBrain._health > 0)
            {
                enemyAIBrain._health -= damage;

                if (enemyAIBrain._health <= 0)
                {
                    return enemyAIBrain._health;
                }
                return enemyAIBrain._health;
            }

            return 0;
        }

    }
}