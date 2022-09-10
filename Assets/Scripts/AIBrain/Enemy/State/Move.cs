using Abstraction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using AIBrain;

namespace AIBrain.Enemy.State
{
    

    public class Move : IState
    {
        private EnemyBrain _enemyBrain;
        private readonly NavMeshAgent _navMeshAgent;
        private readonly Animator _animator;
        private readonly float _moveSpeed;
        private static readonly int _speed = Animator.StringToHash("Speed");
        //private Vector3 _lastPosition = Vector3.zero;

       // public float TimeStuck;
        public Move(NavMeshAgent navMeshAgent, Animator animator)
        {
            _navMeshAgent = navMeshAgent;
            _animator = animator;
        }

        public void Enter()
        {//
            //TimeStuck = 0;


            _navMeshAgent.enabled = true;
            _navMeshAgent.speed = _moveSpeed;

            int randomTarget = Random.Range(0, _enemyBrain.TurretTaretList.Count);
            _navMeshAgent.SetDestination(_enemyBrain.Target.position);
            _animator.SetFloat(_speed, 1f);

        }
        public void Tick()
        {
            //if (Vector3.Distance(_enemyBrain.Target.position, _lastPosition) <= 0)
            //    TimeStuck += Time.deltaTime;

            //    _lastPosition = _enemyBrain.transform.position;
        }
        public void Exit()
        {
            //_navMeshAgent.enabled = false;
            //_animator.SetFloat(_speed, 0f);

        }
    }
}
