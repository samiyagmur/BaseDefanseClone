using Abstraction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;
using System;

namespace States
{
    public class MoveToWareHouse :IState
    {
        #region Constructor

        private NavMeshAgent _agent;
        private Animator _animator;
        private float _movementSpeed;
        private Transform _ammoWareHouse;
        private GameObject _ammoWorker;

        public MoveToWareHouse(NavMeshAgent agent, Animator animator, float movementSpeed, Transform ammoWareHouse, GameObject ammoWorker)
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



            CalculateAmmoWareHouseArea();




        }

        private void CalculateAmmoWareHouseArea()
        {



           



            //return 
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