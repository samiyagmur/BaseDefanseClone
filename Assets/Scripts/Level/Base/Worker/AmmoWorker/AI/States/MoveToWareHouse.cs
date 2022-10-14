using Abstraction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;
using System;
using AIBrain;

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
        private AmmoWorkerBrain ammoWorkerBrain;

        public MoveToWareHouse( NavMeshAgent agent, Animator animator, float movementSpeed, Transform ammoWareHouse, GameObject ammoWorker, AmmoWorkerBrain ammoWorkerBrain)
        {
            _agent = agent;
            _animator = animator;
            _movementSpeed = movementSpeed;
            _ammoWareHouse = ammoWareHouse;
            _ammoWorker = ammoWorker;
            this.ammoWorkerBrain = ammoWorkerBrain;
        }



        #endregion

        #region State
        public void OnEnter()
        {

            _agent.speed = _movementSpeed;
            _agent.SetDestination(_ammoWareHouse.position);

            //_animator.SetTrigger("Walk");
        }

        public void OnExit()
        {

        }

        public void Tick()
        {
            
        } 
        #endregion


    }
}