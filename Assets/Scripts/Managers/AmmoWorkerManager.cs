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
using UnityEngine;
using UnityEngine.AI;

namespace Managers
{
    public class AmmoWorkerManager : MonoBehaviour
    {
        #region SelfVariables
        #region Private Variables

        private AmmoWorkerAIData _ammoWorkerAIData;
        private AmmoManagerPropertyDepository _AmmoManagerPropertyDepository;
        private GridData _gridData;


        private IGridAble gridAble;
        private List<GameObject> _ammoContaynerList;
        private int _isAmmoContaynerMaxAmount;
        private List<float> _ammoContaynerCurrentCount;
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
            _AmmoManagerPropertyDepository = GetComponent<AmmoWorkerBrain>();
            _ammoWorkerAIData = cD_AIData.AmmoWorkerAIDatas;
            _gridData = newGrid.ammoContaynerData;
            SetBrainData();
            SetGridData();
            _AmmoManagerPropertyDepository.Awake();
        }

        private void SetBrainData()
        {
            
            _AmmoManagerPropertyDepository.MovementSpeed = _ammoWorkerAIData.MovementSpeed;
            _AmmoManagerPropertyDepository.AmmoWareHouse = _ammoWorkerAIData.AmmoWareHouse;
            _AmmoManagerPropertyDepository.AmmoWorkerStack = _ammoWorkerAIData.AmmoStack;
            _AmmoManagerPropertyDepository.Animator = animator;
            _AmmoManagerPropertyDepository.Agent = agent;
            _AmmoManagerPropertyDepository.Ammo = _ammoWorkerAIData.Ammo;
            _AmmoManagerPropertyDepository.AmmoTransform = transform;
            _AmmoManagerPropertyDepository.CurrentAmmoTransportStatus = _ammoWorkerAIData.currentTransportAmmoStatus;
            _AmmoManagerPropertyDepository.AmmoWorkerGameObj = gameObject;
            _AmmoManagerPropertyDepository.AmmoWorker = _ammoWorkerAIData.AmmoWorker;

        }

        private void SetGridData() =>
             gridAble = new AmmoWorkerGridController(
            _gridData.XGridSize,
            _gridData.YGridSize,
            _gridData.MaxContaynerAmount,
            _gridData.Offset);

        public void IsSetConteynerLists(List<GameObject> getterConteynerList, int ammoContaynerMaxValue, List<float> getterConteynerCurrentAmunt)
        {
            _ammoContaynerList = getterConteynerList;
            _isAmmoContaynerMaxAmount = ammoContaynerMaxValue;
            _ammoContaynerCurrentCount = getterConteynerCurrentAmunt;
        }
        public void IsHitAmmoWareHouse(Transform ammmoShop)
        {
            //_stateUsers.AmmoContaynerList = AmmoWorkerSignals.Instance.onGetAllConteyner?.Invoke()[Random.Range(0, _ammoContaynerList.Count)].transform;
            //_stateUsers.ShopTransform = ammmoShop;
        }
       

        public void IsStayWareHouse()
        {
            gridAble.ganarateGrid();
            _AmmoManagerPropertyDepository.SendGridInfoToStack(gridAble.LastPosition(), _ammoWorkerAIData.Ammo, _AmmoManagerPropertyDepository.AmmoWareHouse);
        }

        


    }
}