using Abstraction;
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
        private NavMeshAgent _agent;
        private Animator _animator;

        private Transform _ammoWareHouse;
        private int _isAmmoContaynerMaxAmount;
        private float _timer = 0.2f;
        private int counter;
        private Transform _ammoWorker;
        private PlayerAmmaStackStatus _playerAmmaStackStatus;
        
        public TakeAmmo(NavMeshAgent agent, Animator animator)
        {
            _agent = agent;
            _animator = animator;
        }

        public  void SetData(Transform ammoWareHouse, int isAmmoContaynerMaxAmount,Transform ammoWorker)
        {

            _ammoWareHouse = ammoWareHouse;
            _isAmmoContaynerMaxAmount = isAmmoContaynerMaxAmount;
            _ammoWorker = ammoWorker;
        }


        #endregion

        #region State
        public  void Enter()
        {   
            _stackable = new AmmoWorkerStackController();

            Debug.Log(_ammoWareHouse + "  " + _isAmmoContaynerMaxAmount + "  " + _ammoWorker);
            _agent.speed = 0;
        }

        public void Exit()
        {
            Debug.Log("TakeAmmoExit");
      
        }
        public void Tick()
        {
            _timer -= Time.deltaTime;

            if (_timer < 0 )
            {
                if (counter < _isAmmoContaynerMaxAmount)
                {
                    _stackable.AddStack(_ammoWareHouse, _ammoWorker, GetObject(PoolType.Ammo.ToString()));
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