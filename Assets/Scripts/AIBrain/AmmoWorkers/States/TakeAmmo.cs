using Abstraction;
using AIBrain;
using Controllers;
using Enums;
using Interfaces;
using Managers;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace States
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





        #endregion

        #region State
        public  void Enter()
        {

            Debug.Log("TakeAmmoEnter");
            _agent.speed = 0;



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