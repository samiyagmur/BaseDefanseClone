using Abstraction;
using AIBrain;
using Controllers;
using Data.UnityObject;
using Datas.ValueObject;
using Enums;
using Interfaces;
using Signals;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Managers
{
    

    public class AmmoManager : MonoBehaviour
    {
        #region Self-Private Variabels
        [SerializeField]
        private CD_AIData cD_AIData;

        private int counter;

        private AmmoWorkerAIData _ammoWorkerAIData;

        private GameObject _targetStack;

        private PlayerAmmoStackStatus _playerAmmoStackStatus;
        #endregion
        internal void Awake() => Init();

        private void Init() => _ammoWorkerAIData = cD_AIData.AmmoWorkerAIDatas;

        #region Event Subscription
        private void OnEnable() => SubscribeEvents();
        private void SubscribeEvents()
        {
            AmmoManagerSignals.Instance.onSetConteynerList += OnSetConteynerList;   
            AmmoManagerSignals.Instance.onPlayerEnterAmmoWorkerCreaterArea += OnPlayerEnterAmmoWorkerCreaterArea;
            
        }
        private void UnsubscribeEvents()
        {
            AmmoManagerSignals.Instance.onSetConteynerList -= OnSetConteynerList;
            AmmoManagerSignals.Instance.onPlayerEnterAmmoWorkerCreaterArea -= OnPlayerEnterAmmoWorkerCreaterArea;
        }
        private void OnDisable() => UnsubscribeEvents();

        #endregion

        public void IsExitOnTurretStack(AmmoWorkerStackController ammoWorkerStackController) => ammoWorkerStackController.SetClearWorkerStackList();

        internal void IsAmmoWorkerStackEmpty(AmmoWorkerBrain ammoWorkerBrain) => ammoWorkerBrain.IsStackFul(_playerAmmoStackStatus);

        internal void IsSetTargetTurretContayner(AmmoWorkerBrain ammoWorkerBrain) => ammoWorkerBrain.SetTargetTurretContayner(_targetStack);

        internal void IsEnterTurretStack(AmmoWorkerBrain ammoWorkerBrain) => ammoWorkerBrain.IsLoadTurret(true);

        internal void IsExitTurretStack(AmmoWorkerBrain ammoWorkerBrain) => ammoWorkerBrain.IsLoadTurret(false);

        internal void IsAmmoEnterAmmoWareHouse(AmmoWorkerBrain ammoWorkerBrain) => ammoWorkerBrain.SetTriggerInfo(true);

        internal void IsAmmoExitAmmoWareHouse(AmmoWorkerBrain ammoWorkerBrain) => ammoWorkerBrain.SetTriggerInfo(false);

        internal void IsStayOnAmmoWareHouse(AmmoWorkerBrain ammoWorkerBrain,AmmoWorkerStackController ammoWorkerStackController)
        {           
            
            if (counter < _ammoWorkerAIData.MaxStackCount)
            {
               
                ammoWorkerBrain.IsStackFul(PlayerAmmoStackStatus.Empty);

                ammoWorkerStackController.AddStack(_ammoWorkerAIData.AmmoWareHouse, ammoWorkerBrain.gameObject.transform, GetObject(PoolType.Ammo.ToString()));

                counter++;
            }
            else
            {
                ammoWorkerBrain.IsStackFul(PlayerAmmoStackStatus.Full);
            }

        }
        private void OnSetConteynerList(GameObject targetStack)
        {   
            _targetStack = targetStack;
            _playerAmmoStackStatus = PlayerAmmoStackStatus.Empty;

        }

        private void OnPlayerEnterAmmoWorkerCreaterArea(Transform workerCreater) => AddAmmaWorker(workerCreater);

        public GameObject GetObject(string poolName) => ObjectPoolManager.Instance.GetObject<GameObject>(poolName);

        public void AddAmmaWorker(Transform workerCreater)
        {
            GameObject ammoWorker = GetObject(PoolType.AmmoWorkerAI.ToString());

            ammoWorker.transform.position = workerCreater.position;
           
        }
      
        public void ResetItems() => counter = 0;




        #region Subscirabe Event methods

        #endregion

        #region Physics Methods

        #endregion

        #region SendInfo

        #endregion


    }
}