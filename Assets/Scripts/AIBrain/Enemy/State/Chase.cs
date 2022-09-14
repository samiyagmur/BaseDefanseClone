using Abstraction;
using Assets.Scripts.Abstraction;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace AIBrain.Enemy.State
{
    public class Chase :IState
    {

        private Animator _animator;

        private EnemyBrain _enemyBrain;

        private NavMeshAgent _navMeshAgent;

        private Transform _playerTransform;

        public bool _attackOnPlayer;

        private float _movementSpeed;

        private float _atackRange;

        public Chase(Animator animator, NavMeshAgent navMeshAgent, EnemyBrain enemyBrain, float movementSpeed, float atackRange)
        {
            _animator = animator;
            _navMeshAgent = navMeshAgent;
            _movementSpeed = movementSpeed;
            _atackRange = atackRange;
            _enemyBrain = enemyBrain;
        }

        public  void Enter()
        {
            _attackOnPlayer = false;


            _playerTransform = _enemyBrain.PlayerTarget;

            _navMeshAgent.speed= _movementSpeed/2;
            
            if (_playerTransform != null)
            _navMeshAgent.SetDestination(_playerTransform.position);

            _animator.SetTrigger("Run");

            Debug.Log("chase");

            
        }

        public  void Tick()
        {
           _navMeshAgent.destination = _playerTransform.position;

           if (_enemyBrain.PlayerTarget != null) { }
                _navMeshAgent.destination = _enemyBrain.PlayerTarget.transform.position;

            
            checkDestance();
        }

        private void checkDestance()
        {
            if (_navMeshAgent.remainingDistance <= _atackRange)
                _attackOnPlayer = true;
        }

        public bool GetPlayerInRange() => _attackOnPlayer;

        public void Exit()
        {
           
            Debug.Log("exit");
            _enemyBrain.MoveSpeed = 10;
        }
    }
}