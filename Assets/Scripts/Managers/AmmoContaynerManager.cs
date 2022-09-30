using Controllers;
using Datas.UnityObject;
using Datas.ValueObject;
using Interfaces;
using Signals;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utilityies;

namespace Managers
{
    public class AmmoContaynerManager : MonoBehaviour
    {

        #region SelfVariables

        #region Private Variables
      
        private GridData _gridData;

        private AmmoContaynerGridController _gridController;

        private Dictionary<int, GameObject> _turrets = new Dictionary< int, GameObject >();
        private GameObject _selectedTarget;


        private AmmoContaynerStackController _selectedTargetStack;
        
        
            

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

        internal void StackInfos(int count, Transform targetStack)
        {   
            foreach (var item in _turrets)
                if (item.Value == targetStack.parent.gameObject) return;
            

            _turrets.Add(count, targetStack.parent.gameObject);

             _turrets = _turrets.OrderBy(obj => obj.Key).ToDictionary(obj => obj.Key, obj => obj.Value);

            _selectedTarget = _turrets.ElementAt(0).Value;

          

            _selectedTargetStack = _selectedTarget.GetComponentInChildren<AmmoContaynerStackController>();
        }
    
        internal void SendToTargetInfo(List<GameObject> emtyAmmoStack)
        {
            if (_turrets.Count != 0)
            {
               AmmoManagerSignals.Instance.onSetConteynerList?.Invoke(_selectedTarget, emtyAmmoStack);
            }

            else 
            {
                Debug.Log("!!!_turrets Dictionary caunt=0 ");

                AmmoManagerSignals.Instance.onSetConteynerList?.Invoke(null,null);
            } 
        }
        #endregion

        #region PhysicsMethods
        public void IsHitAmmoWorker()
        {
            Debug.Log(_gridController.LastPosition().Count);


            _selectedTargetStack.GetComponentInChildren<AmmoContaynerStackController>().AddStack(_gridController.LastPosition());
        }
        #endregion

        #region Event Methods


        internal void EnterTurretContayner(Transform ammoManager)
        {
           List<GameObject> ammoWorkerStackList = ammoManager.GetComponent<AmmoWorkerStackController>().SendAmmoStack();

            foreach (var item in ammoWorkerStackList)
                

            _selectedTargetStack.GetComponentInChildren<AmmoContaynerStackController>().SetAmmoWorkerList(ammoWorkerStackList);
           
        }


        #endregion

    }
}