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

        public Dictionary<GameObject, int> _turrets = new Dictionary<GameObject, int>();

        private GameObject _selectedTarget;


        private AmmoContaynerStackController _selectedTargetStack;

        [SerializeField]
        private List<GameObject> gameObjectsss = new List<GameObject>();
        [SerializeField]
        private List<int> stackcounts = new List<int>();

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

            Debug.Log(targetStack.parent.gameObject.name);
            _turrets.Add(targetStack.parent.gameObject,count );
            gameObjectsss.Add(targetStack.parent.gameObject);
            stackcounts.Add(count);
             _turrets = _turrets.OrderBy(obj => obj.Value).ToDictionary(obj => obj.Key, obj => obj.Value);

            _selectedTarget = _turrets.ElementAt(0).Key;

 
            _selectedTargetStack = _selectedTarget.GetComponentInChildren<AmmoContaynerStackController>();


            Debug.Log(_turrets.Count);

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
            _turrets.Clear();
            gameObjectsss.Clear();
            gameObjectsss.TrimExcess();
            stackcounts.Clear();
            gameObjectsss.TrimExcess();


            _selectedTargetStack.GetComponentInChildren<AmmoContaynerStackController>().AddStack(_gridController.LastPosition());
        }
        #endregion

        #region Event Methods


        internal void EnterTurretContayner(Transform ammoManager)
        {
           List<GameObject> ammoWorkerStackList = ammoManager.GetComponent<AmmoWorkerStackController>().SendAmmoStack();



            _selectedTargetStack.GetComponentInChildren<AmmoContaynerStackController>().SetAmmoWorkerList(ammoWorkerStackList);
           
        }


        #endregion

    }
}