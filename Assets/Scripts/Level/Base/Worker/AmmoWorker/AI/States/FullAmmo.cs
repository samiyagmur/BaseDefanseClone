using Interfaces;
using UnityEngine;
using UnityEngine.AI;

namespace AI.States
{
    public class FullAmmo : IState
    {
        #region Constructor

        private NavMeshAgent _agent;
        private Animator _animator;
        private float _movementSpeed;
        private Transform _ammoWareHouse;

        public FullAmmo(NavMeshAgent agent, Animator animator, float movementSpeed, Transform ammoWareHouse)
        {
            _agent = agent;
            _animator = animator;
            _movementSpeed = movementSpeed;
            _ammoWareHouse = ammoWareHouse;
        }

        #endregion Constructor

        #region States

        public void OnEnter()
        {
            _agent.speed = _movementSpeed;

            _agent.SetDestination(_ammoWareHouse.position);
        }

        public void OnExit()
        {
            _agent.speed = 0;
        }

        public void Tick()
        {
            _animator.SetFloat("Speed", _agent.velocity.magnitude);
        }

        internal void IncreaseSpeed(float speed)
        {
            _movementSpeed = speed;
        }

        #endregion States
    }
}