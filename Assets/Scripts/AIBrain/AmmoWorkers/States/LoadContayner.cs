﻿using Abstraction;
using Controllers;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace States
{
    public class LoadContayner : IState
    {   
        private NavMeshAgent agent;
        private Animator animator;
        private float movementSpeed;
        private Transform ammoWareHouse;
      
        public LoadContayner(NavMeshAgent agent, Animator animator, float movementSpeed, Transform ammoWareHouse)
        {
            this.agent = agent;
            this.animator = animator;
            this.movementSpeed = movementSpeed;
            this.ammoWareHouse = ammoWareHouse;
        }

        public void Enter()
        {
            
            Debug.Log("LoadContayner");
            agent.speed = 0;
        }

        public void Exit()
        {
            
        }

        public void Tick()
        {
            
        }

        public void SendAmmoStack()
        {

        }
    }
}