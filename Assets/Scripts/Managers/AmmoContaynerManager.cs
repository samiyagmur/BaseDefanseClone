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
        
        private AmmoContaynerData _ammoContaynerData;
        [SerializeField]
        private AmmoWorkerStackController stackController;

        private IGridCretable _gridCretable;

        #endregion

        private void Awake()
        {
            _gridCretable = GetComponent<GridSystem>();
            _ammoContaynerData = GetData();
            SetStackDatas();

        }
        private void Start()
        {
            SendToContaynerTransformPositionInfo();
        }
        private AmmoContaynerData GetData() => Resources.Load<CD_AmmoContayner>("Data/CD_AmmoContayner").ammoContaynerData;

        private void SetStackDatas() => stackController.SetStackData(_ammoContaynerData);

        private void SendToContaynerTransformPositionInfo()
        {
            List<Vector3> gridSystemTransforms = _gridCretable.CreateGrid(_ammoContaynerData.gredObj,_ammoContaynerData.offSet,_ammoContaynerData.amount);
            float maxGridAmunt = _gridCretable.MaxCount();
            AmmoShopSignals.Instance.onGetAmmoContaynerGridPosList?.Invoke(gridSystemTransforms,gameObject);
            AmmoShopSignals.Instance.onGetMaxGridAmunt?.Invoke(maxGridAmunt, gameObject);
        }
        
        public void IsHitAmmoWorker()
        {
            stackController.AddToStack();
        }
    }
}