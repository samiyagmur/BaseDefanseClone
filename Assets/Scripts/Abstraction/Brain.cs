using Abstraction;
using AIBrain;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.Abstraction
{
    public abstract class Brain: IState
    {
        private Animator _animator;

        private NavMeshAgent _navMeshAgent;

        private EnemyBrain _enemyBrain;

        private Transform _spawnPoint;

        private float _movementSpeed;

        private Transform _turretTransform;

        private Transform _playerTransform;

        private float _atackRange;

        private float _damage;

        private float _healt;

        private float _playerDamage;

        
       
      

        public abstract void Enter();

        public virtual void Tick() { }

        public virtual void Exit() { }



    }
}