using Interfaces;
using UnityEngine;
using UnityEngine.AI;

namespace AI.States
{
    public class Wait : IState
    {
        private Animator _animator;
        private NavMeshAgent _navMeshAgent;
        private static readonly int Speed = Animator.StringToHash("Speed");

        public Wait(Animator animator, NavMeshAgent navMeshAgent)
        {
            _animator = animator;
            _navMeshAgent = navMeshAgent;
        }

        public void Tick()
        {
        }

        public void OnEnter()
        {
            _navMeshAgent.speed = 1.801268E-05f;
            _animator.SetFloat(Speed, _navMeshAgent.velocity.magnitude);
        }

        public void OnExit()
        {
        }
    }
}