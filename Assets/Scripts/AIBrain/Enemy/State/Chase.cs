using Abstraction;
using AIBrain;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace State
{
    public class Chase :IState
    {

        private Animator _animator;

        private EnemyBrain _enemyBrain;

        private NavMeshAgent _navMeshAgent;

        private Transform _playerTransform;

        private float _chaseSpeed;
       
        private float _atackRange;

        private bool _attackOnPlayer;

        public bool AttackOnPlayer { get => _attackOnPlayer; set => _attackOnPlayer = value; }

        public Chase(Animator animator, NavMeshAgent navMeshAgent, EnemyBrain enemyBrain, float chaseSpeed, float atackRange)
        {
            _animator = animator;
            _navMeshAgent = navMeshAgent;
            _chaseSpeed = chaseSpeed;
            _atackRange = atackRange;
            _enemyBrain = enemyBrain;
        }

        public  void OnEnter()
        {
            
            AttackOnPlayer = false;


            _playerTransform = _enemyBrain.PlayerTarget;

            _navMeshAgent.speed= _chaseSpeed/2;
            
            if (_playerTransform != null)
            _navMeshAgent.SetDestination(_playerTransform.position);

            _animator.SetTrigger("Run");



            
        }

        public  void Tick()
        {
           _navMeshAgent.destination = _enemyBrain.PlayerTarget.position;

           if (_enemyBrain.PlayerTarget != null) 
                _navMeshAgent.destination = _enemyBrain.PlayerTarget.transform.position;
            
            
            checkDestance();
        }

        private void checkDestance()
        {
            if (_navMeshAgent.remainingDistance <= _atackRange)
            {
                AttackOnPlayer = true;
            }
                       
        }

        public bool GetPlayerInRange() => AttackOnPlayer;

        public void OnExit()
        {
            


            
        }
    }
}