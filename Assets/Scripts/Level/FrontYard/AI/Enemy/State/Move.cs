using AIBrain;
using AIBrain.EnemyBrain;
using Interfaces;
using UnityEngine;
using UnityEngine.AI;

namespace AI.States
{
    public class Move : IState
    {
        private Animator _animator;

        private NavMeshAgent _navMeshAgent;

        private EnemyBrain _enemyBrain;

        private float _movementSpeed;

        private Transform _turretTransform;

        public Move(Animator animator, NavMeshAgent navMeshAgent, EnemyBrain enemyBrain, float movementSpeed, Transform turretTransform)
        {
            _animator = animator;
            _navMeshAgent = navMeshAgent;
            _enemyBrain = enemyBrain;
            _movementSpeed = movementSpeed;
            _turretTransform = turretTransform;
        }

        public void OnEnter()
        {
            _navMeshAgent.enabled = true;
            _navMeshAgent.speed = _movementSpeed;
            _animator.SetBool("Walk", _navMeshAgent.velocity.magnitude > 0.01f);
            _navMeshAgent.SetDestination(_turretTransform.position);
            //_animator.SetFloat(Speed, 1f);
        }

        public void OnExit()
        {
            //_navMeshAgent.enabled = false;
            //_animator.SetFloat(Speed, 0f);
        }

        public void Tick()
        {
        }
    }
}