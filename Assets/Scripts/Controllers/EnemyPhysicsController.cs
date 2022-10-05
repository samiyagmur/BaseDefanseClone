using System.Collections;
using UnityEngine;
using Interfaces;
using AIBrain;

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