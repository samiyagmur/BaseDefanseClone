using Abstraction;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace AIBrain.Enemy.State
{
    public class Bomb : IState
    {
        private readonly NavMeshAgent _navMeshAgent;
        private readonly Animator _animator;

        public Bomb(NavMeshAgent navMeshAgent, Animator animator)
        {
            this._navMeshAgent = navMeshAgent;
            this._animator = animator;
        }

        public bool BombIsAlive { get; internal set; }

        public void Enter()
        {
            throw new System.NotImplementedException();
        }

        public void Exit()
        {
            throw new System.NotImplementedException();
        }

        public void Tick()
        {
            throw new System.NotImplementedException();
        }
    }
}