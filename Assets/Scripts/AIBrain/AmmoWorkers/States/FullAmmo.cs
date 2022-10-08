using Abstraction;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace States
{
    public class FullAmmo :IState
    {
        #region Constructor

        private NavMeshAgent _agent;
        private Animator _animator;
        private float _movementSpeed;

        public FullAmmo(NavMeshAgent agent, Animator animator, float movementSpeed)
        {
            _agent = agent;
            _animator = animator;
            _movementSpeed = movementSpeed;
        }


        #endregion

        #region States
        public void OnEnter()
        {
            Debug.Log("FullAmmo");
            _agent.speed = 0;
           // _animator.SetTrigger("Idle");
        }

        public void OnExit()
        {
            _agent.speed = _movementSpeed;
           // _animator.SetTrigger("Walk");
        }

        public void Tick()
        {





        }

        #endregion

    }
}