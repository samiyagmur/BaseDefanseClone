using Abstraction;
using AIBrain;
using Controllers;
using Data.UnityObject;
using Datas.UnityObject;
using Datas.ValueObject;
using Interfaces;
using Signals;
using States;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

namespace Managers
{
    public class AmmoWorkerManager : MonoBehaviour
    {
        #region SelfVariables
        #region Private Variables

        private AmmoWorkerAIData _ammoWorkerAIData;
        private AmmoManagerPropertyDepository _ammoManagerPropertyDepository;
        private GridData _gridData;

        #endregion
        #region SerializeField Variables
        [SerializeField]
        private CD_AIData cD_AIData;
        [SerializeField]
        private CD_GridData newGrid;
        [SerializeField]
        private NavMeshAgent agent;
        [SerializeField]
        private Animator animator;
        #endregion
        #endregion

        private void Awake()
        {   
            Init();
        }

        private void Init()
        {
            
            _ammoManagerPropertyDepository = GetComponent<AmmoWorkerBrain>();
            _ammoWorkerAIData = cD_AIData.AmmoWorkerAIDatas;
            _gridData = newGrid.ammoContaynerData;
            SetBrainData();
        }

        private void SetBrainData()
        {
            _ammoManagerPropertyDepository.MovementSpeed = _ammoWorkerAIData.MovementSpeed;
            _ammoManagerPropertyDepository.AmmoWareHouse = _ammoWorkerAIData.AmmoWareHouse;
            _ammoManagerPropertyDepository.AmmoWorkerStack = _ammoWorkerAIData.AmmoStack;
            _ammoManagerPropertyDepository.Animator = animator;
            _ammoManagerPropertyDepository.Agent = agent;
            _ammoManagerPropertyDepository.Ammo = _ammoWorkerAIData.Ammo;
            _ammoManagerPropertyDepository.AmmoTransform = transform;
            _ammoManagerPropertyDepository.CurrentAmmoTransportStatus = _ammoWorkerAIData.currentTransportAmmoStatus;
            _ammoManagerPropertyDepository.AmmoWorkerGameObj = gameObject;
            _ammoManagerPropertyDepository.AmmoWorker = _ammoWorkerAIData.AmmoWorker;
        }


        public void IsSetConteynerLists(List<GameObject> getterConteynerList, int ammoContaynerMaxValue, List<float> ammoContaynerCurrentCount)
        {
            _ammoManagerPropertyDepository.IsAmmoContaynerMaxAmount = ammoContaynerMaxValue;
            _ammoManagerPropertyDepository.SendContanerInfos(getterConteynerList, ammoContaynerMaxValue, ammoContaynerCurrentCount);
        }
        public async void IsEnterAmmoWareHouse()
        {
            _ammoManagerPropertyDepository.InplaceWorker = true;
           
        }
        internal void IsExitAmmoWareHouse()
        {
            _ammoManagerPropertyDepository.InplaceWorker=false;
        }
        public void IsStayWareHouse()
        {

            _ammoManagerPropertyDepository.SendStackInfos( _ammoManagerPropertyDepository.AmmoWareHouse, _ammoManagerPropertyDepository.IsAmmoContaynerMaxAmount, transform);

        }




    }
}