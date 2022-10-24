using Interfaces;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

namespace AI.States
{
    public class LoadContayner : IState
    {
        private NavMeshAgent agent;
        private Animator animator;
        private Transform ammoWareHouse;

        public LoadContayner(NavMeshAgent agent, Animator animator, Transform ammoWareHouse)
        {
            this.agent = agent;
            this.animator = animator;
            this.ammoWareHouse = ammoWareHouse;
        }

        public void OnEnter()
        {
            
            agent.speed = 0;
        }

        public void OnExit()
        {
        }

        public void Tick()
        {
            animator.SetFloat("Speed", agent.velocity.magnitude);
        }

        public void SendAmmoStack()
        {
        }
    }
}