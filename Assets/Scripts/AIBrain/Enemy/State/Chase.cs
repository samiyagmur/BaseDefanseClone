using Abstraction;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace AIBrain.Enemy.State
{
    public class Chase : IState
    {
        private readonly NavMeshAgent _navMeshAgent;
        private readonly Animator _animator;
        public bool IsPlayerInRange;

        public Chase(NavMeshAgent navMeshAgent, Animator animator)
        {
            _navMeshAgent = navMeshAgent;
            _animator = animator;
        }

        public void Enter()
        {
            _animator.SetTrigger("Run");
        }

        public void Exit()
        {
            
        }

        public void Tick()
        {
            _animator.ResetTrigger("Run");
        }
    }
}