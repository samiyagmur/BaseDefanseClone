using Controllers;
using Data.UnityObject;
using Datas.UnityObject;
using Datas.ValueObject;
using Interfaces;
using Signals;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utilityies;
using Random = UnityEngine.Random;

namespace Managers
{
    public class AmmoContaynerManager : MonoBehaviour
    {

        #region SelfVariables

        #region Private Variables
      
        private GridData _gridData;

        private AmmoContaynerGridController _gridController;

        public Dictionary<GameObject, int> _turretsContayner = new Dictionary<GameObject, int>();

        private GameObject _selectedTarget;

        [SerializeField]
        private List<AmmoContaynerStackController> _selectableTargetStacks = new List<AmmoContaynerStackController>();


        #endregion

        #region Serilizefield Variebles


        #endregion

        #endregion

        #region Get&SetData

        private void Awake() => Init();

        private void Init()
        {
            
            _gridData = GetGridData();

            _gridController = NewGrid();

            GenerateGrid();

        }

        private GridData GetGridData() => Resources.Load<CD_GridData>("Data/AmmoContayner/CD_ContaynerData").ammoContaynerData;
        private AmmoContaynerGridController NewGrid() 
            => new AmmoContaynerGridController(_gridData.XGridSize, _gridData.YGridSize, _gridData.MaxContaynerAmount, _gridData.Offset);
        private void GenerateGrid() => _gridController.GanarateGrid();

        #endregion

        #region Event Subscription
        private void OnEnable() => SubscribeEvents();

        private void SubscribeEvents()
        {
            
        }

        private void UnsubscribeEvents()
        {
           
        }

        private void OnDisable() => UnsubscribeEvents();


        #endregion

        #region SentMomentİnfo

        internal void StackInfos()
        {
            _selectableTargetStacks = transform.GetComponentsInChildren<AmmoContaynerStackController>().ToList();

            _selectableTargetStacks= _selectableTargetStacks.OrderBy(x => x.GetCurrentCount()).ToList();

            _selectedTarget = _selectableTargetStacks[0].gameObject;
        }

        internal void SendToTargetInfo()
        {
            if (_selectableTargetStacks.Count != 0)
            {
               AmmoManagerSignals.Instance.onSetConteynerList?.Invoke(_selectedTarget);
            }

            else
            {
                Debug.Log("!!!_turrets Dictionary caunt=0 ");

               AmmoManagerSignals.Instance.onSetConteynerList?.Invoke(null);
            }
        }
        #endregion

        #region PhysicsMethods
        public void IsHitAmmoWorker()
        {
            _selectableTargetStacks.First().AddStack(_gridController.LastPosition());
        }
        #endregion

        #region Event Methods


        internal void EnterTurretContayner(List<GameObject> ammoWorkerStackList)
        {
            _selectableTargetStacks.First().SetAmmoWorkerList(ammoWorkerStackList);
           
        }

        public GameObject GetTargetStack()
        {
            return _selectedTarget;
        }

        #endregion

    }
}