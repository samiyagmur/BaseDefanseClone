using System;

using AIBrains.SoldierBrain;
using DG.Tweening.Core;
using Interfaces;
using UnityEngine;

namespace Controllers.SoldierPhysicsControllers
{
    public class SoldierHealthController : MonoBehaviour,IDamagable
    {
        [SerializeField]
        private SoldierAIBrain soldierAIBrain;

        private void OnEnable()
        {
            IsDead = false;
        }

        public bool IsTaken { get; set; }
        public bool IsDead { get; set; }
        public int TakeDamage(int damage)
        {
            soldierAIBrain.Health -= damage;
            if (soldierAIBrain.Health <= 0)
            {
                IsDead = true;
            }

            return soldierAIBrain.Health;
        }

        public Transform GetTransform()
        {
            return transform;
        }
    }
}