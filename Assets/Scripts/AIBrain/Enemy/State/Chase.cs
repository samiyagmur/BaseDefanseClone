using Abstraction;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace AIBrain.Enemy.State
{
    public class Chase : IState
    {
        private readonly EnemyBrain _enemyBrain;
        private readonly NavMeshAgent _navMeshAgent;
        private readonly Animator _animator;
        private readonly float _attackRange;
        private readonly float _chaseSpeed;

        private bool _inAttack = false;

        public bool IsPlayerInRange;

        public Chase(EnemyBrain enemyBrain, NavMeshAgent navMeshAgent, Animator animator, float attackRange, float chaseSpeed)
        {
            _enemyBrain = enemyBrain;
            _navMeshAgent = navMeshAgent;
            _animator = animator;
            _attackRange = attackRange;
            _chaseSpeed = chaseSpeed;
        }

        public void Enter()
        {
            _inAttack = false;
            _navMeshAgent.speed = _chaseSpeed;
            _navMeshAgent.SetDestination(_enemyBrain.Target.transform.position);
           // _animator.SetTrigger("Run");
        }

        public void Exit()
        {
            
        }

        public void Tick()
        {
            _navMeshAgent.destination = _enemyBrain.Target.transform.position;
            ChechDistanceChase();
            //_animator.ResetTrigger("Run");
        }

        private void ChechDistanceChase()
        {
            if (_navMeshAgent.remainingDistance<=_attackRange)
            {
                _inAttack = true;
            }
        }
    }
}