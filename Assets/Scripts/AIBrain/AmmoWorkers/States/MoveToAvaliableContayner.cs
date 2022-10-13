using Abstraction;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace States
{
    public class MoveToAvaliableContayner :  IState
    {
        #region Constructor

        private NavMeshAgent _agent;
        private Animator _animator;
        private float _movementSpeed;
        private GameObject _decidedAmmoDropZone;

        public void SetData(GameObject decidedContayner)
        {
            _decidedAmmoDropZone = decidedContayner;
        }

        public MoveToAvaliableContayner(NavMeshAgent agent, Animator animator, float movementSpeed)
        {
            _agent = agent;
            _animator = animator;
            _movementSpeed = movementSpeed;
        }

        #endregion

        #region State
        public void OnEnter()
        {   

            _agent.speed = _movementSpeed;
           // _animator.SetTrigger("Walk");
            _agent.SetDestination(_decidedAmmoDropZone.transform.position);
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