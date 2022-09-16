using Abstraction;
using AIBrain;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Abstraction
{
    public  class Brain: IState
    {
        private Animator _animator { get; set; }

        private NavMeshAgent _navMeshAgent { get; set; }

        private EnemyBrain _enemyBrain { get; set; }

        private Transform _spawnPoint { get; set; }

        private float _movementSpeed { get; set; }

        private Transform _turretTransform { get; set; }

        private Transform _playerTransform { get; set; }

        private float _atackRange { get; set; }

        private float _damage { get; set; }

        private float _healt { get; set; }

        private float _playerDamage { get; set; }

           
        public virtual void Enter() { }

        public virtual void Tick() { }

        public virtual void Exit() { }



    }
}