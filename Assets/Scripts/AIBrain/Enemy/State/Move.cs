using Abstraction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using AIBrain;
using Assets.Scripts.Abstraction;

namespace AIBrain.Enemy.State
{


    public class Move :IState
    {
        private Animator _animator;

        private NavMeshAgent _navMeshAgent;

        private EnemyBrain _enemyBrain;

        private float _movementSpeed;

        private Transform _turretTransform;

        private static readonly int Speed = Animator.StringToHash("Speed");

        private Vector3 _lastPosition = Vector3.zero;

        public Move(Animator animator, NavMeshAgent navMeshAgent, EnemyBrain enemyBrain, float movementSpeed, Transform turretTransform)
        {
            _animator = animator;
            _navMeshAgent = navMeshAgent;
            _enemyBrain = enemyBrain;
            _movementSpeed = movementSpeed;
            _turretTransform = turretTransform;
        }

        public  void Enter()
        {
            
            _navMeshAgent.enabled = true;
            _navMeshAgent.speed = _movementSpeed;
            _animator.SetBool("Walk", _navMeshAgent.velocity.magnitude > 0.01f);
            _navMeshAgent.SetDestination(_turretTransform.position);
            //_animator.SetFloat(Speed, 1f);
        }

        public  void Exit()
        {
             //_navMeshAgent.enabled = false;
             //_animator.SetFloat(Speed, 0f);
        }

        public void Tick()
        {
           
        }
    }
}
