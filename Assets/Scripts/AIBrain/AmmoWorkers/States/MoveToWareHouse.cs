using Abstraction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace States
{
    public class MoveToWareHouse : IState
    {
        #region Constructor

        private NavMeshAgent _agent;
        private Animator _animator;
        private float _movementSpeed;
        private Transform _ammoWareHouse;

        public MoveToWareHouse(NavMeshAgent agent, Animator animator, float movementSpeed, Transform ammoWareHouse)
        {
            _agent = agent;
            _animator = animator;
            _movementSpeed = movementSpeed;
            _ammoWareHouse = ammoWareHouse;
        }

        #endregion

        #region State
        public void Enter()
        {
            Debug.Log("MoveToWareHouse");
            _agent.speed = _movementSpeed;
            _agent.SetDestination(_ammoWareHouse.position);
            //_animator.SetTrigger("Walk");
        }

        public void Exit()
        {

        }

        public void Tick()
        {

        } 
        #endregion


    }
}