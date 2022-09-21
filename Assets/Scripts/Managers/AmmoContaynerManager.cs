using Controllers;
using Datas.UnityObject;
using Datas.ValueObject;
using System;
using System.Collections;
using UnityEngine;

namespace Managers
{
    public class AmmoContaynerManager : MonoBehaviour
    {

        #region SelfVariables
        
        private AmmoContaynerData _ammoContaynerData;
        [SerializeField]
        private AmmoWorkerStackController stackController;
        #endregion


        private void Awake()
        {
            _ammoContaynerData = GetData();
            SetStackDatas();
        }

        private AmmoContaynerData GetData() => Resources.Load<CD_AmmoContayner>("Data/CD_AmmoContayner").ammoContaynerData;

        private void SetStackDatas() => stackController.SetStackData(_ammoContaynerData);

        private void SendToAmountInfo()
        {

        }

        public void IsHitAmmoWorker()
        {
            stackController.AddToStack();
        }
    }
}