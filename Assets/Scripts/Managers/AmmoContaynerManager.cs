using Controllers;
using Datas.UnityObject;
using Datas.ValueObject;
using Interfaces;
using Signals;
using System;
using System.Collections.Generic;
using UnityEngine;
using Utilityies;

namespace Managers
{
    public class AmmoContaynerManager : MonoBehaviour
    {

        #region SelfVariables

        #region Private Variables
        private GridData _gridData; 
        #endregion

        #region Serilizefield Variebles
        [SerializeField] private IGridAble gridController;

        [SerializeField] private CD_GridData newGrid;

        [SerializeField] private AmmoContaynerStackController ammoContaynerStackController;

        #endregion

        #endregion

        #region Get&SetData

        private void Awake()
        {
            Init();
            SetGridData();
        }
        private void Start()
        {
            //onSetConteyerList();
        }
        private void Init() => _gridData = newGrid.ammoContaynerData;
        private void SetGridData() =>
            gridController = new AmmoContaynerGridController(
            _gridData.XGridSize,
            _gridData.YGridSize,
            _gridData.MaxContaynerAmount,
            _gridData.Offset); 
        #endregion

        #region Event Subscription
        private void OnEnable() => SubscribeEvents();

        private void SubscribeEvents() => AmmoManagerSignals.Instance.onGetCurrentContaynerInfo += OnGetCurrentContaynerInfo;

        private void UnsubscribeEvents() => AmmoManagerSignals.Instance.onGetCurrentContaynerInfo -= OnGetCurrentContaynerInfo;

        private void OnDisable() => UnsubscribeEvents();

        #endregion

        #region SentMomentİnfo

        private void onSetConteyerList() => AmmoManagerSignals.Instance.onSetConteynerList(gameObject);

        private void onFullConteyner() => AmmoManagerSignals.Instance.onContaynerStackFull(_gridData.MaxContaynerAmount);

        private void onCurrentStackCount() => AmmoManagerSignals.Instance.onCurrentContaynerAmount(ammoContaynerStackController.CurrentAmunt());

        #endregion

        #region PhysicsMethods
        public void IsHitAmmoWorker()
        {
            gridController.ganarateGrid();

            Vector3 gridLastPoint = gridController.LastPosition();

            ammoContaynerStackController.AddStack(gridLastPoint, gameObject/*change*/, _gridData.MaxContaynerAmount);

        }

        #endregion

        #region Event Methods
        private void OnGetCurrentContaynerInfo()
        {
            onFullConteyner();
            onCurrentStackCount();
            onSetConteyerList();
        } 
        #endregion

    }
}