﻿using System.Collections;
using UnityEngine;
using Interfaces;
using AIBrain;
using Signals;
using AIBrain.EnemyBrain;

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