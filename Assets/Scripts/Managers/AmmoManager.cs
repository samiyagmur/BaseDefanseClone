using Abstraction;
using AIBrain;
using Enums;
using Interfaces;
using Signals;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{


    public class AmmoManager : MonoBehaviour
    {
        #region Self-Private Variabels
        [SerializeField]
        private Transform ammoCreater;
        [SerializeField]
        private List<GameObject> _ammoWorkerList=new List<GameObject>();

        AmmoWorkerBrain ammoWorkerBrain;
        #endregion

        private void Start()
        {

        }
        #region Event Subscription

        private void OnEnable() => SubscribeEvents();

        private void SubscribeEvents()
        {
            AmmoManagerSignals.Instance.onSetConteynerList += OnSetConteynerList;
            AmmoManagerSignals.Instance.onPlayerEnterAmmoWorkerCreaterArea += OnPlayerEnterAmmoWorkerCreaterArea;
        }

        internal void IsEnterTurretContayner()
        {
            ammoWorkerBrain.IsLoadTurret(true);
        }

        internal void IsExitTurretContayner()
        {
            ammoWorkerBrain.IsLoadTurret(false);
        }

        private void UnsubscribeEvents()
        {
            AmmoManagerSignals.Instance.onSetConteynerList -= OnSetConteynerList;
            AmmoManagerSignals.Instance.onPlayerEnterAmmoWorkerCreaterArea -= OnPlayerEnterAmmoWorkerCreaterArea;
        }

        private void OnDisable() => UnsubscribeEvents();

        #endregion

        

        internal void IsAmmoEnterAmmoWareHouse()
        {
            ammoWorkerBrain.SetTriggerInfo(true);
        }

        internal void IsAmmoExitAmmoWareHouse() => ammoWorkerBrain.SetTriggerInfo(false);

        private void OnSetConteynerList(GameObject targetContayner)
        {
            if (_ammoWorkerList.Count != 0)
                ammoWorkerBrain.SetTargetTurretContayner(targetContayner);
        }

        private void OnPlayerEnterAmmoWorkerCreaterArea(Transform workerCreater)
        {
            AddAmmaWorker(workerCreater);
            ammoWorkerBrain = _ammoWorkerList[_ammoWorkerList.Count - 1].GetComponent<AmmoWorkerBrain>();
        }

        public void AddAmmaWorker(Transform workerCreater)
        {
            GameObject ammoWorker = GetObject(PoolType.AmmoWorkerAI.ToString());
            ammoWorker.transform.position = workerCreater.position;
            _ammoWorkerList.Add(ammoWorker);
        }
        public GameObject GetObject(string poolName)
        {
            return ObjectPoolManager.Instance.GetObject<GameObject>(poolName);

        }

        #region Subscirabe Event methods

        #endregion

        #region Physics Methods

        #endregion

        #region SendInfo

        #endregion


    }
}