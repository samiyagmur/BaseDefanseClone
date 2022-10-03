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
        #endregion

        #region Serilizefield Variebles

        [SerializeField]

        private List<AmmoContaynerStackController> _selectableTargetStacks = new List<AmmoContaynerStackController>();
        private GridDatas _gridData;
        private AmmoContaynerGridController _gridController;

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

        private void Start() => SendToTargetFirstTimes();

        private async void SendToTargetFirstTimes()
        {
            SelectTarget();

            await Task.Delay(50);

            SendToTargetInfo();
        } 

        #endregion

        #region Get&SetData
        private GridDatas GetGridData() => Resources.Load<CD_GridData>("Data/AmmoContayner/CD_ContaynerData").ammoContaynerData;

        private AmmoContaynerGridController GridController() 
            => new AmmoContaynerGridController(_gridData.XGridSize, _gridData.YGridSize, _gridData.MaxContaynerAmount, _gridData.Offset);

        private void GenerateGrid() => _gridController.GanarateGrid();

        #endregion

        #region SendMomentInfo

        internal void SelectTarget()
        {

            _selectableTargetStacks = transform.GetComponentsInChildren<AmmoContaynerStackController>().ToList();

            _selectableTargetStacks = _selectableTargetStacks.OrderBy(x => x.GetCurrentCount()).ToList();

            _selectedTarget = _selectableTargetStacks[0].gameObject;

 
        }

        internal void SendToTargetInfo() => AmmoManagerSignals.Instance.onSetConteynerList?.Invoke(_selectedTarget);

        #endregion

        #region PhysicsMethods
        public void IsHitAmmoWorker() => _selectableTargetStacks.First().AddStack(_gridController.LastPosition());




        #endregion

        #region Event Methods

        internal void SetTurretStack(List<GameObject> ammoWorkerStackList) => _selectableTargetStacks.First().SetAmmoWorkerList(ammoWorkerStackList);

        #endregion




    }
}