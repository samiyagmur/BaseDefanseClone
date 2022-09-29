using Abstraction;
using AIBrain;
using Controllers;
using Enums;
using Interfaces;
using Managers;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace States
{
    public class TakeAmmo : IState
    {

        #region Constructor

        private AmmoWorkerStackController _stackable;
        private AmmoWorkerBrain _ammoWorkerBrain;
        private NavMeshAgent _agent;
        private Animator _animator;
        private Transform _ammoWareHouse;
        private int _ammoWorkerStackMaxAmount;
        private Transform _ammoWorker;


        private float _timer = 0.2f;
        private int counter;
    
        private PlayerAmmaStackStatus _playerAmmaStackStatus;

        public TakeAmmo(NavMeshAgent agent, Animator animator, Transform ammoWareHouse, int ammoWorkerStackMaxAmount, Transform ammoWorker, AmmoWorkerBrain ammoWorkerBrain)
        {
            _agent = agent;
            _animator = animator;
            _ammoWareHouse = ammoWareHouse;
            _ammoWorkerStackMaxAmount = ammoWorkerStackMaxAmount;
            _ammoWorker = ammoWorker;
            _ammoWorkerBrain= ammoWorkerBrain;
        }



        #endregion

        #region State
        public  void Enter()
        {   
            _stackable = new AmmoWorkerStackController();
            Debug.Log("TakeAmmoEnter");
            _agent.speed = 0;
            
        }

        public void Exit()
        {
            
      
        }
        public void Tick()
        {
            _timer -= Time.deltaTime;

            if (_timer < 0 )
            {
                if (counter < _ammoWorkerStackMaxAmount)
                {
                    _stackable.AddStack(_ammoWareHouse, _ammoWorkerBrain.gameObject.transform, GetObject(PoolType.Ammo.ToString()));
                    _timer = 0.1f;
                    counter++;
                   
                }
                else
                {
                    _playerAmmaStackStatus = PlayerAmmaStackStatus.Full;
                }

            }
        }

        public GameObject GetObject(string poolName)
        {
            return ObjectPoolManager.Instance.GetObject<GameObject>(poolName);
        }
        public PlayerAmmaStackStatus IsStackFull()
        {
            return _playerAmmaStackStatus;
        }



        #endregion


    }
}