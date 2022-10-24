using AIBrain;
using AIBrain.AmmoWorkers;
using Interfaces;
using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

namespace AI.States
{
    public class MoveToWareHouse : IState
    {
        #region Constructor

        private NavMeshAgent _agent;
        private Animator _animator;
        private float _movementSpeed;
        private Transform _ammoWareHouse;
        private GameObject _ammoWorker;
        private AmmoWorkerBrain ammoWorkerBrain;

        public MoveToWareHouse(NavMeshAgent agent, Animator animator, float movementSpeed, Transform ammoWareHouse, GameObject ammoWorker, AmmoWorkerBrain ammoWorkerBrain)
        {
            _agent = agent;
            _animator = animator;
            _movementSpeed = movementSpeed;
            _ammoWareHouse = ammoWareHouse;
            _ammoWorker = ammoWorker;
            this.ammoWorkerBrain = ammoWorkerBrain;
        }

        #endregion Constructor

        #region State

        public  void OnEnter()
        {
            _agent.speed = _movementSpeed;

            _agent.SetDestination(_ammoWareHouse.position);
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