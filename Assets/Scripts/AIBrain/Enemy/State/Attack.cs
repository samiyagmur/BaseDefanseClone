using Abstraction;
using Assets.Scripts.Abstraction;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace AIBrain.Enemy.State
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
            Debug.Log(_playerTransform.gameObject.name);
            Debug.Log(_navMeshAgent.remainingDistance);


            if (_playerTransform)
                _navMeshAgent.destination = _playerTransform.position;

            CheckAttackDistance();
        }

        public  void Enter()
        {   
            _playerTransform = _enemyBrain.PlayerTarget;
            _inAttack = true;
            Debug.Log("attackenter");


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