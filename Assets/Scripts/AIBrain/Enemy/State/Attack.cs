using Abstraction;
using AIBrain;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace State
{
    public class Attack : IState
    {
        private Animator _animator;

        private NavMeshAgent _navMeshAgent;

        private EnemyBrain _enemyBrain;

        private float _movementSpeed;

        private Transform _playerTransform;

        private float _atackRange;




        public bool _inAttack;

        public Attack(Animator animator, NavMeshAgent navMeshAgent, EnemyBrain enemyBrain, Transform playerTransform, float atackRange)
        {

            _animator = animator;
            _navMeshAgent = navMeshAgent;
            _enemyBrain = enemyBrain;
            _playerTransform = playerTransform;
            _atackRange = atackRange;
             
        }

        public  void Tick()
        {   
            if (_enemyBrain.PlayerTarget)
            {
                _navMeshAgent.destination = _enemyBrain.PlayerTarget.position;
            }
            else
            {
                Debug.Log("_inatack");
                _inAttack = false;
            }

            CheckAttackDistance();
        }

        public  void Enter()
        {
            _navMeshAgent.SetDestination(_enemyBrain.PlayerTarget.position);
            _inAttack = true;
            _animator.SetTrigger("Attack");
        }


        private void CheckAttackDistance()
        {
            if (_navMeshAgent.remainingDistance > _atackRange)
            {
                
                _inAttack = false;
            }
        }
        public bool InPlayerAttackRange() => _inAttack;

        public void Exit()
        {
        
        }
    }
}