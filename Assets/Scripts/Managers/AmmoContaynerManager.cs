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

        public CD_AmmoContayner newContayner;
        #endregion

        private void Awake()
        {
            _gridCretable = GetComponent<GridSystem>();
            SetStackDatas();

        }
        private void Start()
        {
            SendToContaynerTransformPositionInfo();
        }


        private void SetStackDatas() => stackController.SetStackData(_ammoContaynerData);

        private void SendToContaynerTransformPositionInfo()
        {
            List<Vector3> gridSystemTransforms = _gridCretable.CreateGrid(newContayner.ammoContaynerData.gredObj, newContayner.ammoContaynerData.offSet, newContayner.ammoContaynerData.amount);
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