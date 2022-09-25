using Abstraction;
using Controllers;
using Enums;
using Interfaces;
using UnityEngine;
using UnityEngine.AI;

namespace States
{
    public class TakeAmmo : IState
    {

        #region Constructor

        private IStackable stackable;
        private NavMeshAgent _agent;
        private Animator _animator; 

       

        public TakeAmmo( NavMeshAgent agent, Animator animator)
        {
            _agent = agent;
            _animator = animator;
        }

        #endregion

        #region State
        public void Enter()
        {
            stackable = new AmmoWorkerStackController();

            Debug.Log("TakeAmmo");

            _agent.speed = 0;

            stackable.StartStack(StackStatus.Start);
            //_animator.SetTrigger("Idle");
        }

        public void Exit()
        {

            stackable.StartStack(StackStatus.Stop);


        }
        public void Tick()
        {

            //Arrange Load Time;You can get physics to here.


        } 
        #endregion

        //internal override void SendGridInfoToStack(Vector3 orderOfLasGrid, GameObject stackObject,Transform ammoWareHouse)
        //{
        //    stackable.AddStack(orderOfLasGrid, stackObject, ammoWareHouse);

        //    base.SendGridInfoToStack(orderOfLasGrid, stackObject,ammoWareHouse);
        //}
    }
}