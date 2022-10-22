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
        #endregion

        #region State
        public  void OnEnter()
        {

            _agent.speed = 0;
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