using DG.Tweening;
using Interfaces;
using UnityEngine;
using UnityEngine.AI;

namespace StateMachines.AIBrain.Workers.MoneyStates
{
    public class MoveToGateState : IState
    {
        private readonly NavMeshAgent _navmeshAgent;
        private readonly Animator _animator;
        private readonly Vector3 _gateTarget;
        private static readonly int Speed = Animator.StringToHash("Speed");
        private readonly float _speed;

        public bool IsArrive = false;

        public MoveToGateState(NavMeshAgent navMeshAgent, Animator animator, Vector3 gateTarget, float maxSpeed)
        {
            _navmeshAgent = navMeshAgent;
            _animator = animator;
            _gateTarget = gateTarget;
            _speed = maxSpeed;
        }

        public void OnEnter()
        {
            //isWalking anim
            _navmeshAgent.speed = _speed;
            _navmeshAgent.SetDestination(_gateTarget);
        }

        public void OnExit()
        {
            _navmeshAgent.transform.DORotate(Vector3.zero, 0.5f);
            IsArrive = false;
        }

        public void Tick()
        {
            _animator.SetFloat(Speed, _navmeshAgent.velocity.magnitude);
            if (_navmeshAgent.remainingDistance <= 0.1f)
                IsArrive = true;
        }
    }
}