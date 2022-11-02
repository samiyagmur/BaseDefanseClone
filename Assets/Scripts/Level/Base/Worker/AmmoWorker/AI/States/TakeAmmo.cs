using Interfaces;
using UnityEngine;
using UnityEngine.AI;

namespace AI.States
{
    public class TakeAmmo : IState
    {
        #region Constructor

        private NavMeshAgent _agent;
        private Animator _animator;

        public TakeAmmo(NavMeshAgent agent, Animator animator)
        {
            _agent = agent;
            _animator = animator;
        }

        #endregion Constructor

        #region State

        public void OnEnter()
        {
            _agent.speed = 0;
        }

        public void OnExit()
        {
        }

        public void Tick()
        {
            _animator.SetFloat("Speed", _agent.velocity.magnitude);
        }

        #endregion State
    }
}