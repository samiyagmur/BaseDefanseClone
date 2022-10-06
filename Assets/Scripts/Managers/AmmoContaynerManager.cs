using Controllers;
using Datas.UnityObject;
using Datas.ValueObject;
using Enums;
using Signals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;


namespace Managers
{
    public class AmmoContaynerManager : MonoBehaviour
    {
        #region SelfVariables

        #region Private Variables
     
        private GameObject _selectedTarget;
        private int count;
        #endregion

        #region Serilizefield Variebles

        [SerializeField]

        private List<AmmoContaynerStackController> _selectableTargetStacks = new List<AmmoContaynerStackController>();
        private GridDatas _gridData;
        private AmmoContaynerGridController _gridController;

        public bool IsTargetChange { get; private set; }

        #endregion

        #endregion

        #region LoadScript

        private void Awake() => Init();

        private void Init()
        {

            _gridData=GetGridData();

            _gridController=GridController();

            GenerateGrid();
        }

        #endregion

        #region Get&SetData
        private GridDatas GetGridData() => Resources.Load<CD_GridData>("Data/AmmoContayner/CD_ContaynerData").ammoContaynerData;

        private AmmoContaynerGridController GridController() 
            => new AmmoContaynerGridController(_gridData.XGridSize, _gridData.YGridSize, _gridData.MaxContaynerAmount, _gridData.Offset);

        private void GenerateGrid() => _gridController.GanarateGrid();

        #endregion

        #region SendMomentInfo

        #region Event Subscription
        private void OnEnable() => SubscribeEvents();
        private void SubscribeEvents()
        {
            AmmoManagerSignals.Instance.onSetConteynerList += OnSetConteynerList;
        }

        private void UnsubscribeEvents()
        {
            AmmoManagerSignals.Instance.onSetConteynerList -= OnSetConteynerList;
        }
        private void OnDisable() => UnsubscribeEvents();

        #endregion
        internal void SelectTarget()
        {   
               
            _selectableTargetStacks = transform.GetComponentsInChildren<AmmoContaynerStackController>().ToList();

            _selectableTargetStacks = _selectableTargetStacks.OrderBy(x => x.GetCurrentCount()).ToList();

            _selectedTarget= _selectableTargetStacks[count].gameObject;
            Debug.Log("sda");
            AmmoManagerSignals.Instance.onAmmoStackStatus?.Invoke(AmmoStackStatus.Empty);
        }


        #endregion

        #region PhysicsMethods
        public void IsHitAmmoWorker() => _selectableTargetStacks.First().AddStack(_gridController.LastPosition());

        #endregion

        #region Event Methods

        internal void SetTurretStack(List<GameObject> ammoWorkerStackList) => _selectableTargetStacks.First().SetAmmoWorkerList(ammoWorkerStackList);

        #endregion

        private GameObject OnSetConteynerList()
        {
            SelectTarget();


            return _selectedTarget;
        }

    }
}