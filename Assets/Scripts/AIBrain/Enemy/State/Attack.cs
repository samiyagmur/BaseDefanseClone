using Abstraction;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace AIBrain.Enemy.State
{   
    public class Attack : IState
    {
        private readonly EnemyBrain _enemyBrain;
        private readonly NavMeshAgent _navMeshAgent;
        private readonly Animator _animator;
        private readonly float _atackRange;

        private bool _inAttack;

        public Attack(EnemyBrain enemyBrain, NavMeshAgent navMeshAgent, Animator animator, float atackRange)
        {
            _enemyBrain = enemyBrain;
            _navMeshAgent = navMeshAgent;
            _animator = animator;
            _atackRange = atackRange;
        }

        public void Enter()
        {
            _inAttack=true;
            _navMeshAgent.SetDestination(_enemyBrain.Target.transform.position);
        }

        public void Exit()
        {
           
        }

        public void Tick()
        {
            _navMeshAgent.destination = _enemyBrain.Target.transform.position;
            CheckDistanceAttack();
        }

        private void CheckDistanceAttack()
        {
            if (_navMeshAgent.remainingDistance > _atackRange)
                _inAttack = false;
        }
    }
}