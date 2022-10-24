using Interfaces;
using System;
using UnityEngine;
using UnityEngine.AI;

namespace AI.States
{
    public class MoveToAvaliableContayner : IState
    {
        #region Constructor

        private NavMeshAgent _agent;
        private Animator _animator;
        private float _movementSpeed;
        private GameObject _decidedContayner;

        public void SetData(GameObject decidedContayner)
        {
            _decidedContayner = decidedContayner;
        }

        public MoveToAvaliableContayner(NavMeshAgent agent, Animator animator, float movementSpeed)
        {
            _agent = agent;
            _animator = animator;
            _movementSpeed = movementSpeed;
        }

        #endregion Constructor

        #region State

        public void OnEnter()
        {
            _agent.speed = _movementSpeed;

            _agent.SetDestination(_decidedContayner.transform.position);
        }

        public void OnExit()
        {
        }

        public void Tick()
        {
            _animator.SetFloat("Speed", _agent.velocity.magnitude);
        }

        internal void IncreaseSpeed(float speed)
        {
           _movementSpeed=speed;
        }

        #endregion State
    }
}